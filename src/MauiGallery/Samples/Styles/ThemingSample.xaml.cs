namespace MauiGallery.Samples.Styles;

public partial class ThemingSample : ContentView
{
	public ThemingSample()
	{
		InitializeComponent();
		UpdateInfo();
	}

	private void OnLight(object? sender, EventArgs e) => SetTheme(AppTheme.Light);
	private void OnDark(object? sender, EventArgs e) => SetTheme(AppTheme.Dark);
	private void OnSystem(object? sender, EventArgs e) => SetTheme(AppTheme.Unspecified);

	private void SetTheme(AppTheme theme)
	{
		if (Application.Current is not null)
			Application.Current.UserAppTheme = theme;
		UpdateInfo();
	}

	private void UpdateInfo()
	{
		var app = Application.Current;
		ThemeOutput.Text = $"UserAppTheme: {app?.UserAppTheme} · PlatformAppTheme: {app?.PlatformAppTheme}";
		ThemeApiOutput.Text = $"PlatformAppTheme = {app?.PlatformAppTheme}, RequestedTheme = {app?.RequestedTheme}";
	}
}
