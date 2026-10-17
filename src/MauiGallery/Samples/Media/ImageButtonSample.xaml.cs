namespace MauiGallery.Samples.Media;

public partial class ImageButtonSample : ContentView
{
	public ImageButtonSample()
	{
		InitializeComponent();
	}

	private void OnClicked(object? sender, EventArgs e) => IbOutput.Text = "Clicked ✓";
	private void OnPressed(object? sender, EventArgs e) => IbOutput.Text = "Pressed…";
	private void OnReleased(object? sender, EventArgs e) => IbOutput.Text = "Released";
}
