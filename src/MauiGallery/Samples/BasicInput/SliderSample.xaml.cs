namespace MauiGallery.Samples.BasicInput;

public partial class SliderSample : ContentView
{
	public SliderSample()
	{
		InitializeComponent();
	}

	private void OnBasicChanged(object? sender, ValueChangedEventArgs e) =>
		BasicOutput.Text = $"Value = {e.NewValue:F0}";

	private void OnValueChanged(object? sender, ValueChangedEventArgs e) =>
		EventOutput.Text = $"ValueChanged: {e.NewValue:F2}";

	private void OnDragCompleted(object? sender, EventArgs e) =>
		EventOutput.Text = $"拖动结束，最终值已提交 / DragCompleted";

	private void OnFontSizeChanged(object? sender, ValueChangedEventArgs e) =>
		DynamicLabel.FontSize = e.NewValue;
}
