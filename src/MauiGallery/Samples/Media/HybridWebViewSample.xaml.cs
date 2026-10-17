namespace MauiGallery.Samples.Media;

public partial class HybridWebViewSample : ContentView
{
	public HybridWebViewSample()
	{
		InitializeComponent();
		HybridView.RawMessageReceived += OnRawMessageReceived;
		// Register this object as the target for window.HybridWebView.InvokeDotNet(...) calls.
		HybridView.SetInvokeJavaScriptTarget(this);
	}

	private void OnRawMessageReceived(object? sender, HybridWebViewRawMessageReceivedEventArgs e) =>
		HybridOutput.Text = $"RawMessage: {e.Message}";

	// Invoked from JavaScript via window.HybridWebView.InvokeDotNet('Greet', { name: 'MAUI' })
	public string Greet(string name)
	{
		HybridOutput.Text = $"InvokeDotNet: Greet(\"{name}\")";
		return $"Hello {name} from C#!";
	}

	// Invoked from JavaScript via window.HybridWebView.InvokeDotNet('Add', [2, 40])
	public int Add(int a, int b)
	{
		HybridOutput.Text = $"InvokeDotNet: Add({a}, {b})";
		return a + b;
	}

	private async void OnInvokeJs(object? sender, EventArgs e)
	{
		try
		{
			var result = await HybridView.EvaluateJavaScriptAsync("greetFromCsharp('MAUI')");
			HybridOutput.Text = $"JS 返回 / returned: {result}";
		}
		catch (Exception ex)
		{
			HybridOutput.Text = $"调用失败 / failed: {ex.Message}";
		}
	}
}
