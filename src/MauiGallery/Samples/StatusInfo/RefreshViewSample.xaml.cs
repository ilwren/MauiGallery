namespace MauiGallery.Samples.StatusInfo;

public partial class RefreshViewSample : ContentView
{
	private int _refreshCount;

	public RefreshViewSample()
	{
		InitializeComponent();
		RefreshList.ItemsSource = LoadItems();
	}

	private IEnumerable<string> LoadItems() =>
		Enumerable.Range(1, 10).Select(i => $"条目 {i} / Item {i}（第 {_refreshCount} 次刷新）");

	private async void OnRefreshing(object? sender, EventArgs e)
	{
		await Task.Delay(1200);
		_refreshCount++;
		RefreshContent.Children.Insert(0, new Label { Text = $"✨ 刷新于 {System.DateTime.Now:HH:mm:ss}" });
		DemoRefresh.IsRefreshing = false;
	}

	private async void OnListRefreshing(object? sender, EventArgs e)
	{
		if (sender is RefreshView rv)
		{
			await Task.Delay(1200);
			_refreshCount++;
			RefreshList.ItemsSource = LoadItems();
			rv.IsRefreshing = false;
		}
	}
}
