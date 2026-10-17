namespace MauiGallery;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		// Window is a first-class control in .NET MAUI; the gallery demonstrates
		// it (title bar, overlays, window events) on the Windowing pages.
		var window = new Window(new AppShell());
		window.Title = "MAUI Gallery";
#if WINDOWS
		window.Created += (_, _) =>
		{
			var dark = Application.Current?.RequestedTheme == AppTheme.Dark;
			if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
				WinUI.TitleBarHelper.ApplyTo(nativeWindow, dark);
		};
#endif
		return window;
	}
}
