namespace MauiGallery.Helpers;

/// <summary>
/// Optional interface implemented by sample views that need the hosting page
/// (e.g. to push full-page demos or display alerts).
/// </summary>
public interface IControlSample
{
	void Initialize(ContentPage hostPage, ControlInfoDataItem item);
}
