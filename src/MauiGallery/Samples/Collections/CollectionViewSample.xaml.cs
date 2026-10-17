namespace MauiGallery.Samples.Collections;

public partial class CollectionViewSample : ContentView
{
	public CollectionViewSample()
	{
		InitializeComponent();

		var items = SampleData.CreateItems(12);
		ListCollectionView.ItemsSource = items;
		GridCollectionView.ItemsSource = items;
		HCollectionView.ItemsSource = items;
		SelectionCollectionView.ItemsSource = SampleData.CreateItems(8);
		HeaderCollectionView.ItemsSource = items;
		GroupCollectionView.ItemsSource = SampleData.CreateGroups();
		// EmptyCollectionView intentionally has no items.
	}

	private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e) =>
		SelectionOutput.Text = e.CurrentSelection.FirstOrDefault() is SampleItem item
			? $"已选择 / Selected: {item.Name}"
			: "未选择任何项 / Nothing selected";
}

/// <summary>Small demo model used by several collection samples.</summary>
public sealed record SampleItem(string Name, string Detail);

public sealed record SampleGroup(string Title, List<SampleItem> Items) : List<SampleItem>(Items);

public static class SampleData
{
	public static List<SampleItem> CreateItems(int count) =>
		Enumerable.Range(1, count)
			.Select(i => new SampleItem($"项目 {i} / Item {i}", $"描述文字 {i} · Detail {i}"))
			.ToList();

	public static List<SampleGroup> CreateGroups() => new()
	{
		new SampleGroup("水果 / Fruits", new()
		{
			new("苹果 / Apple", "红色"), new("香蕉 / Banana", "黄色"), new("橙子 / Orange", "橙色"),
		}),
		new SampleGroup("蔬菜 / Vegetables", new()
		{
			new("胡萝卜 / Carrot", "橙色"), new("菠菜 / Spinach", "绿色"),
		}),
		new SampleGroup("饮料 / Drinks", new()
		{
			new("茶 / Tea", "无糖"), new("咖啡 / Coffee", "拿铁"),
		}),
	};
}
