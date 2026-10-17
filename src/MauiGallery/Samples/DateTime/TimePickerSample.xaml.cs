namespace MauiGallery.Samples.DateTime;

public partial class TimePickerSample : ContentView
{
	public TimePickerSample()
	{
		InitializeComponent();
	}

	private void OnTimeSelected(object? sender, TimeChangedEventArgs e) =>
		TimeOutput.Text = $"从 {e.OldTime:hh\\:mm} 变为 {e.NewTime:hh\\:mm}";
}
