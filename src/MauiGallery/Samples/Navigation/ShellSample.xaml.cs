namespace MauiGallery.Samples.Navigation;

public partial class ShellSample : ContentView
{
	public ShellSample()
	{
		InitializeComponent();
	}

	private void OnOpenFlyout(object? sender, EventArgs e) =>
		Shell.Current.FlyoutIsPresented = true;

	private async void OnNavDemo(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-navigation");

	private async void OnBack(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("..");

	private async void OnGoButton(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("controlinfo?id=button");
}
