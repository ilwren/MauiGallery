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
		return window;
	}
}
