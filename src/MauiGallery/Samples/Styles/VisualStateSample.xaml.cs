namespace MauiGallery.Samples.Styles;

public partial class VisualStateSample : ContentView
{
	private bool _expanded = true;

	public VisualStateSample()
	{
		InitializeComponent();
	}

	private void OnToggle(object? sender, EventArgs e)
	{
		_expanded = !_expanded;
		VisualStateManager.GoToState(ExpandBox, _expanded ? "Expanded" : "Collapsed");
	}
}
