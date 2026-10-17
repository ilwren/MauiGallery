namespace MauiGallery;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register the routed control-info page that hosts every sample.
		Routing.RegisterRoute("controlinfo", typeof(ControlInfoPage));

		// Register full-page demos used by the navigation / windowing samples.
		Routing.RegisterRoute("demo-navigation", typeof(Demos.NavigationDemoPage));
		Routing.RegisterRoute("demo-modal", typeof(Demos.ModalDemoPage));
		Routing.RegisterRoute("demo-tabbed", typeof(Demos.TabbedPageDemo));
		Routing.RegisterRoute("demo-flyout", typeof(Demos.FlyoutPageDemo));
		Routing.RegisterRoute("demo-toolbar", typeof(Demos.ToolbarDemoPage));
		Routing.RegisterRoute("demo-titlebar", typeof(Demos.TitleBarDemoPage));
		Routing.RegisterRoute("demo-windowoverlay", typeof(Demos.WindowOverlayDemoPage));
		Routing.RegisterRoute("demo-menubar", typeof(Demos.MenuBarDemoPage));

		BuildCategoryFlyout();
	}

	private void BuildCategoryFlyout()
	{
		// Insert control categories between "All controls" and "Search".
		var insertAt = Math.Min(Items.Count, 2);
		foreach (var group in ControlInfoDataSource.Groups)
		{
			var flyoutItem = new FlyoutItem
			{
				Title = group.Title,
				Icon = "dotnet_bot.png",
				FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
			};

			flyoutItem.Items.Add(new ShellContent
			{
				Title = group.Title,
				Route = $"category-{group.UniqueId}",
				ContentTemplate = new DataTemplate(() => new SectionPage(group)),
			});

			Items.Insert(insertAt++, flyoutItem);
		}
	}
}
