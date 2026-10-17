using System.Reflection;

namespace MauiGallery.Pages;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
		BindingContext = this;
		UpdateThemeLabel();
	}

	public string AppVersion
	{
		get
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0";
			return $"MAUI Gallery {version} · .NET MAUI {Microsoft.Maui.Controls.Application.Current?.GetType().Assembly.GetName().Version?.ToString(3) ?? "?"}";
		}
	}

	public string RuntimeInfo =>
		$"运行平台 / Platform: {DeviceInfo.Platform} {DeviceInfo.VersionString} · {DeviceInfo.Idiom}";

	private void OnThemeSystem(object? sender, EventArgs e) => SetTheme(AppTheme.Unspecified);
	private void OnThemeLight(object? sender, EventArgs e) => SetTheme(AppTheme.Light);
	private void OnThemeDark(object? sender, EventArgs e) => SetTheme(AppTheme.Dark);

	private void SetTheme(AppTheme theme)
	{
		if (Application.Current is not null)
			Application.Current.UserAppTheme = theme;
		UpdateThemeLabel();
	}

	private void UpdateThemeLabel()
	{
		ThemeLabel.Text = $"当前主题 / Current theme: {Application.Current?.UserAppTheme} (系统 / OS: {Application.Current?.PlatformAppTheme})";
	}
}
