namespace MauiGallery.Pages;

public partial class AllControlsPage : ContentPage
{
	public AllControlsPage()
	{
		InitializeComponent();
		ControlsCollection.ItemsSource = ControlInfoDataSource.Groups.Select(g => new ItemGroup(g)).ToList();
	}

	private async void OnItemTapped(object? sender, TappedEventArgs e)
	{
		if ((sender as BindableObject)?.BindingContext is ControlInfoDataItem item)
			await Shell.Current.GoToAsync($"controlinfo?id={item.UniqueId}");
	}
}
