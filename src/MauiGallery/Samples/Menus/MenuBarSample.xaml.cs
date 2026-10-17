namespace MauiGallery.Samples.Menus;

public partial class MenuBarSample : ContentView
{
	public MenuBarSample()
	{
		InitializeComponent();
	}

	private async void OnOpenDemo(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-menubar");
}
