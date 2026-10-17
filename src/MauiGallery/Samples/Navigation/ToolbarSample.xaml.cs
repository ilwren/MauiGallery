namespace MauiGallery.Samples.Navigation;

public partial class ToolbarSample : ContentView
{
	public ToolbarSample()
	{
		InitializeComponent();
	}

	private async void OnOpenDemo(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-toolbar");
}
