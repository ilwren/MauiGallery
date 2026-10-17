namespace MauiGallery.Samples.BasicInput;

public partial class SwitchSample : ContentView
{
	public SwitchSample()
	{
		InitializeComponent();
	}

	private void OnToggled(object? sender, ToggledEventArgs e) =>
		SwitchOutput.Text = e.Value ? "当前：开 / On" : "当前：关 / Off";
}
