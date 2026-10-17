#!/usr/bin/env python3
"""Generates the control catalog (Data/ControlInfoData.json) and the sample registry
(Helpers/SampleRegistry.cs) from the single SPEC table below.

Also (re)creates trivial code-behind stubs for sample views that don't have one yet,
so the XAML partial classes always compile.

Usage:  python3 tools/gen_catalog.py
"""
import json
import os

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
NS = "MauiGallery.Samples"

# ---------------------------------------------------------------------------
# Category spec: (id, title, subtitle, description)
# Folder/namespace for sample views is the PascalCase form of the id.
# ---------------------------------------------------------------------------
FOLDER = {
    "basicinput": "BasicInput",
    "text": "Text",
    "datetime": "DateTime",
    "collections": "Collections",
    "layouts": "Layouts",
    "media": "Media",
    "shapes": "Shapes",
    "statusinfo": "StatusInfo",
    "navigation": "Navigation",
    "menus": "Menus",
    "dialogs": "Dialogs",
    "gestures": "Gestures",
    "styles": "Styles",
    "accessibility": "Accessibility",
    "windowing": "Windowing",
    "legacy": "Legacy",
}
CATEGORIES = [
    ("basicinput", "基础输入 / Basic input", "按钮、选择与数值输入控件",
     "用于触发操作或收集用户选择的控件：Button、CheckBox、RadioButton、Slider、Stepper、Switch、Picker。"),
    ("text", "文本 / Text", "文本显示与编辑控件",
     "显示和编辑文本的控件：Label、Entry、Editor、SearchBar。"),
    ("datetime", "日期与时间 / Date & time", "日期和时间选择控件",
     "选择日期与时间的控件：DatePicker、TimePicker。"),
    ("collections", "集合 / Collections", "列表与集合展示控件",
     "展示数据集合的控件：CollectionView、CarouselView、ListView、TableView、IndicatorView。"),
    ("layouts", "布局 / Layouts", "组织子视图的布局控件",
     "定义子视图排列方式的布局：Grid、堆叠布局、AbsoluteLayout、FlexLayout、ScrollView、Border、ContentView、Frame（遗留）、BindableLayout。"),
    ("media", "媒体 / Media", "图像、网页与自定义绘制",
     "显示多媒体内容的控件：Image、ImageButton、WebView、HybridWebView、GraphicsView、BoxView。"),
    ("shapes", "图形 / Shapes & brushes", "矢量图形、路径与画刷",
     "Microsoft.Maui.Controls.Shapes 提供的矢量图形（Line、Polyline、Polygon、Ellipse、Rectangle、RoundRectangle、Path）与几何、变换、画刷。"),
    ("statusinfo", "状态与信息 / Status & info", "进度与刷新控件",
     "向用户展示运行状态的控件：ActivityIndicator、ProgressBar、RefreshView。"),
    ("navigation", "导航 / Navigation", "Shell 与页面导航",
     "页面导航与组织：Shell（FlyoutItem/Tab/ShellContent/SearchHandler）、NavigationPage、TabbedPage、FlyoutPage、工具栏。"),
    ("menus", "菜单 / Menus", "菜单栏、右键菜单与菜单项",
     "菜单系统：MenuBar、MenuBarItem、MenuFlyout 系列、ContextMenu（FlyoutBase.ContextFlyout）、MenuItem、KeyboardAccelerator。"),
    ("dialogs", "对话框与浮层 / Dialogs & flyouts", "弹窗、操作表与工具提示",
     "模态交互：DisplayAlert、DisplayActionSheet、DisplayPrompt、ToolTipProperties。"),
    ("gestures", "手势与输入 / Gestures & input", "手势识别与拖放",
     "交互手势：Tap、Swipe、Pan、Pinch、Pointer 手势识别器，SwipeView 滑动操作，以及拖放（Drag/Drop）与指针（Pointer）手势。"),
    ("styles", "样式与模板 / Styles & templates", "样式、动画、行为、触发器与模板",
     "外观与结构复用：Style、VisualStateManager、触发器（Trigger/DataTrigger/EventTrigger/MultiTrigger/StateTrigger）、AppThemeBinding、Shadow/Clip/Transform、DataTemplate/ControlTemplate、CSS 样式表。"),
    ("accessibility", "辅助功能 / Accessibility", "语义与自动化属性",
     "无障碍支持：SemanticProperties、AutomationProperties。"),
    ("windowing", "窗口 / Windowing", "Window、TitleBar 与窗口覆盖层",
     "窗口级能力：Window 事件与多窗口、TitleBar（Windows/Mac Catalyst）、IWindowOverlay 自定义覆盖层。"),
    ("legacy", "遗留 / Legacy", "兼容保留的控件",
     "为兼容保留、官方不推荐用于新代码的控件：Cells（TextCell/ImageCell/SwitchCell/EntryCell/ViewCell）、Compatibility 布局与 Effects。"),
]

# ---------------------------------------------------------------------------
# Sample spec:
# (id, category, class, title_zh, subtitle_zh, desc_zh, base_classes, api_ns, tags, docs, is_new, is_legacy)
# ---------------------------------------------------------------------------
DOCS_BASE = "https://learn.microsoft.com/dotnet/maui/user-interface/controls/"

def doc(path, title="官方文档 / Docs"):
    return [{"title": title, "uri": DOCS_BASE + path}]

