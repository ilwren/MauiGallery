namespace MauiGallery.Samples.Collections;

public partial class IndicatorViewSample : ContentView
{
	public IndicatorViewSample()
	{
		InitializeComponent();
		IndicatorCarousel.ItemsSource = SampleData.CreateItems(4);
	}

	private void OnPrev(object? sender, EventArgs e) =>
		BasicIndicator.Position = Math.Max(0, BasicIndicator.Position - 1);

	private void OnNext(object? sender, EventArgs e) =>
		BasicIndicator.Position = Math.Min(BasicIndicator.Count - 1, BasicIndicator.Position + 1);
}
