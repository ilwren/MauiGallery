namespace MauiGallery.Samples.Navigation;

public partial class FlyoutPageSample : ContentView
{
	public FlyoutPageSample()
	{
		InitializeComponent();
	}

	private async void OnOpenDemo(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-flyout");
}
