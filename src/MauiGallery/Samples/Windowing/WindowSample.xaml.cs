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
}
