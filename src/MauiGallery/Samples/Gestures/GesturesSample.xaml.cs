namespace MauiGallery.Samples.Gestures;

public partial class GesturesSample : ContentView
{
	private double _panX;
	private double _panY;

	public GesturesSample()
	{
		InitializeComponent();
	}

	private void OnTapped(object? sender, TappedEventArgs e) => TapOutput.Text = "单击 / Single tap ✓";
	private void OnDoubleTapped(object? sender, TappedEventArgs e) => TapOutput.Text = "双击 / Double tap ✓✓";

	private void OnSwipedLeft(object? sender, SwipedEventArgs e) => SwipeOutput.Text = "向左滑动 / Swiped Left ←";
	private void OnSwipedRight(object? sender, SwipedEventArgs e) => SwipeOutput.Text = "向右滑动 / Swiped Right →";

	private void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
	{
		switch (e.StatusType)
		{
			case GestureStatus.Started:
				PanOutput.Text = "Started";
				break;
			case GestureStatus.Running:
				_panX = e.TotalX;
				_panY = e.TotalY;
				PanBox.TranslationX = _panX;
				PanBox.TranslationY = _panY;
				PanOutput.Text = $"Running: X={_panX:F0}, Y={_panY:F0}";
				break;
			case GestureStatus.Completed:
				PanOutput.Text = "Completed ✓";
				break;
			case GestureStatus.Canceled:
				PanOutput.Text = "Canceled";
				break;
		}
	}

	private async void OnPinchUpdated(object? sender, PinchGestureUpdatedEventArgs e)
	{
		switch (e.Status)
		{
			case GestureStatus.Running:
				PinchOutput.Text = $"Scale: {e.Scale:F2}";
				await PinchBox.ScaleTo(PinchBox.Scale * e.Scale, 50);
				break;
			case GestureStatus.Completed:
				PinchOutput.Text = $"完成 / Completed，最终缩放 {PinchBox.Scale:F2}";
				break;
		}
	}

	private void OnClickGesture(object? sender, ClickEventArgs e) =>
		ClickGestureOutput.Text = "ClickGestureRecognizer 触发 / fired (accessible click)";
}
