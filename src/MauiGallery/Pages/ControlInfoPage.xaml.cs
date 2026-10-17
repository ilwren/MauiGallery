namespace MauiGallery.Pages;

/// <summary>
/// The detail page for one control. The control catalog id arrives through the
/// Shell query string ("controlinfo?id=button"); the matching sample view is
/// created by <see cref="SampleRegistry"/> and hosted below the page header.
/// </summary>
public partial class ControlInfoPage : ContentPage, IQueryAttributable
{
	private ControlInfoDataItem? _item;

	public ControlInfoPage()
	{
		InitializeComponent();
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		if (!query.TryGetValue("id", out var raw) || raw is not string id)
			return;

		var item = ControlInfoDataSource.FindItem(id);
		if (item is null)
			return;

		_item = item;
		BindingContext = item;
		Title = item.Title;

		var sample = SampleRegistry.Create(item.UniqueId);
		SampleHost.Content = sample;
		if (sample is IControlSample s)
			s.Initialize(this, item);
	}
}
