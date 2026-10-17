namespace MauiGallery.Samples.Dialogs;

public partial class AlertsSample : ContentView, IControlSample
{
	private ContentPage? _host;

	public AlertsSample()
	{
		InitializeComponent();
	}

	public void Initialize(ContentPage hostPage, ControlInfoDataItem item) => _host = hostPage;

	private async void OnAlert(object? sender, EventArgs e)
	{
		if (_host is null) return;
		var ok = await _host.DisplayAlertAsync("退出 / Quit", "确定要退出吗？/ Are you sure?", "确定 / Yes", "取消 / No");
		AlertOutput.Text = ok ? "用户选择：确定 / Yes" : "用户选择：取消 / No";
	}

	private async void OnMessage(object? sender, EventArgs e)
	{
		if (_host is null) return;
		await _host.DisplayAlertAsync("提示 / Info", "这是一条提示消息。/ This is a message.", "好的 / OK");
		AlertOutput.Text = "消息框已关闭 / Closed";
	}

	private async void OnActionSheet(object? sender, EventArgs e)
	{
		if (_host is null) return;
		var action = await _host.DisplayActionSheetAsync("分享到 / Share to", "取消 / Cancel", "删除 / Delete",
			"微信 / WeChat", "微博 / Weibo", "邮件 / Email");
		SheetOutput.Text = $"用户选择 / Selected: {action}";
	}

	private async void OnPrompt(object? sender, EventArgs e)
	{
		if (_host is null) return;
		var text = await _host.DisplayPromptAsync("重命名 / Rename", "输入新名称 / Enter a new name:",
			"确定 / OK", "取消 / Cancel", "名称 / Name", 20);
		PromptOutput.Text = text is null ? "用户取消 / Cancelled" : $"输入内容 / Input: {text}";
	}
}
