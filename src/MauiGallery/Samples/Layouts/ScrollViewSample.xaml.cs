namespace MauiGallery.Samples.Layouts;

public partial class ScrollViewSample : ContentView
{
	public ScrollViewSample()
	{
		InitializeComponent();
	}

	private async void OnScrollBottom(object? sender, EventArgs e) =>
		await DemoScroll.ScrollToAsync(BottomTarget, ScrollToPosition.End, true);

	private async void OnScrollTop(object? sender, EventArgs e) =>
		await DemoScroll.ScrollToAsync(0, 0, true);
}
