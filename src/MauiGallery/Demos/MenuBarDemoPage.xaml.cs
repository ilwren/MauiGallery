namespace MauiGallery.Demos;

public partial class MenuBarDemoPage : ContentPage
{
	public MenuBarDemoPage()
	{
		InitializeComponent();
	}

	private void OnNew(object? sender, EventArgs e) => Output.Text = "新建 / New（Ctrl+N）";
	private void OnOpen(object? sender, EventArgs e) => Output.Text = "打开 / Open";
	private void OnExit(object? sender, EventArgs e) => Output.Text = "退出 / Exit（示例中不关闭应用）";
	private void OnExportPdf(object? sender, EventArgs e) => Output.Text = "导出为 PDF";
	private void OnExportPng(object? sender, EventArgs e) => Output.Text = "导出为 PNG";
	private void OnAbout(object? sender, EventArgs e) => Output.Text = "关于 / About";
}
