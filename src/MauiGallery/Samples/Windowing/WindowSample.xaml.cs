namespace MauiGallery.Samples.Windowing;

public partial class WindowSample : ContentView
{
	public WindowSample()
	{
		InitializeComponent();

		if (Application.Current?.Windows.FirstOrDefault() is { } window)
		{
			window.Activated += (_, _) => LifecycleOutput.Text = $"Activated @ {DateTime.Now:HH:mm:ss}";
			window.Deactivated += (_, _) => LifecycleOutput.Text = $"Deactivated @ {DateTime.Now:HH:mm:ss}";
			window.Stopped += (_, _) => LifecycleOutput.Text = $"Stopped @ {DateTime.Now:HH:mm:ss}";
			window.Resumed += (_, _) => LifecycleOutput.Text = $"Resumed @ {DateTime.Now:HH:mm:ss}";
		}
	}

	private void OnInfo(object? sender, EventArgs e)
	{
		var window = Application.Current?.Windows.FirstOrDefault();
		if (window is null)
		{
			WindowInfo.Text = "未找到窗口 / No window";
			return;
		}

		WindowInfo.Text = $"Title: {window.Title} · Size: {window.Width:F0}×{window.Height:F0} · Overlays: {window.Overlays.Count}";
	}

	private Window? _childWindow;

	private void OnOpenWindow(object? sender, EventArgs e)
	{
		if (Application.Current is null)
			return;

		_childWindow = new Window(new ContentPage
		{
			Title = "第二个窗口 / Second window",
			Content = new Label
			{
				Text = "由 Application.OpenWindow 创建 /\ncreated via Application.OpenWindow",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			},
		});
		Application.Current.OpenWindow(_childWindow);
		WindowDemoOutput.Text = "已请求打开窗口（桌面平台生效）/ window opened (desktop)";
	}

	private void OnCloseWindow(object? sender, EventArgs e)
	{
		if (Application.Current is null || _childWindow is null)
		{
			WindowDemoOutput.Text = "没有可关闭的子窗口 / no child window";
			return;
		}

		Application.Current.CloseWindow(_childWindow);
		_childWindow = null;
		WindowDemoOutput.Text = "子窗口已关闭 / child window closed";
	}
}
