namespace MauiGallery.Samples.Windowing;

public partial class TitleBarSample : ContentView
{
	public TitleBarSample()
	{
		InitializeComponent();
	}

	private async void OnOpenDemo(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-titlebar");
}
