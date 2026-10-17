namespace MauiGallery.Samples.BasicInput;

public partial class StepperSample : ContentView
{
	public StepperSample()
	{
		InitializeComponent();
	}

	private void OnBasicChanged(object? sender, ValueChangedEventArgs e) =>
		BasicOutput.Text = $"Value = {e.NewValue:F0}";

	private void OnDecimalChanged(object? sender, ValueChangedEventArgs e) =>
		DecimalOutput.Text = $"Value = {e.NewValue:0.00}";

	private void OnRoomChanged(object? sender, ValueChangedEventArgs e) =>
		RoomLabel.Text = $"{e.NewValue:F0}";
}
