namespace MauiGallery.Samples.BasicInput;

public partial class ButtonSample : ContentView
{
	private int _count;

	public ButtonSample()
	{
		InitializeComponent();

		// ButtonContentLayout is a nested type — set it in code
		ImageButton.ContentLayout = new Button.ButtonContentLayout(
			Button.ButtonContentLayout.ImagePosition.Left, 8);
	}

	private void OnCountClicked(object? sender, EventArgs e)
	{
		_count++;
		EventOutput.Text = $"Clicked × {_count}";
	}

	private void OnPressed(object? sender, EventArgs e) =>
		EventOutput.Text = $"Pressed（第 {_count + 1} 次按下）";

	private void OnReleased(object? sender, EventArgs e) =>
		EventOutput.Text = $"Released（第 {_count} 次完成点击）";
}
