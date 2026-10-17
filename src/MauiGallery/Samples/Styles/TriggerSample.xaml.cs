namespace MauiGallery.Samples.Styles;

public partial class TriggerSample : ContentView
{
	public TriggerSample()
	{
		InitializeComponent();
	}

	private void OnTextChanged(object? sender, TextChangedEventArgs e) =>
		TriggerLabel.Text = string.IsNullOrEmpty(e.NewTextValue) ? "输入内容后我变色" : e.NewTextValue;

	private void OnBusyChanged(object? sender, CheckedChangedEventArgs e) { }

	private void OnWordChanged(object? sender, TextChangedEventArgs e) { }
}
