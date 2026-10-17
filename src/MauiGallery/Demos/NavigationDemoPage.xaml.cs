namespace MauiGallery.Demos;

public partial class NavigationDemoPage : ContentPage
{
	private static int _sequence = 1;

	public NavigationDemoPage()
	{
		InitializeComponent();
		_sequence++;
		DepthLabel.Text = $"实例编号 / Instance: {_sequence}";
	}

	private async void OnPush(object? sender, EventArgs e) =>
		await Shell.Current.Navigation.PushAsync(new NavigationDemoPage());

	private async void OnPop(object? sender, EventArgs e)
	{
		if (Shell.Current.Navigation.NavigationStack.Count > 1)
			await Shell.Current.Navigation.PopAsync();
	}

	private async void OnPushModal(object? sender, EventArgs e) =>
		await Shell.Current.Navigation.PushModalAsync(new ModalDemoPage());
}
