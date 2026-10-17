namespace MauiGallery.Samples.Navigation;

public partial class NavigationPageSample : ContentView
{
	public NavigationPageSample()
	{
		InitializeComponent();
	}

	private async void OnOpenDemo(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-navigation");

	private async void OnOpenModal(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-modal");
}
