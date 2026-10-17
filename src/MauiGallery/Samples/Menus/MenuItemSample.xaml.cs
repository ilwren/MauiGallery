namespace MauiGallery.Samples.Menus;

public partial class MenuItemSample : ContentView
{
	public MenuItemSample()
	{
		InitializeComponent();
	}

	private void OnOpenFlyout(object? sender, EventArgs e) =>
		Shell.Current.FlyoutIsPresented = true;
}
