namespace MauiGallery.Demos;

public partial class SearchHandlerDemoPage : ContentPage
{
	private readonly string[] _fruits = ["苹果 / Apple", "香蕉 / Banana", "樱桃 / Cherry", "葡萄 / Grape", "柠檬 / Lemon", "橙子 / Orange"];

	public SearchHandlerDemoPage()
	{
		InitializeComponent();
		BindingContext = this;

		SearchBox.PropertyChanged += (s, e) =>
		{
			if (e.PropertyName == SearchBox.QueryProperty.PropertyName && !string.IsNullOrEmpty(SearchBox.Query))
			{
				var hits = _fruits.Where(f => f.Contains(SearchBox.Query, StringComparison.OrdinalIgnoreCase)).ToArray();
				ResultLabel.Text = hits.Length > 0 ? "结果 / Results: " + string.Join(", ", hits) : "无匹配 / no matches";
				SearchBox.ItemsSource = hits;
			}
		};
	}
}
