namespace MauiGallery.Samples.Collections;

public partial class CarouselViewSample : ContentView
{
	public CarouselViewSample()
	{
		InitializeComponent();
		var items = SampleData.CreateItems(5);
		BasicCarousel.ItemsSource = items;
		IndicatorCarousel.ItemsSource = SampleData.CreateItems(4);
		EventCarousel.ItemsSource = SampleData.CreateItems(6);
		PeekCarousel.ItemsSource = SampleData.CreateItems(5);

		EventCarousel.CurrentItemChanged += (_, e) =>
		{
			if (e.CurrentItem is SampleItem item)
				CarouselOutput.Text = $"CurrentItem: {item.Name}（Previous: {(e.PreviousItem as SampleItem)?.Name ?? "无"}）";
		};
	}
}
