using MauiGallery.Converters;

namespace MauiGallery.Samples.Styles;

public partial class StateTriggerSample : ContentView
{
	public StateTriggerSample()
	{
		InitializeComponent();
	}

	private void OnBusyToggled(object? sender, ToggledEventArgs e) { }

	private void OnCountChanged(object? sender, ValueChangedEventArgs e) =>
		CountLabel.Text = $"Count = {e.NewValue:F0}";
}
