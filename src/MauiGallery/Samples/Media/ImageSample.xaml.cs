namespace MauiGallery.Samples.Media;

public partial class ImageSample : ContentView
{
	public ImageSample()
	{
		InitializeComponent();
	}

	private void OnLoadingStarted(object? sender, EventArgs e) =>
		ImageOutput.Text = "LoadingStarted…";

	private void OnLoadingCompleted(object? sender, EventArgs e) =>
		ImageOutput.Text = "LoadingCompleted ✓";

	private void OnLoadingFailed(object? sender, EventArgs e) =>
		ImageOutput.Text = "LoadingFailed ✗";
}
