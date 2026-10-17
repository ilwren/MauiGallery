namespace MauiGallery.Samples.Layouts;

public partial class BindableLayoutSample : ContentView
{
	private bool _hasData = true;

	public BindableLayoutSample()
	{
		InitializeComponent();
		SetData(_hasData);
	}

	private void OnToggleData(object? sender, EventArgs e)
	{
		_hasData = !_hasData;
		SetData(_hasData);
	}

	private void SetData(bool hasData) =>
		BindableLayout.SetItemsSource(ToggleHost, hasData
			? new[] { "项目 A / Item A", "项目 B / Item B", "项目 C / Item C" }
			: Array.Empty<string>());
}