SAMPLES = [
    # ---------------------------- basic input ----------------------------
    ("button", "basicinput", "ButtonSample", "Button 按钮", "响应点击操作的标准按钮",
     "Button 用于触发命令。支持文本、图像（ImageSource）、内容布局（ContentLayout）、字体、圆角、边距，以及 Clicked/Pressed/Released 事件。",
     ["Button", "View", "VisualElement"], "Microsoft.Maui.Controls.Button",
     ["button", "click", "command", "按钮"], doc("button"), False, False),
    ("checkbox", "basicinput", "CheckBoxSample", "CheckBox 复选框", "独立的布尔选择控件",
     "CheckBox 表示开/关选择，通过 IsChecked 与 CheckedChanged/Checked/Unchecked 事件交互。可以用 Color 自定义选中颜色。",
     ["CheckBox", "View"], "Microsoft.Maui.Controls.CheckBox",
     ["checkbox", "bool", "check", "复选"], doc("checkbox"), False, False),
    ("radiobutton", "basicinput", "RadioButtonSample", "RadioButton 单选按钮", "从一组选项中单选",
     "RadioButton 表示互斥选项。可配合 RadioButtonGroup.GroupName 分组，或用 GroupName 附加属性指定组；支持自定义内容模板与 Value 绑定。",
     ["RadioButton", "View"], "Microsoft.Maui.Controls.RadioButton",
     ["radio", "group", "choice", "单选"], doc("radio-button"), False, False),
    ("slider", "basicinput", "SliderSample", "Slider 滑块", "在数值范围内拖动选择",
     "Slider 通过 MinValue/MaxValue/Value 在连续区间内选择数值，支持 ValueChanged 与拖动结束 DragCompleted 事件。",
     ["Slider", "View"], "Microsoft.Maui.Controls.Slider",
     ["slider", "range", "value", "滑块"], doc("slider"), False, False),
    ("stepper", "basicinput", "StepperSample", "Stepper 步进器", "以固定步长增减数值",
     "Stepper 提供 +/- 按钮，以 Increment 为步长在 MinValue 与 MaxValue 之间调整 Value。",
     ["Stepper", "View"], "Microsoft.Maui.Controls.Stepper",
     ["stepper", "increment", "number", "步进"], doc("stepper"), False, False),
    ("switch", "basicinput", "SwitchSample", "Switch 开关", "开/关切换控件",
     "Switch 表示布尔状态切换，通过 IsToggled 与 Toggled 事件交互。视觉样式由平台原生控件呈现。",
     ["Switch", "View"], "Microsoft.Maui.Controls.Switch",
     ["switch", "toggle", "bool", "开关"], doc("switch"), False, False),
    ("picker", "basicinput", "PickerSample", "Picker 选择器", "从列表中选择一项",
     "Picker 显示可选项列表（Items/ItemsSource）并允许选择其一（SelectedIndex/SelectedItem），支持 Title 与自定义字体。类似其他平台的组合框（ComboBox）。",
     ["Picker", "View", "InputView"], "Microsoft.Maui.Controls.Picker",
     ["picker", "dropdown", "combo", "选择"], doc("picker"), False, False),

    # ------------------------------- text --------------------------------
    ("label", "text", "LabelSample", "Label 标签", "显示文本（含富文本）",
     "Label 显示单一样式文本或富文本（FormattedString/Span）。支持字体、颜色、行断模式（LineBreakMode）、TextType（Plain/Html）、语义属性等。",
     ["Label", "View"], "Microsoft.Maui.Controls.Label",
     ["label", "text", "span", "formatted", "文本"], doc("label"), False, False),
    ("entry", "text", "EntrySample", "Entry 单行输入框", "编辑单行文本",
     "Entry 提供单行文本输入，支持占位符、密码模式、键盘类型（Keyboard）、清除按钮、回车类型（ReturnType）以及 TextChanged/Completed 事件。",
     ["Entry", "InputView", "View"], "Microsoft.Maui.Controls.Entry",
     ["entry", "input", "textbox", "password", "输入"], doc("entry"), False, False),
    ("editor", "text", "EditorSample", "Editor 多行编辑器", "编辑多行文本",
     "Editor 提供多行文本编辑，支持自动调整高度（AutoSizeText）、最大长度、键盘类型与占位符。",
     ["Editor", "InputView", "View"], "Microsoft.Maui.Controls.Editor",
     ["editor", "textarea", "multiline", "多行"], doc("editor"), False, False),
    ("searchbar", "text", "SearchBarSample", "SearchBar 搜索栏", "带搜索按钮的输入框",
     "SearchBar 是带占位符与搜索按钮的输入控件，通过 SearchCommand/CommandParameter、TextChanged 与 SearchButtonPressed 事件工作。",
     ["SearchBar", "InputView", "View"], "Microsoft.Maui.Controls.SearchBar",
     ["search", "filter", "搜索"], doc("searchbar"), False, False),

    # ----------------------------- date/time -----------------------------
    ("datepicker", "datetime", "DatePickerSample", "DatePicker 日期选择器", "选择日期",
     "DatePicker 以平台原生方式选择日期，支持 Date、Format、MinimumDate、MaximumDate 与 DateSelected 事件。",
     ["DatePicker", "View"], "Microsoft.Maui.Controls.DatePicker",
     ["date", "calendar", "日期"], doc("datepicker"), False, False),
    ("timepicker", "datetime", "TimePickerSample", "TimePicker 时间选择器", "选择时间",
     "TimePicker 以平台原生方式选择时间，支持 Time、Format 与 TimeSelected 事件。",
     ["TimePicker", "View"], "Microsoft.Maui.Controls.TimePicker",
     ["time", "clock", "时间"], doc("timepicker"), False, False),

    # ---------------------------- collections ----------------------------
    ("collectionview", "collections", "CollectionViewSample", "CollectionView 集合视图", "灵活的列表/网格集合控件",
     "CollectionView 是展示可绑定数据集合的主推控件：支持线性/网格布局、水平滚动、分组、头尾模板、空视图（EmptyView）、下拉刷新（RefreshView 配合）与多种选择模式。",
     ["CollectionView", "SelectableItemsView", "ItemsView", "View"], "Microsoft.Maui.Controls.CollectionView",
     ["collectionview", "list", "grid", "items", "列表", "集合"], doc("collectionview"), False, False),
    ("carouselview", "collections", "CarouselViewSample", "CarouselView 轮播视图", "可滑动的项目轮播",
     "CarouselView 一次展示一个（或一屏）项目，可水平/垂直滑动切换，支持 Loop、PeekAreaInsets，并可与 IndicatorView 联动。",
     ["CarouselView", "ReorderableItemsView", "StructuredItemsView", "ItemsView", "View"], "Microsoft.Maui.Controls.CarouselView",
     ["carousel", "swipe", "banner", "轮播"], doc("carouselview"), False, False),
    ("listview", "collections", "ListViewSample", "ListView 列表（兼容）", "经典列表控件",
     "ListView 是 Xamarin.Forms 时代的列表控件，在 .NET MAUI 中仍可用但新代码建议使用 CollectionView。支持分组、单元格（Cells）、上下文菜单（ContextActions）与下拉刷新。",
     ["ListView", "View"], "Microsoft.Maui.Controls.ListView",
     ["listview", "grouping", "cell", "列表"], doc("listview"), False, True),
    ("tableview", "collections", "TableViewSample", "TableView 表格（兼容）", "设置页风格的表格",
     "TableView 以 TableRoot/TableSection 组织 Cell（TextCell、EntryCell 等），常用于设置页。新代码可用 CollectionView + Border 替代。",
     ["TableView", "View"], "Microsoft.Maui.Controls.TableView",
     ["table", "settings", "cell", "表格"], doc("tableview"), False, True),
    ("indicatorview", "collections", "IndicatorViewSample", "IndicatorView 指示器", "页面/项目位置指示点",
     "IndicatorView 显示项目位置指示器（小圆点/条形），常与 CarouselView 或 CollectionView 通过 ItemsSourceBy 绑定联动。",
     ["IndicatorView", "View"], "Microsoft.Maui.Controls.IndicatorView",
     ["indicator", "dots", "pager", "指示器"], doc("indicatorview"), False, False),

    # ------------------------------ layouts ------------------------------
    ("stacklayouts", "layouts", "StackLayoutSample", "StackLayout 堆叠布局", "垂直/水平顺序排列",
     "垂直与水平堆叠布局（VerticalStackLayout、HorizontalStackLayout）按顺序排列子视图，Spacing 控制间距。传统 StackLayout（Orientation 属性）为兼容保留。",
     ["VerticalStackLayout", "HorizontalStackLayout", "StackLayout", "Layout", "View"], "Microsoft.Maui.Controls.VerticalStackLayout",
     ["stack", "linear", "vertical", "horizontal", "堆叠"], doc("layouts/stacklayout"), False, False),
    ("grid", "layouts", "GridSample", "Grid 网格布局", "按行列组织子视图",
     "Grid 以行与列组织子视图。支持 Auto/* 绝对值尺寸（GridLength）、行高列宽定义（RowDefinition/ColumnDefinition）、跨行跨列（RowSpan/ColumnSpan）与间距。",
     ["Grid", "Layout", "View"], "Microsoft.Maui.Controls.Grid",
     ["grid", "row", "column", "table", "网格"], doc("layouts/grid"), False, False),
    ("absolutelayout", "layouts", "AbsoluteLayoutSample", "AbsoluteLayout 绝对布局", "按坐标与比例定位",
     "AbsoluteLayout 通过 LayoutBounds 与 LayoutFlags 将子视图定位在精确坐标（Absolute）或按比例（Proportional）布局，常用于叠加场景。",
     ["AbsoluteLayout", "Layout", "View"], "Microsoft.Maui.Controls.AbsoluteLayout",
     ["absolute", "position", "bounds", "坐标"], doc("layouts/absolutelayout"), False, False),
    ("flexlayout", "layouts", "FlexLayoutSample", "FlexLayout 弹性布局", "类似 CSS Flexbox",
     "FlexLayout 实现 CSS Flexbox 语义：Direction（主轴方向）、Wrap（换行）、JustifyContent（主轴对齐）、AlignItems/AlignContent（交叉轴对齐），以及子项的 Grow/Shrink/Basis。",
     ["FlexLayout", "Layout", "View"], "Microsoft.Maui.Controls.FlexLayout",
     ["flexbox", "wrap", "responsive", "弹性"], doc("layouts/flexlayout"), False, False),
    ("scrollview", "layouts", "ScrollViewSample", "ScrollView 滚动视图", "为内容提供滚动",
     "ScrollView 为超出可视区域的内容提供滚动，支持 Orientation、ScrollToRequest（ScrollToAsync）、ScrollX/ScrollY 与 Scrolled 事件。",
     ["ScrollView", "Layout", "View"], "Microsoft.Maui.Controls.ScrollView",
     ["scroll", "scrollview", "滚动"], doc("layouts/scrollview"), False, False),
    ("border", "layouts", "BorderSample", "Border 边框容器", "带描边与圆角的内容容器",
     "Border 为任意内容提供边框：Stroke、StrokeThickness、StrokeShape（Rectangle/RoundRectangle/Path）、StrokeDashArray、StrokeLineJoin/LineCap，并可搭配 Shadow。",
     ["Border", "TemplatedView", "View"], "Microsoft.Maui.Controls.Border",
     ["border", "stroke", "corner", "容器"], doc("layouts/border"), False, False),
    ("contentview", "layouts", "ContentViewSample", "ContentView 与模板", "可复用的内容容器",
     "ContentView 是最简单的可复用容器；TemplatedView + ControlTemplate + ContentPresenter/TemplateBinding 可定义可定制外观的复合控件。",
     ["ContentView", "TemplatedView", "Layout", "View"], "Microsoft.Maui.Controls.ContentView",
     ["contentview", "template", "controltemplate", "usercontrol", "模板", "容器"], doc("layouts/contentview"), False, False),
    ("frame", "layouts", "FrameSample", "Frame 框架（遗留）", "早期的内容边框控件",
     "Frame 是 Xamarin.Forms 时代的边框控件（CornerRadius、BorderColor、Padding），官方建议新代码改用 Border。",
     ["Frame", "ContentView", "View"], "Microsoft.Maui.Controls.Frame",
     ["frame", "legacy", "border", "框架"], doc("layouts/frame"), False, True),
    ("bindablelayout", "layouts", "BindableLayoutSample", "BindableLayout 可绑定布局", "给任意布局绑定集合",
     "BindableLayout 是附加能力（BindableLayout.ItemsSource + ItemTemplate + EmptyView），可把任意 Layout 变成由数据集合驱动的容器。",
     ["IBindableLayout", "Attached"], "Microsoft.Maui.Controls.BindableLayout",
     ["bindablelayout", "items", "collection", "动态布局"], doc("layouts/bindablelayout"), False, False),

    # ------------------------------- media -------------------------------
    ("image", "media", "ImageSample", "Image 图像", "显示图像（文件/URI/字体图标/流）",
     "Image 显示来自 FileImageSource、UriImageSource、FontImageSource 或 StreamImageSource 的图像，支持 Aspect（AspectFill/Fill/Center）与加载事件。",
     ["Image", "View"], "Microsoft.Maui.Controls.Image",
     ["image", "picture", "fontimage", "uri", "图像"], doc("image"), False, False),
    ("imagebutton", "media", "ImageButtonSample", "ImageButton 图像按钮", "以图像呈现的按钮",
     "ImageButton 是可点击的图像控件：支持 Aspect、Command/CommandParameter、Clicked/Pressed/Released 事件以及 BorderWidth/BorderColor/CornerRadius。",
     ["ImageButton", "View"], "Microsoft.Maui.Controls.ImageButton",
     ["imagebutton", "button", "icon", "图像按钮"], doc("imagebutton"), False, False),
    ("webview", "media", "WebViewSample", "WebView 网页视图", "显示网页与 HTML 内容",
     "WebView 加载网页（UrlWebViewSource）、HTML 字符串（HtmlWebViewSource）或本地文件，支持导航事件、GoBackAsync/GoForwardAsync 与 EvaluateJavaScriptAsync。",
     ["WebView", "View"], "Microsoft.Maui.Controls.WebView",
     ["web", "html", "browser", "网页"], doc("webview"), False, False),
    ("hybridwebview", "media", "HybridWebViewSample", "HybridWebView 混合视图", "JS 与 C# 双向互操作",
     "HybridWebView 在应用内承载本地 HTML/JS（HybridRoot/DefaultFile），并支持 JS→C#（RawMessageReceived / InvokeDotNet）与 C#→JS（InvokeJavaScriptAsync）双向调用。",
     ["HybridWebView", "View"], "Microsoft.Maui.Controls.HybridWebView",
     ["hybrid", "javascript", "interop", "混合", "js"], doc("hybridwebview"), True, False),
    ("graphicsview", "media", "GraphicsViewSample", "GraphicsView 自定义绘制", "用 IDrawable 绘制矢量图",
     "GraphicsView 通过 IDrawable.Draw(ICanvas, RectF) 在任意画布上进行矢量绘制（路径、填充、渐变、文本），并可配合 StartInteraction 等手势使用。",
     ["GraphicsView", "View"], "Microsoft.Maui.Controls.GraphicsView",
     ["graphics", "draw", "canvas", "vector", "绘制"], doc("graphicsview"), False, False),
    ("boxview", "media", "BoxViewSample", "BoxView 色块", "纯色矩形色块",
     "BoxView 绘制简单的纯色矩形（Color），常作分隔线、占位块或背景色块，可设置 CornerRadius。",
     ["BoxView", "View"], "Microsoft.Maui.Controls.BoxView",
     ["boxview", "color", "rectangle", "色块"], doc("boxview"), False, False),

    # ------------------------------- shapes ------------------------------
    ("shapes", "shapes", "ShapesSample", "Shapes 形状", "Line / Polyline / Polygon / Ellipse / Rectangle / RoundRectangle",
     "Microsoft.Maui.Controls.Shapes 提供矢量形状：Line、Polyline、Polygon、Ellipse、Rectangle、RoundRectangle。所有形状继承 Shape，支持 Fill、Stroke、StrokeThickness、虚线（StrokeDashArray）与端点/连接样式。",
     ["Shape", "View", "Line", "Ellipse", "Rectangle"], "Microsoft.Maui.Controls.Shapes",
     ["shape", "ellipse", "rectangle", "line", "polygon", "vector", "形状"], doc("shapes/"), False, False),
    ("path", "shapes", "PathSample", "Path 路径", "几何路径与线段",
     "Path 使用 Geometry（PathGeometry、LineGeometry、EllipseGeometry、RectangleGeometry、GeometryGroup）绘制任意矢量轮廓；PathFigure 与各种 PathSegment（Line/Bezier/Poly*）定义路径细节。",
     ["Path", "Shape", "Geometry", "PathFigure"], "Microsoft.Maui.Controls.Shapes.Path",
     ["path", "geometry", "bezier", "arc", "vector", "路径"], doc("shapes/path"), False, False),
    ("brushes", "shapes", "BrushesSample", "Brush 画刷与变换", "纯色 / 线性渐变 / 径向渐变 / 图像画刷",
     "Brush 定义形状与控件的填充：SolidColorBrush、LinearGradientBrush、RadialGradientBrush、ImageBrush，以及 GradientStop。Transform（Rotate/Scale/Skew/Translate/Composite）可作用于形状与视图。",
     ["Brush", "SolidColorBrush", "GradientBrush", "Transform"], "Microsoft.Maui.Controls.Brush",
     ["brush", "gradient", "fill", "transform", "画刷", "渐变", "变换"], doc("shapes/gradient"), False, False),

    # ----------------------------- status info ---------------------------
    ("activityindicator", "statusinfo", "ActivityIndicatorSample", "ActivityIndicator 活动指示器", "忙碌状态指示",
     "ActivityIndicator 以旋转动画表示进行中的操作：IsRunning 控制显示，Color 控制颜色。",
     ["ActivityIndicator", "View"], "Microsoft.Maui.Controls.ActivityIndicator",
     ["activity", "spinner", "busy", "loading", "加载"], doc("activityindicator"), False, False),
    ("progressbar", "statusinfo", "ProgressBarSample", "ProgressBar 进度条", "确定/不确定进度",
     "ProgressBar 在 0~1 之间显示进度（Progress），支持 ProgressColor，可用于文件传输等场景。",
     ["ProgressBar", "View"], "Microsoft.Maui.Controls.ProgressBar",
     ["progress", "bar", "percent", "进度"], doc("progressbar"), False, False),
    ("refreshview", "statusinfo", "RefreshViewSample", "RefreshView 下拉刷新", "为滚动内容添加下拉刷新",
     "RefreshView 包裹可滚动内容（如 CollectionView、ScrollView），提供下拉刷新手势：IsRefreshing 与 RefreshCommand 配合完成数据刷新。",
     ["RefreshView", "Layout", "View"], "Microsoft.Maui.Controls.RefreshView",
     ["refresh", "pull", "reload", "刷新"], doc("refreshview"), False, False),

    # ----------------------------- navigation ----------------------------
    ("shell", "navigation", "ShellSample", "Shell 应用外壳", "导航、菜单与搜索的统一入口",
     "Shell 提供应用结构：Flyout（飞出菜单）/ TabBar（标签）/ 路由导航（GoToAsync）、URI 路由与查询参数（QueryProperty/IQueryAttributable）、SearchHandler 搜索、BackButtonBehavior、FlyoutHeader/Footer、TitleView 等。",
     ["Shell", "Page", "FlyoutItem", "Tab", "ShellContent"], "Microsoft.Maui.Controls.Shell",
     ["shell", "flyout", "tab", "route", "searchhandler", "导航", "路由"], doc("shell"), False, False),
    ("navigationpage", "navigation", "NavigationPageSample", "NavigationPage 堆栈导航", "Push/Pop 页面堆栈",
     "NavigationPage 维护页面堆栈：PushAsync/PopAsync、模态页（PushModalAsync/PopModalAsync）、导航事件与返回按钮行为。Shell 内部同样基于堆栈导航。",
     ["NavigationPage", "Page"], "Microsoft.Maui.Controls.NavigationPage",
     ["navigation", "stack", "push", "pop", "modal", "导航"], doc("navigationpage"), False, False),
    ("tabbedpage", "navigation", "TabbedPageSample", "TabbedPage 标签页", "多页签容器",
     "TabbedPage 以标签（Tab）组织多个子页，BarBackgroundColor/BarTextColor 控制外观。Shell 的 TabBar 提供类似能力。",
     ["TabbedPage", "Page"], "Microsoft.Maui.Controls.TabbedPage",
     ["tabbed", "tabs", "pages", "标签页"], doc("tabbedpage"), False, False),
    ("flyoutpage", "navigation", "FlyoutPageSample", "FlyoutPage 侧滑页", "主从式飞出菜单布局",
     "FlyoutPage 展示一个 Flyout（飞出菜单）与 Detail（详情页）区域，支持 FlyoutLayoutBehavior 与菜单切换。Shell Flyout 是更现代的实现。",
     ["FlyoutPage", "Page"], "Microsoft.Maui.Controls.FlyoutPage",
     ["flyout", "masterdetail", "menu", "侧滑"], doc("flyoutpage"), False, False),
    ("toolbar", "navigation", "ToolbarSample", "ToolbarItem 工具栏项", "页面顶部操作按钮",
     "ToolbarItem 向导航栏添加操作按钮，支持 Order（Primary/Secondary）、IconImageSource、Priority 与点击命令。",
     ["ToolbarItem", "MenuItem", "Element"], "Microsoft.Maui.Controls.ToolbarItem",
     ["toolbar", "appbar", "action", "工具栏"], doc("toolbaritem"), False, False),

    # ------------------------------- menus -------------------------------
    ("menubar", "menus", "MenuBarSample", "MenuBar 菜单栏", "桌面端菜单栏与快捷键",
     "MenuBar（Windows/Mac Catalyst）定义窗口菜单栏：MenuBarItem 为顶级菜单，MenuFlyoutItem/MenuFlyoutSubItem/MenuFlyoutSeparator 组成菜单，并可附加 KeyboardAccelerator 快捷键。",
     ["MenuBar", "MenuBarItem", "MenuFlyout", "Element"], "Microsoft.Maui.Controls.MenuBar",
     ["menubar", "menu", "accelerator", "shortcut", "菜单", "快捷键"], doc("menubar"), True, False),
    ("contextflyout", "menus", "ContextFlyoutSample", "ContextMenu 上下文菜单", "右键/长按弹出菜单",
     "通过附加属性 FlyoutBase.ContextFlyout 为任意视图附加上下文菜单（MenuFlyout），支持子菜单、分隔线、图标与快捷键。",
     ["FlyoutBase", "MenuFlyout", "MenuFlyoutItem"], "Microsoft.Maui.Controls.MenuFlyout",
     ["context", "flyout", "rightclick", "menu", "右键", "上下文菜单"], doc("contextflyout"), False, False),
    ("menuitem", "menus", "MenuItemSample", "MenuItem 菜单项", "Shell 飞出菜单项与列表上下文操作",
     "MenuItem 表示可点击菜单项（Shell Flyout 的 MenuItems、ListView/Cell 的 ContextActions），支持 Command、IconImageSource 与 IsEnabled。",
     ["MenuItem", "BaseMenuItem", "Element"], "Microsoft.Maui.Controls.MenuItem",
     ["menuitem", "contextactions", "shell", "菜单项"], doc("menuitem"), False, False),

    # ------------------------------ dialogs ------------------------------
    ("alerts", "dialogs", "AlertsSample", "对话框 Dialogs", "DisplayAlert / ActionSheet / Prompt",
     "Page 提供平台原态对话框 API：DisplayAlert（确认框）、DisplayActionSheet（操作表）与 DisplayPrompt（输入框），均为异步方法。",
     ["Page", "DisplayAlert", "DisplayActionSheet"], "Microsoft.Maui.Controls.Page",
     ["alert", "dialog", "actionsheet", "prompt", "弹窗", "对话框"], doc("dialog"), False, False),
    ("tooltip", "dialogs", "ToolTipSample", "ToolTip 工具提示", "悬停/长按提示",
     "ToolTipProperties.Text 为任意视图附加工具提示文本（桌面端悬停、移动端长按显示）。",
     ["ToolTipProperties", "Attached"], "Microsoft.Maui.Controls.ToolTipProperties",
     ["tooltip", "hint", "悬浮提示"], doc("tooltip"), True, False),

    # ------------------------------ gestures -----------------------------
    ("gestures", "gestures", "GesturesSample", "手势识别器 Gestures", "Tap / Swipe / Pan / Pinch / Pointer",
     "为视图附加手势：TapGestureRecognizer（点按，支持 NumberOfTapsRequired/Buttons/ButtonsMask）、SwipeGestureRecognizer（滑动方向）、PanGestureRecognizer（拖动位移）、PinchGestureRecognizer（缩放）。",
     ["GestureRecognizer", "TapGestureRecognizer", "PanGestureRecognizer"], "Microsoft.Maui.Controls.GestureRecognizer",
     ["gesture", "tap", "swipe", "pan", "pinch", "手势"], doc("gestures/tap"), False, False),
    ("draganddrop", "gestures", "DragAndDropSample", "拖放与指针 Drag & Pointer", "拖拽、放置与指针手势",
     "DragGestureRecognizer 启动拖放（CanDrag、DragStarting），DropGestureRecognizer 接收放置（AllowDrop、Drop、DragOver），PointerGestureRecognizer 处理指针进入/移动/离开。",
     ["DragGestureRecognizer", "DropGestureRecognizer", "PointerGestureRecognizer"], "Microsoft.Maui.Controls.DragGestureRecognizer",
     ["drag", "drop", "pointer", "mouse", "拖放", "指针"], doc("gestures/drag-and-drop"), False, False),
    ("swipeview", "gestures", "SwipeViewSample", "SwipeView 滑动操作", "SwipeItem / SwipeItemView / 阈值",
     "SwipeView 为内容包裹滑动操作区：LeftItems/RightItems/TopItems/BottomItems（SwipeItems）+ SwipeItem（Text/BackgroundColor/Invoked）/SwipeItemView（自定义项），Threshold 控制触发距离，SwipeBehaviorOnInvoked 控制点击后行为。",
     ["SwipeView", "SwipeItems", "SwipeItem"], "Microsoft.Maui.Controls.SwipeView",
     ["swipe", "swipeitem", "reveal", "滑动"], doc("user-interface/controls/swipeview"), True, False),

    # ------------------------------- styles ------------------------------
    ("styles", "styles", "StylesSample", "Style 样式", "显式/隐式样式与资源",
     "Style 集中定义属性（Setter）：隐式样式（TargetType 自动应用）、显式样式（x:Key + Style 引用）、样式继承（BasedOn）以及 StaticResource/DynamicResource 资源查找。",
     ["Style", "Setter", "ResourceDictionary"], "Microsoft.Maui.Controls.Style",
     ["style", "setter", "resource", "css", "样式", "资源"], doc("styles/"), False, False),
    ("visualstates", "styles", "VisualStateSample", "VisualStateManager 状态管理", "CommonStates 与自定义状态",
     "VisualStateManager 定义视图状态组（VisualStateGroup/VisualState），在状态切换时应用 Setter。CommonStates（Normal/Disabled/PointerOver/Pressed/Focused）由控件自动驱动，也可用 GoToState 自定义状态。",
     ["VisualStateManager", "VisualState", "Setter"], "Microsoft.Maui.Controls.VisualStateManager",
     ["vsm", "visualstate", "state", "状态"], doc("visualstatemanager"), False, False),
    ("triggers", "styles", "TriggerSample", "Triggers 触发器", "Trigger / DataTrigger / EventTrigger / MultiTrigger",
     "触发器按条件修改外观：Trigger（属性触发）、DataTrigger（绑定触发）、EventTrigger（事件触发 + TriggerAction）、MultiTrigger（多条件组合）。",
     ["Trigger", "DataTrigger", "EventTrigger", "MultiTrigger"], "Microsoft.Maui.Controls.Trigger",
     ["trigger", "datatrigger", "eventtrigger", "condition", "触发器"], doc("triggers"), False, False),
    ("statetriggers", "styles", "StateTriggerSample", "StateTriggers 状态触发器", "Adaptive / Compare / Device / Orientation",
     "StateTriggerBase 系列把条件绑定到 VisualState：AdaptiveTrigger（MinWindowWidth）、CompareStateTrigger（比较绑定值）、DeviceStateTrigger（设备状态）、OrientationStateTrigger（横竖屏）、StateTrigger（手动 IsActive）。",
     ["StateTriggerBase", "AdaptiveTrigger", "CompareStateTrigger"], "Microsoft.Maui.Controls.StateTriggerBase",
     ["statetrigger", "adaptive", "responsive", "触发器", "响应式"], doc("visualstatemanager"), False, False),
    ("theming", "styles", "ThemingSample", "主题与平台条件 Theming", "AppThemeBinding / OnPlatform / OnIdiom",
     "按主题与平台切换值：AppThemeBinding（Light/Dark/Default）、OnPlatform（按平台）、OnIdiom（按设备类型）。Application.UserAppTheme 可强制主题。",
     ["AppThemeBinding", "OnPlatform", "OnIdiom"], "Microsoft.Maui.Controls.AppThemeBinding",
     ["theme", "dark", "light", "platform", "idiom", "主题", "深色"], doc("user-interface/theme"), False, False),
    ("shadow", "styles", "ShadowSample", "Shadow / Clip / Transform", "阴影、裁剪与变换",
     "VisualElement 支持 Shadow（Offset/Radius/Brush/Opacity）、Clip（用 Geometry 裁剪内容）与 Translate/Scale/Rotation/AnchorX/AnchorY 变换。",
     ["Shadow", "VisualElement", "Transform"], "Microsoft.Maui.Controls.Shadow",
     ["shadow", "clip", "transform", "rotate", "scale", "阴影", "裁剪", "变换"], doc("user-interface/shadow"), False, False),
    ("datatemplates", "styles", "DataTemplateSample", "DataTemplate 与 ControlTemplate", "数据模板、选择器与控件模板",
     "DataTemplate 为数据定义视觉结构（支持 x:DataType 编译绑定），DataTemplateSelector 按数据切换模板，ControlTemplate + TemplateBinding + ContentPresenter 定义控件外观。",
     ["DataTemplate", "DataTemplateSelector", "ControlTemplate"], "Microsoft.Maui.Controls.DataTemplate",
     ["datatemplate", "selector", "controltemplate", "templatebinding", "模板"], doc("fundamentals/datatemplate"), False, False),
    ("animation", "styles", "AnimationSample", "Animation 动画", "ViewExtensions / Animation / Easing",
     "视图扩展动画 FadeTo/ScaleTo/TranslateTo/RotateTo/RelRotateTo/LayoutTo（Task 异步、AbortAnimation 取消）；Animation 类组合子动画（Insert/WithConcurrent、Commit 循环）；Easing 缓动函数（Sin/Cubic/Bounce…）。",
     ["Animation", "ViewExtensions", "Easing"], "Microsoft.Maui.Controls.Animation",
     ["animation", "easing", "fade", "rotate", "动画", "缓动"], doc("user-interface/animation"), False, False),
    ("behaviors", "styles", "BehaviorsSample", "Behavior 行为", "Behavior<T> 可复用交互逻辑",
     "Behavior/Behavior<T> 将交互逻辑封装为可复用对象（OnAttachedTo/OnDetachingFrom 生命周期），可带 BindableProperty，通过 Behaviors 集合附加到任意视图。",
     ["Behavior", "Behavior<T>"], "Microsoft.Maui.Controls.Behavior",
     ["behavior", "attached", "logic", "行为"], doc("fundamentals/behaviors"), True, False),

    # ---------------------------- accessibility --------------------------
    ("semantics", "accessibility", "SemanticsSample", "语义与自动化 Semantics", "SemanticProperties / AutomationProperties",
     "SemanticProperties（Description/Hint/HeadingLevel）向屏幕阅读器描述 UI；AutomationProperties 提供自动化支持（IsInAccessibleTree、HelpText、Name）。配合 ToolTipProperties 与视觉焦点顺序。",
     ["SemanticProperties", "AutomationProperties", "Attached"], "Microsoft.Maui.Controls.SemanticProperties",
     ["accessibility", "a11y", "screenreader", "semantic", "无障碍", "辅助功能"], doc("accessibility"), False, False),

    # ------------------------------ windowing ----------------------------
    ("window", "windowing", "WindowSample", "Window 窗口", "窗口事件与多窗口",
     "Window 表示应用窗口：提供 Activated/Deactivated/Stopped/Resumed/Destroyed 事件、Title/Width/Height/X/Y 等属性、多窗口创建（Application.OpenWindow）与覆盖层（Overlays）。",
     ["Window", "Element", "IWindowOverlay"], "Microsoft.Maui.Controls.Window",
     ["window", "multiwindow", "lifecycle", "窗口", "多窗口"], doc("window"), True, False),
    ("titlebar", "windowing", "TitleBarSample", "TitleBar 标题栏", "自定义窗口标题栏",
     "TitleBar（Windows / Mac Catalyst）自定义窗口标题栏：图标、标题/副标题、前置内容（Leading）/后置内容（Trailing）以及标准标题栏视觉状态。",
     ["TitleBar", "TemplatedView", "View"], "Microsoft.Maui.Controls.TitleBar",
     ["titlebar", "window", "chrome", "标题栏"], doc("titlebar"), True, False),
    ("windowoverlay", "windowing", "WindowOverlaySample", "WindowOverlay 窗口覆盖层", "在窗口上绘制覆盖层",
     "IWindowOverlay 允许在窗口内容之上绘制自定义覆盖层（Window.AddOverlay），常用于屏幕标注、水印或调试信息。",
     ["IWindowOverlay", "Window"], "Microsoft.Maui.Controls.IWindowOverlay",
     ["overlay", "window", "draw", "覆盖层"], doc("window"), False, False),

    # ------------------------------- legacy ------------------------------
    ("cells", "legacy", "CellsSample", "Cells 单元格（遗留）", "TextCell / ImageCell / SwitchCell / EntryCell / ViewCell",
     "Cell 系列是 ListView/TableView 的行模板（TextCell、ImageCell、SwitchCell、EntryCell、ViewCell），支持 ContextActions 上下文菜单。列表新代码请用 CollectionView + DataTemplate。",
     ["Cell", "TextCell", "ViewCell"], "Microsoft.Maui.Controls.Cell",
     ["cell", "textcell", "viewcell", "contextactions", "单元格"], doc("cells"), False, True),
    ("compat", "legacy", "CompatSample", "Compatibility 兼容层（遗留）", "Compatibility 布局、Effects 与平台配置",
     "Microsoft.Maui.Controls.Compatibility 保留了 Xamarin.Forms 时代的 RelativeLayout/Constraint* 布局与渲染器；Effects（RoutingEffect/PlatformEffect）与 PlatformConfiguration 用于平台特定定制。新代码建议使用 Handlers 与附加行为。",
     ["Compatibility", "Effect", "PlatformEffect"], "Microsoft.Maui.Controls.Compatibility",
     ["compatibility", "effect", "renderer", "platformconfiguration", "兼容"], doc("platform-integration/invoke-platform-code"), False, True),
]


