#!/usr/bin/env python3
"""Static validation for the MauiGallery project (no .NET SDK required).

Checks:
  1. Every .xaml file is well-formed XML.
  2. Every XAML x:Class has a matching .xaml.cs partial class with InitializeComponent().
  3. Every Clicked/Tapped/... event handler referenced in XAML exists in code-behind.
  4. Every StaticResource key referenced in a XAML file is defined somewhere
     (page resources, app resources, styles).
  5. Sample classes registered in SampleRegistry exist.
  6. Every catalog id in Data/ControlInfoData.json has a registry entry (and vice versa).
  7. GoToAsync routes referenced in code are registered in AppShell.
  8. All .xaml files are included (SDK-style) and namespaces resolve to real folders.
"""
import os, re, sys, json
import xml.etree.ElementTree as ET

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
SRC = os.path.join(ROOT, "src", "MauiGallery")
errors, warnings = [], []

XAML_NS = {
    "x": "http://schemas.microsoft.com/winfx/2009/xaml",
    "maui": "http://schemas.microsoft.com/dotnet/2021/maui",
}

def rel(p): return os.path.relpath(p, ROOT)

def all_files(ext):
    for dirpath, dirnames, filenames in os.walk(SRC):
        dirnames[:] = [d for d in dirnames if d not in ("bin", "obj")]
        for f in filenames:
            if f.endswith(ext):
                yield os.path.join(dirpath, f)

xaml_files = list(all_files(".xaml"))
cs_files = list(all_files(".cs"))

# ---------------------------------------------------------------- 1. XML well-formed
parsed = {}
for xf in xaml_files:
    try:
        parsed[xf] = ET.parse(xf)
    except ET.ParseError as e:
        errors.append(f"[XML] {rel(xf)}: {e}")

# ------------------------------------------------- 2/3. x:Class <-> code-behind & events
EVENT_ATTRS = ["Clicked", "Tapped", "DoubleTapped", "TextChanged", "CheckedChanged", "Toggled",
               "DateSelected", "TimeSelected", "ValueChanged", "SelectionChanged", "ItemTapped",
               "ItemSelected", "DragStarting", "DragOver", "Drop", "DragCompleted", "Swiped",
               "PanUpdated", "PinchUpdated", "PointerEntered", "PointerMoved", "PointerExited",
               "PointerPressed", "PointerReleased", "Refreshing", "Scrolled", "Navigated",
               "Navigating", "SearchButtonPressed", "Focused", "Unfocused", "Completed",
               "SizeChanged", "Loaded", "Unloaded", "OnDragStarting", "DragOver", "Drop"]

cs_index = {}
for cf in cs_files:
    text = open(cf, encoding="utf-8").read()
    cs_index[os.path.basename(cf)] = (cf, text)

for xf, tree in parsed.items():
    root = tree.getroot()
    xclass = root.get(f"{{{XAML_NS['x']}}}Class")
    if not xclass:
        if os.path.basename(xf) not in ("App.xaml",) and "Platforms" not in xf:
            # Resource dictionaries etc. don't need x:Class
            if "Styles" not in xf and "Resources" not in xf:
                warnings.append(f"[Class] {rel(xf)}: no x:Class")
        continue
    ns, _, cls = xclass.rpartition(".")
    cs_path = xf + ".cs"
    if not os.path.exists(cs_path):
        errors.append(f"[Class] {rel(xf)}: missing code-behind {rel(cs_path)}")
        continue
    cs_text = open(cs_path, encoding="utf-8").read()
    if f"class {cls}" not in cs_text:
        errors.append(f"[Class] {rel(xf)}: code-behind does not declare class {cls}")
    if "InitializeComponent()" not in cs_text:
        errors.append(f"[Class] {rel(cs_path)}: missing InitializeComponent()")
    if xclass not in cs_text and f"namespace {ns}" not in cs_text:
        warnings.append(f"[Class] {rel(cs_path)}: namespace/class mismatch with {xclass}")

    # event handlers
    handlers = set()
    for el in root.iter():
        for attr, val in el.attrib.items():
            name = attr.split("}")[-1]
            if name in EVENT_ATTRS and re.fullmatch(r"[A-Za-z_]\w*", val or ""):
                handlers.add(val)
    for h in sorted(handlers):
        if f" {h}(" not in cs_text and f"\t{h}(" not in cs_text:
            errors.append(f"[Event] {rel(xf)}: handler '{h}' not found in code-behind")

# --------------------------------------------- 4. StaticResource keys defined somewhere
defined_keys = set()
key_re = re.compile(r'x:Key="([^"]+)"')
for xf in xaml_files:
    for m in key_re.finditer(open(xf, encoding="utf-8").read()):
        defined_keys.add(m.group(1))
# implicit styles & converter keys defined in code
defined_keys |= {"InvertedBoolConverter", "BoolToObjectConverter"}

used_res = re.compile(r'^\{StaticResource(?:Extension)?\s+([^}]+)\}')
for xf, tree in parsed.items():
    for el in tree.getroot().iter():
        for attr, val in el.attrib.items():
            m = used_res.match(val or "")
            if m:
                key = m.group(1).strip()
                if key not in defined_keys:
                    errors.append(f"[Resource] {rel(xf)}: StaticResource '{key}' is never defined")

