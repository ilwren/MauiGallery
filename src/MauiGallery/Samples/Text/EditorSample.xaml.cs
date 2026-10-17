namespace MauiGallery.Samples.Text;

public partial class EditorSample : ContentView
{
	public EditorSample()
	{
		InitializeComponent();
		OptionEditor.TextChanged += (_, _) => LengthOutput.Text = $"{OptionEditor.Text?.Length ?? 0} / 120";
	}

	private void OnSpellCheckChanged(object? sender, CheckedChangedEventArgs e) =>
		OptionEditor.IsSpellCheckEnabled = e.Value;

	private void OnPredictionChanged(object? sender, CheckedChangedEventArgs e) =>
		OptionEditor.IsTextPredictionEnabled = e.Value;

	private void OnTextChanged(object? sender, TextChangedEventArgs e) =>
		EditorOutput.Text = $"TextChanged: {e.NewTextValue?.Length ?? 0} 字符";

	private void OnCompleted(object? sender, EventArgs e) =>
		EditorOutput.Text = "Completed ✓";
}
