namespace MauiGallery.Samples.Styles;

public partial class StylesSample : ContentView
{
	public StylesSample()
	{
		InitializeComponent();
	}

	private void OnApplyCss(object? sender, EventArgs e)
	{
		var sheet = Microsoft.Maui.Controls.StyleSheets.StyleSheet.FromString("label { color: #D600AA; font-style: italic; }");
		Resources.Add(sheet);
		CssTarget.Text = "已套用 CSS（斜体紫色）/ CSS applied";
	}
}
