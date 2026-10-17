namespace MauiGallery.Demos;

public partial class ToolbarDemoPage : ContentPage
{
	public ToolbarDemoPage()
	{
		InitializeComponent();
	}

	private void OnSave(object? sender, EventArgs e) => Output.Text = "保存 / Save 被点击";
	private void OnShare(object? sender, EventArgs e) => Output.Text = "分享 / Share 被点击";
	private void OnMore(object? sender, EventArgs e) => Output.Text = "更多 / More（Secondary 溢出菜单）";
	private void OnDelete(object? sender, EventArgs e) => Output.Text = "删除 / Delete（Secondary 溢出菜单）";
}
