namespace MauiGallery.Samples.StatusInfo;

public partial class ActivityIndicatorSample : ContentView
{
	public ActivityIndicatorSample()
	{
		InitializeComponent();
	}

	private async void OnSimulate(object? sender, EventArgs e)
	{
		BusyIndicator.IsRunning = true;
		BusyOutput.Text = "处理中… / Working…";
		await Task.Delay(1500);
		BusyIndicator.IsRunning = false;
		BusyOutput.Text = "完成 ✓ / Done";
	}
}
