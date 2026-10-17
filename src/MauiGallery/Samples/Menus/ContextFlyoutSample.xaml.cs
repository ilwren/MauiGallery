namespace MauiGallery.Samples.Menus;

public partial class ContextFlyoutSample : ContentView
{
	public ContextFlyoutSample()
	{
		InitializeComponent();
	}

	private void OnCopy(object? sender, EventArgs e) => CtxOutput.Text = "复制 / Copy";
	private void OnPaste(object? sender, EventArgs e) => CtxOutput.Text = "粘贴 / Paste";
	private void OnProperties(object? sender, EventArgs e) => CtxOutput.Text = "属性 / Properties";
	private void OnExportPdf(object? sender, EventArgs e) => CtxOutput.Text = "导出为 PDF";
	private void OnExportPng(object? sender, EventArgs e) => CtxOutput.Text = "导出为 PNG";
	private void OnDelete(object? sender, EventArgs e) => CtxOutput.Text = "删除 / Delete";
	private void OnRed(object? sender, EventArgs e) => CtxOutput.Text = "红色 / Red";
	private void OnPurple(object? sender, EventArgs e) => CtxOutput.Text = "紫色 / Purple";
}
