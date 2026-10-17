namespace MauiGallery.Samples.Text;

public partial class EntrySample : ContentView
{
	public EntrySample()
	{
		InitializeComponent();
	}

	private void OnTextChanged(object? sender, TextChangedEventArgs e) =>
		EntryOutput.Text = $"TextChanged: \"{e.NewTextValue}\" ({e.NewTextValue?.Length ?? 0} 字符)";

	private void OnCompleted(object? sender, EventArgs e) =>
		EntryOutput.Text = "Completed ✓（回车 / 完成键被按下）";
}
