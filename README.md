# MAUI Gallery 🎨

**完整展示 .NET MAUI（`Microsoft.Maui.Controls`）全部内置控件的示例画廊应用。**

一个可运行的 .NET MAUI 应用，为每一个官方控件提供：**可交互的实时示例**、**可调选项**、以及**对应的 XAML / C# 源码**。
页面组织与交互设计参考微软官方 [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery)；控件清单、API 与示例语义依据 [dotnet/maui](https://github.com/dotnet/maui) 官方源码（`src/Controls/src/Core` 源码与 `PublicAPI` 清单）整理。

> *A complete .NET MAUI controls gallery — one interactive sample page per built-in control,
> modeled after the WinUI 3 Gallery, with the catalog derived from the official dotnet/maui source.*

---

## 功能特性 / Features

- **64 个控件示例页面**，覆盖 16 个分类（基础输入、文本、日期时间、集合、布局、媒体、图形画刷、状态、导航、菜单、对话框、手势、样式模板、辅助功能、窗口、遗留控件）
- 每页多个 `ControlExample` 示例块：**实时演示 + 参数选项 + XAML/C# 源码（可一键复制）**
- 参考 WinUI Gallery 的导航体验：主页 / 全部控件 / 分类 / 搜索 / 控件详情
- Shell 飞出菜单按分类导航，支持搜索（标题、副标题、描述、标签、API 命名空间）
- 主题切换（浅色 / 深色 / 跟随系统）
- 整页演示：NavigationPage、TabbedPage、FlyoutPage、MenuBar、Toolbar、TitleBar、WindowOverlay
- HybridWebView 演示 JS ⇄ C# 双向互操作（含 `wwwroot` 示例资产）

## 控件覆盖清单 / Control coverage

| 分类 | 数量 | 控件 |
| --- | --- | --- |
| 基础输入 / Basic input | 7 | **Button**、**CheckBox**、**RadioButton**、**Slider**、**Stepper**、**Switch**、**Picker** |
| 文本 / Text | 4 | **Label**、**Entry**、**Editor**、**SearchBar** |
| 日期与时间 / Date & time | 2 | **DatePicker**、**TimePicker** |
| 集合 / Collections | 5 | **CollectionView**、**CarouselView**、**ListView**、**TableView**、**IndicatorView** |
| 布局 / Layouts | 9 | **StackLayout**、**Grid**、**AbsoluteLayout**、**FlexLayout**、**ScrollView**、**Border**、**ContentView**、**Frame**、**BindableLayout** |
| 媒体 / Media | 6 | **Image**、**ImageButton**、**WebView**、**HybridWebView**、**GraphicsView**、**BoxView** |
| 图形 / Shapes & brushes | 3 | **Shapes**、**Path**、**Brush** |
| 状态与信息 / Status & info | 3 | **ActivityIndicator**、**ProgressBar**、**RefreshView** |
| 导航 / Navigation | 5 | **Shell**、**NavigationPage**、**TabbedPage**、**FlyoutPage**、**ToolbarItem** |
| 菜单 / Menus | 3 | **MenuBar**、**ContextMenu**、**MenuItem** |
| 对话框与浮层 / Dialogs & flyouts | 2 | **对话框**、**ToolTip** |
| 手势与输入 / Gestures & input | 2 | **手势识别器**、**拖放与指针** |
| 样式与模板 / Styles & templates | 7 | **Style**、**VisualStateManager**、**Triggers**、**StateTriggers**、**主题与平台条件**、**Shadow**、**DataTemplate** |
| 辅助功能 / Accessibility | 1 | **语义与自动化** |
| 窗口 / Windowing | 3 | **Window**、**TitleBar**、**WindowOverlay** |
| 遗留 / Legacy | 2 | **Cells**、**Compatibility** |

> 覆盖范围 = `Microsoft.Maui.Controls` 公共 API 中的全部控件类型（含 Shell 体系、Shapes 几何画刷、
> 手势识别器、触发器/状态机、模板系统、Cells 等）。`Microsoft.Maui.Essentials`（地理定位、传感器等
> 平台服务）不属于控件范畴，未列入；`Microsoft.Maui.Controls.Maps` 为可选独立包，同样未列入。

## 快速开始 / Getting started

### 环境要求 / Prerequisites

- .NET 10 SDK（或修改 csproj 为 `net8.0-*` / `net9.0-*`）
- .NET MAUI workload：`dotnet workload install maui`
- Windows（Windows/Android 目标）、macOS（iOS/MacCatalyst 目标）或 Linux（仅 Android）

### 构建与运行 / Build & run

```bash
# 还原并构建（Windows 上自动包含 net10.0-windows 目标）
dotnet build

# Linux/CI 上只构建 Android
dotnet build -f net10.0-android

# 运行（示例）
dotnet run -f net10.0-android        # Android 模拟器/设备
dotnet run -f net10.0-windows10.0.19041.0   # Windows 桌面
dotnet run -f net10.0-maccatalyst    # Mac Catalyst
```

也可以用 Visual Studio 2022/2026（安装 “.NET Multi-platform App UI development” 工作负载）打开
`MauiGallery.sln` 直接部署到各平台模拟器。

### 校验脚本 / Validation

仓库内置无需 .NET SDK 的静态校验（XML 合法性、x:Class 配对、事件处理器、资源引用、目录/注册表同步、路由注册）：

```bash
python3 tools/validate.py
```

## 项目结构 / Project layout

```
MauiGallery/
├── MauiGallery.sln
├── tools/
│   ├── gen_catalog.py        # 单一数据规格 → 目录 JSON + SampleRegistry + 代码桩
│   └── validate.py           # 静态校验
└── src/MauiGallery/
    ├── MauiProgram.cs / App.xaml / AppShell.xaml   # 应用与 Shell 导航
    ├── Models/               # 控件目录数据模型（参考 WinUI Gallery ControlInfoData）
    ├── Data/ControlInfoData.json                   # 全部控件的目录数据（嵌入资源）
    ├── Helpers/              # 数据源、SampleRegistry、示例契约
    ├── Controls/             # 画廊外壳组件：ControlExample、PageHeader
    ├── Pages/                # 主页 / 全部控件 / 分类 / 控件详情 / 搜索 / 关于
    ├── Samples/<分类>/*Sample.xaml(.cs)            # 64 个控件示例（ContentView）
    ├── Demos/                # 整页演示（TabbedPage、FlyoutPage、TitleBar 等）
    └── Resources/            # 图标、字体、样式、HybridWebView wwwroot 资产
```

### 架构要点 / Architecture

- **目录驱动**：`Data/ControlInfoData.json`（由 `tools/gen_catalog.py` 生成）描述分类与控件元数据；
  `SampleRegistry` 将控件 id 映射到示例视图；`ControlInfoPage` 通过 Shell 路由
  `controlinfo?id=xxx` 托管「页头信息 + 示例视图」。
- **ControlExample**：可复用示例容器（标题 / 描述 / 徽章 / 实时内容 / 选项区 / XAML 与 C# 源码面板 / 复制按钮），
  对应 WinUI Gallery 的 `ControlExample` + `SampleCodePresenter`。
- **示例即视图**：每个控件的示例是 `ContentView`（而非 Page），可嵌入统一的控件详情页；
  需要整屏交互的场景（导航、菜单栏、标题栏）通过 `Demos/` 整页演示 + 路由跳转承载。

### 添加新示例 / Adding a sample

1. 在 `tools/gen_catalog.py` 的 `SAMPLES` 表中登记（id、分类、类名、标题、描述、API、文档链接、标签）；
2. 新建 `Samples/<分类>/<类名>.xaml(.cs)` 编写示例（使用 `controls:ControlExample`）；
3. 运行 `python3 tools/gen_catalog.py && python3 tools/validate.py`。

## 参考资料 / References

- [dotnet/maui 官方源码](https://github.com/dotnet/maui) — 控件清单取自 `src/Controls/src/Core`
  （含 `PublicAPI/net-android` 公共 API 清单、官方 `Controls.Sample` 示例组织）
- [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery) — 画廊信息架构（ControlInfoData、
  ControlExample、SampleCodePresenter、ItemPage/SectionPage/Search）与页面布局设计
- [.NET MAUI 官方文档](https://learn.microsoft.com/dotnet/maui/) — 各控件页的“官方文档”按钮直达对应文档

## 许可 / License

示例代码以 MIT 协议提供。.NET MAUI 与 WinUI Gallery 源码版权归 Microsoft 所有（MIT）。
App 图标、启动屏与 OpenSans 字体取自 dotnet/maui 官方模板资源（MIT）。
