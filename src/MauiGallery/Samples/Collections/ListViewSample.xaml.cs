namespace MauiGallery.Samples.Collections;

public partial class ListViewSample : ContentView
{
	public ListViewSample()
	{
		InitializeComponent();
		BasicListView.ItemsSource = SampleData.CreateItems(10);
		GroupListView.ItemsSource = SampleData.CreateGroups();
		ContextListView.ItemsSource = SampleData.CreateItems(6);
		RefreshListView.ItemsSource = SampleData.CreateItems(6);
	}

	private void OnItemTapped(object? sender, ItemTappedEventArgs e) { }

	private void OnFavorite(object? sender, EventArgs e) =>
		ContextOutput.Text = $"收藏 / Favorite: {(sender as MenuItem)?.BindingContext}";

	private void OnDeleteRow(object? sender, EventArgs e) =>
		ContextOutput.Text = $"删除请求 / Delete: {(sender as MenuItem)?.BindingContext}（示例中不真正删除）";

	private void OnRefresh()
	{
		RefreshListView.ItemsSource = SampleData.CreateItems(6).Select(i => i with { Detail = $"已刷新 {System.DateTime.Now:HH:mm:ss}" }).ToList();
		RefreshListView.IsRefreshing = false;
	}
}
