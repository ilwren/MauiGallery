namespace MauiGallery.Pages;

/// <summary>Lists the controls that belong to one control category.</summary>
public partial class SectionPage : ContentPage, IQueryAttributable
{
	private ControlInfoDataGroup _group;
	private IReadOnlyList<ControlInfoDataItem> _allItems = Array.Empty<ControlInfoDataItem>();

	public SectionPage()
	{
		// Design-time / query-based creation: resolved in ApplyQueryAttributes.
		InitializeComponent();
		_group = ControlInfoDataSource.Groups[0];
		LoadGroup(_group);
	}

	public SectionPage(ControlInfoDataGroup group)
	{
		InitializeComponent();
		_group = group;
		LoadGroup(group);
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		if (query.TryGetValue("id", out var id) &&
			ControlInfoDataSource.FindGroup(id?.ToString()) is { } group)
		{
			_group = group;
			LoadGroup(group);
		}
	}

	private void LoadGroup(ControlInfoDataGroup group)
	{
		BindingContext = group;
		Title = group.Title;
		_allItems = group.Items;
		ItemsCollection.ItemsSource = _allItems;
	}

	private void OnFilterChanged(object? sender, TextChangedEventArgs e)
	{
		var query = e.NewTextValue?.Trim() ?? string.Empty;
		ItemsCollection.ItemsSource = query.Length == 0
			? _allItems
			: _allItems.Where(i =>
				i.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
				i.Subtitle.Contains(query, StringComparison.OrdinalIgnoreCase) ||
				i.Tags.Any(t => t.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();
	}

	private async void OnItemTapped(object? sender, TappedEventArgs e)
	{
		if ((sender as BindableObject)?.BindingContext is ControlInfoDataItem item)
			await Shell.Current.GoToAsync($"controlinfo?id={item.UniqueId}");
	}
}
