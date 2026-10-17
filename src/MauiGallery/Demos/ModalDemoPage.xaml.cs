namespace MauiGallery.Demos;

public partial class ModalDemoPage : ContentPage
{
	public ModalDemoPage()
	{
		InitializeComponent();
	}

	private async void OnClose(object? sender, EventArgs e)
	{
		if (Shell.Current.Navigation.ModalStack.Count > 0)
			await Shell.Current.Navigation.PopModalAsync();
		else
			await Shell.Current.GoToAsync("..");
	}
}
