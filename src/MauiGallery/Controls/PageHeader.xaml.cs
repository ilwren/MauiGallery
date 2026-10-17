using Microsoft.Maui.ApplicationModel;

namespace MauiGallery.Controls;

/// <summary>Header shown on every control page: title, subtitle, description, API and doc links.</summary>
public partial class PageHeader : ContentView
{
	public PageHeader()
	{
		InitializeComponent();
	}

	private async void OnDocLinkClicked(object? sender, EventArgs e)
	{
		if (sender is Button { BindingContext: ControlInfoDocLink link } && Uri.TryCreate(link.Uri, UriKind.Absolute, out var uri))
			await Browser.OpenAsync(uri, BrowserLaunchMode.SystemBrowser);
	}
}