def main() -> None:
    # ---- JSON catalog ----
    cat_by_id = {c[0]: {"uniqueId": c[0], "title": c[1], "subtitle": c[2], "description": c[3], "items": []}
                 for c in CATEGORIES}
    order = [c[0] for c in CATEGORIES]

    registry = []
    for (sid, cat, cls, title, subtitle, desc, bases, ns, tags, docs, is_new, is_legacy) in SAMPLES:
        cat_by_id[cat]["items"].append({
            "uniqueId": sid,
            "title": title,
            "subtitle": subtitle,
            "description": desc,
            "apiNamespace": ns,
            "baseClasses": bases,
            "docs": docs,
            "isNew": is_new,
            "isLegacy": is_legacy,
            "tags": tags,
        })
        registry.append((sid, FOLDER[cat], cls))

    catalog = {"groups": [cat_by_id[c] for c in order]}
    json_path = os.path.join(ROOT, "src", "MauiGallery", "Data", "ControlInfoData.json")
    with open(json_path, "w", encoding="utf-8") as f:
        json.dump(catalog, f, ensure_ascii=False, indent=2)
        f.write("\n")

    # ---- SampleRegistry.cs ----
    lines = [
        "// <auto-generated> by tools/gen_catalog.py — do not edit by hand. </auto-generated>",
        "using MauiGallery.Samples;",
        "",
        "namespace MauiGallery.Helpers;",
        "",
        "/// <summary>Creates the sample view that demonstrates one catalog control.</summary>",
        "public static class SampleRegistry",
        "{",
        "\tpublic static ContentView? Create(string uniqueId) => uniqueId switch",
        "\t{",
    ]
    for sid, cat, cls in registry:
        lines.append(f'\t\t"{sid}" => new {NS}.{cat}.{cls}(),')
    lines += [
        "\t\t_ => null,",
        "\t};",
        "}",
        "",
    ]
    reg_path = os.path.join(ROOT, "src", "MauiGallery", "Helpers", "SampleRegistry.cs")
    with open(reg_path, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))

    # ---- code-behind stubs (only when missing) ----
    for sid, cat, cls in registry:
        os.makedirs(os.path.join(ROOT, "src", "MauiGallery", "Samples", cat), exist_ok=True)
        cs_path = os.path.join(ROOT, "src", "MauiGallery", "Samples", cat, f"{cls}.xaml.cs")
        if not os.path.exists(cs_path):
            with open(cs_path, "w", encoding="utf-8") as f:
                f.write(f"""namespace {NS}.{cat};

public partial class {cls} : ContentView
{{
	public {cls}()
	{{
		InitializeComponent();
	}}
}}
""")

    print(f"catalog: {len(SAMPLES)} samples in {len(CATEGORIES)} categories")
    print(f"wrote {json_path}")
    print(f"wrote {reg_path}")


if __name__ == "__main__":
    main()
