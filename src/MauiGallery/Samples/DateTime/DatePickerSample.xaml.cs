namespace MauiGallery.Samples.DateTime;

public partial class DatePickerSample : ContentView
{
	public DatePickerSample()
	{
		InitializeComponent();
	}

	private void OnBasicDateSelected(object? sender, DateChangedEventArgs e) { }

	private void OnDateSelected(object? sender, DateChangedEventArgs e) =>
		DateOutput.Text = $"从 {e.OldDate:yyyy-MM-dd} 变为 {e.NewDate:yyyy-MM-dd}";
}
