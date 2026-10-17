namespace MauiGallery.Samples.StatusInfo;

public partial class ProgressBarSample : ContentView
{
	public ProgressBarSample()
	{
		InitializeComponent();
	}

	private async void OnStart(object? sender, EventArgs e)
	{
		for (var i = 0; i <= 20; i++)
		{
			LiveProgress.ProgressTo(i / 20.0, 120, Easing.Linear);
			ProgressOutput.Text = $"{i * 5}%";
			await Task.Delay(120);
		}
		ProgressOutput.Text = "100% ✓";
	}
}
