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

/// <summary>Custom TriggerAction used by EventTrigger: shakes the target on focus.</summary>
public sealed class ShakeAction : TriggerAction<VisualElement>
{
	protected override async void Invoke(VisualElement sender)
	{
		for (var i = 0; i < 4; i++)
		{
			await sender.TranslateTo(-6, 0, 40);
			await sender.TranslateTo(6, 0, 40);
		}
		await sender.TranslateTo(0, 0, 40);
	}
}
