namespace MauiGallery.Samples.Media;

public partial class ImageSample : ContentView
{
	private bool _swap;

	public ImageSample()
	{
		InitializeComponent();
	}

	private void OnSwapSource(object? sender, EventArgs e)
	{
		_swap = !_swap;
		ImageOutput.Text = _swap
			? "源 A（本地资源）/ local resource"
			: "源 B（可换成 UriImageSource）/ uri source";
	}
}
