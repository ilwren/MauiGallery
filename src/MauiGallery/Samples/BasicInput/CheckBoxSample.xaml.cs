namespace MauiGallery.Samples.BasicInput;

public partial class CheckBoxSample : ContentView
{
	public CheckBoxSample()
	{
		InitializeComponent();
	}

	private void OnCheckedChanged(object? sender, CheckedChangedEventArgs e) =>
		CheckOutput.Text = e.IsChecked ? "已选中 ✓" : "未选中 ✗";

	private void OnLabelTapped(object? sender, TappedEventArgs e) =>
		CheckOutput.Text = "点击文本也应切换复选框（将 CheckBox 与 Label 组合为整体）";
}
