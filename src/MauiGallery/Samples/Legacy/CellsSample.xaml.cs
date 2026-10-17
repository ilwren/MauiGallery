namespace MauiGallery.Samples.Legacy;

public partial class CellsSample : ContentView
{
	public CellsSample()
	{
		InitializeComponent();
	}

	private void OnSwitchChanged(object? sender, ToggledEventArgs e) =>
		CellOutput.Text = $"SwitchCell → {e.Value}";

	private void OnCopy(object? sender, EventArgs e) =>
		CellOutput.Text = "复制 / Copy";

	private void OnDelete(object? sender, EventArgs e) =>
		CellOutput.Text = "删除请求 / Delete requested";
}
