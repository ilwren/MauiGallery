namespace MauiGallery.Samples.Navigation;

public partial class TabbedPageSample : ContentView
{
	public TabbedPageSample()
	{
		InitializeComponent();
	}

	private async void OnOpenDemo(object? sender, EventArgs e) =>
		await Shell.Current.GoToAsync("demo-tabbed");
}
