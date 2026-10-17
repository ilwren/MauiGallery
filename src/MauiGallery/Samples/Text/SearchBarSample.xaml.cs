namespace MauiGallery.Samples.Text;

public partial class SearchBarSample : ContentView
{
	private static readonly string[] _items =
	{
		"Button 按钮", "BoxView 色块", "Border 边框", "CarouselView 轮播", "CheckBox 复选框",
		"CollectionView 集合", "DatePicker 日期", "Editor 编辑器", "Entry 输入框", "Grid 网格",
		"Image 图像", "Label 标签", "Picker 选择器", "ProgressBar 进度条", "Slider 滑块",
	};

	public SearchBarSample()
	{
		InitializeComponent();
		// SearchCommand is an ICommand (MVVM); here a simple Command<string> demonstrates the pattern.
		CommandSearchBar.SearchCommand = new Command<string>(parameter =>
			SearchOutput.Text = $"SearchCommand 执行，参数：{parameter}");
	}

	private void OnTextChanged(object? sender, TextChangedEventArgs e) =>
		SearchOutput.Text = $"输入中 / Typing: \"{e.NewTextValue}\"";

	private void OnSearchPressed(object? sender, EventArgs e)
	{
		var query = EventSearchBar.Text?.Trim() ?? string.Empty;
		var hits = _items.Where(i => i.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
		SearchOutput.Text = $"搜索 \"{query}\"：{hits.Count} 个结果";
		ResultList.Children.Clear();
		foreach (var hit in hits.Take(6))
			ResultList.Children.Add(new Label { Text = "• " + hit, FontSize = 13 });
	}
}