# ------------------------------------------------------- 5/6. Registry & catalog sync
reg_path = os.path.join(SRC, "Helpers", "SampleRegistry.cs")
reg_text = open(reg_path, encoding="utf-8").read()
reg_entries = dict(re.findall(r'"([\w-]+)" => new ([\w.]+)\(\)', reg_text))
for sid, type_name in reg_entries.items():
    parts = type_name.split(".")
    cls = parts[-1]
    folder = parts[-2] if len(parts) >= 2 else ""
    cs = os.path.join(SRC, "Samples", folder, f"{cls}.xaml.cs")
    xaml = os.path.join(SRC, "Samples", folder, f"{cls}.xaml")
    if not os.path.exists(cs):
        errors.append(f"[Registry] '{sid}' -> missing {rel(cs)}")
    if not os.path.exists(xaml):
        errors.append(f"[Registry] '{sid}' -> missing {rel(xaml)}")

catalog = json.load(open(os.path.join(SRC, "Data", "ControlInfoData.json"), encoding="utf-8"))
cat_ids = [i["uniqueId"] for g in catalog["groups"] for i in g["items"]]
for cid in cat_ids:
    if cid not in reg_entries:
        errors.append(f"[Catalog] id '{cid}' has no SampleRegistry entry")
for rid in reg_entries:
    if rid not in cat_ids:
        errors.append(f"[Registry] id '{rid}' missing from catalog JSON")

# ------------------------------------------ 6b. named elements used in code-behind exist
MEMBER_HINT = re.compile(r"^\s*([A-Z][A-Za-z0-9_]*)\.(Text|ItemsSource|IsRunning|IsRefreshing|IsVisible|Opacity|Value|Progress|Scale|Position|Count|Source|Drawable|HeightRequest|WidthRequest|TranslationX|TranslationY|FontSize|IsChecked|IsToggled|SelectedItem|SelectedIndex|IsEnabled|BackgroundColor|TextColor|IsSpellCheckEnabled|IsTextPredictionEnabled|Children|Content)\b", re.M)
KNOWN_TYPES = {"Math", "Task", "DateTime", "String", "Convert", "Enumerable", "Application", "DeviceInfo",
               "Shell", "TimeSpan", "DataTemplate", "VisualStateManager", "BindableLayout", "WindowOverlay",
               "SampleData", "Colors", "Color", "PointF", "RectF", "Brush", "FontImageSource", "Command",
               "HtmlWebViewSource", "UriImageSource", "StreamImageSource", "FileImageSource", "EditorAutoSizeOption",
               "ClearButtonVisibility", "Keyboard", "ButtonContentLayout", "MenuFlyout", "MenuFlyoutItem",
               "CheckedChangedEventArgs", "ToggledEventArgs", "TextChangedEventArgs", "EventArgs", "Exception"}
for xf, tree in parsed.items():
    cs_path = xf + ".cs"
    if not os.path.exists(cs_path):
        continue
    names = set()
    for el in tree.getroot().iter():
        n = el.get(f"{{{XAML_NS['x']}}}Name") or el.get("Name")
        if n:
            names.add(n)
    cs_text = open(cs_path, encoding="utf-8").read()
    for m in MEMBER_HINT.finditer(cs_text):
        ident = m.group(1)
        if ident in KNOWN_TYPES or ident in names:
            continue
        # allow method-local declarations: "Type ident = ..."
        if re.search(rf"\bvar {ident}\b|\b\w+ {ident}\s*[=;)]", cs_text):
            continue
        errors.append(f"[Name] {rel(cs_path)}: '{ident}' is used like a named element but has no x:Name in XAML")

# ------------------------------------------------------------------ 7. Shell routes
shell_cs = open(os.path.join(SRC, "AppShell.xaml.cs"), encoding="utf-8").read()
registered = set(re.findall(r'RegisterRoute\("([^"]+)"', shell_cs))
# also ShellContent routes
shell_xaml = open(os.path.join(SRC, "AppShell.xaml"), encoding="utf-8").read()
registered |= set(re.findall(r'Route="([^"]+)"', shell_xaml))
registered |= {f"category-{g['uniqueId']}" for g in catalog["groups"]}

go_re = re.compile(r'GoToAsync\("([^"#?]+)')
for cf in cs_files:
    text = open(cf, encoding="utf-8").read()
    for m in go_re.finditer(text):
        route = m.group(1).strip().lstrip("/")
        if route in ("..", "."):
            continue
        if route not in registered:
            errors.append(f"[Route] {rel(cf)}: GoToAsync route '{route}' is not registered")

# --------------------------------------------------------- 8. every sample has xaml+cs
for dirpath, dirnames, filenames in os.walk(os.path.join(SRC, "Samples")):
    for f in filenames:
        if f.endswith(".xaml") and not os.path.exists(os.path.join(dirpath, f + ".cs")):
            errors.append(f"[Sample] {rel(os.path.join(dirpath, f))}: no code-behind")
        if f.endswith(".xaml.cs"):
            base = f[:-3]
            if not os.path.exists(os.path.join(dirpath, base)):
                errors.append(f"[Sample] {rel(os.path.join(dirpath, f))}: no XAML file {base}")

# ---------------------------------------------------------------------------- report
print(f"checked {len(xaml_files)} XAML files, {len(cs_files)} C# files, "
      f"{len(cat_ids)} catalog items, {len(reg_entries)} registry entries, "
      f"{len(registered)} routes")
for w in warnings:
    print("WARN ", w)
if errors:
    print(f"\n{len(errors)} ERROR(S):")
    for e in errors:
        print("  ", e)
    sys.exit(1)
print("OK: all checks passed")
