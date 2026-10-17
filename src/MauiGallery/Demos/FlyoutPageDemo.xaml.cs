namespace MauiGallery.Demos;

public partial class FlyoutPageDemo : FlyoutPage
{
	public FlyoutPageDemo()
	{
		InitializeComponent();
		Detail = new NavigationPage(new DetailPage("A"));
		FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
	}

	private void OnOpenA(object? sender, EventArgs e)
	{
		Detail = new NavigationPage(new DetailPage("A"));
		IsPresented = false;
	}

	private void OnOpenB(object? sender, EventArgs e)
	{
		Detail = new NavigationPage(new DetailPage("B"));
		IsPresented = false;
	}

	private sealed class DetailPage : ContentPage
	{
		public DetailPage(string which)
		{
			Title = $"页面 {which} / Detail {which}";
			Content = new ScrollView
			{
				Content = new VerticalStackLayout
				{
					Padding = 20,
					Spacing = 8,
					Children =
					{
						new Label { Text = $"Detail {which}", FontFamily = "OpenSansSemibold", FontSize = 22 },
						new Label { Text = "FlyoutPage 将 Flyout（菜单）与 Detail（详情）组合为主从布局。" },
					},
				},
			};
		}
	}
}
