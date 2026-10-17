namespace MauiGallery.Samples.BasicInput;

public partial class PickerSample : ContentView
{
	public PickerSample()
	{
		InitializeComponent();
		FruitPicker.ItemsSource = new[] { "苹果 / Apple", "香蕉 / Banana", "橙子 / Orange", "葡萄 / Grape", "西瓜 / Watermelon" };
		CountryPicker.ItemsSource = new[] { "中国 / China", "美国 / USA", "日本 / Japan", "德国 / Germany", "巴西 / Brazil" };
		StylePicker.ItemsSource = new[] { "宋体 / Song", "黑体 / Hei", "楷体 / Kai" };
	}

	private void OnSelectedIndexChanged(object? sender, EventArgs e) =>
		PickerOutput.Text = FruitPicker.SelectedItem is { } item ? $"已选择 / Selected: {item}" : "尚未选择";

	private void OnCountryChanged(object? sender, EventArgs e) =>
		CountryOutput.Text = $"SelectedIndex = {CountryPicker.SelectedIndex}, SelectedItem = {CountryPicker.SelectedItem}";
}
