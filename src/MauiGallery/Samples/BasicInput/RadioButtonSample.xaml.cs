namespace MauiGallery.Samples.BasicInput;

public partial class RadioButtonSample : ContentView
{
	public RadioButtonSample()
	{
		InitializeComponent();
	}

	private void OnRadioChanged(object? sender, CheckedChangedEventArgs e)
	{
		if (e.IsChecked && sender is RadioButton rb && rb.Value is string value)
			RadioOutput.Text = $"已选择 / Selected: {value}";
	}
}
