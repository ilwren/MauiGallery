namespace MauiGallery.Pages;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (SearchBox.Text?.Length > 0)
			ResultsCollection.ItemsSource = ControlInfoDataSource.Search(SearchBox.Text).ToList();
	}

	private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
	{
		ResultsCollection.ItemsSource = ControlInfoDataSource.Search(e.NewTextValue).ToList();
	}

	private async void OnItemTapped(object? sender, TappedEventArgs e)
	{
		if ((sender as BindableObject)?.BindingContext is ControlInfoDataItem item)
			await Shell.Current.GoToAsync($"controlinfo?id={item.UniqueId}");
	}
}
