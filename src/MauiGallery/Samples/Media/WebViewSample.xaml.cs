namespace MauiGallery.Samples.Media;

public partial class WebViewSample : ContentView
{
	public WebViewSample()
	{
		InitializeComponent();
	}

	private void OnLoadHtml(object? sender, EventArgs e)
	{
		NavWebView.Source = new HtmlWebViewSource
		{
			Html = "<h2 style='font-family:sans-serif'>Loaded from code</h2><p>HtmlWebViewSource assigned in C#.</p>",
		};
		WebOutput.Text = "已加载 HTML 源";
	}

	private async void OnEvalJs(object? sender, EventArgs e)
	{
		var result = await NavWebView.EvaluateJavaScriptAsync("1 + 2");
		WebOutput.Text = $"EvaluateJavaScriptAsync(\"1 + 2\") = {result}";
	}

	private void OnNavigating(object? sender, WebNavigatingEventArgs e) =>
		EventOutput.Text = $"Navigating → {e.Url}";

	private void OnNavigated(object? sender, WebNavigatedEventArgs e) =>
		EventOutput.Text = $"Navigated ✓ {e.Url}（{e.Result}）";
}
