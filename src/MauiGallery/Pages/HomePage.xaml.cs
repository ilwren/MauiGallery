namespace MauiGallery.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = new RootGroups();
	}

	private async void OnBrowseAllClicked(object? sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//allcontrols");
	}

	private async void OnCategoryTapped(object? sender, TappedEventArgs e)
	{
		if ((sender as BindableObject)?.BindingContext is ControlInfoDataGroup group)
			await Shell.Current.GoToAsync($"//category-{group.UniqueId}");
	}

	/// <summary>The catalog groups, presented as the page BindingContext (for the FlexLayout).</summary>
	private sealed class RootGroups : List<ControlInfoDataGroup>
	{
		public RootGroups()
			: base(ControlInfoDataSource.Groups)
		{
		}
	}
}
