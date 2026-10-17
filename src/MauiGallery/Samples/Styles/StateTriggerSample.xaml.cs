using MauiGallery.Converters;

namespace MauiGallery.Samples.Styles;

public partial class StateTriggerSample : ContentView
{
	public StateTriggerSample()
	{
		InitializeComponent();
	}

	private void OnBusyToggled(object? sender, ToggledEventArgs e) { }

	private void OnCountChanged(object? sender, ValueChangedEventArgs e)
	{
		CountLabel.Text = $"Count = {e.NewValue:F0}";
		CountBar.Color = e.NewValue >= 10
			? Color.FromArgb("#512BD4")
			: Color.FromArgb("#C8C8C8");
	}
}
