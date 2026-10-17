namespace MauiGallery.Models;

/// <summary>
/// A group of catalog items that can be used as an <c>ItemsSource</c> group for
/// <c>CollectionView</c> (each group must be enumerable and expose a title).
/// </summary>
public sealed class ItemGroup : List<ControlInfoDataItem>
{
    public ItemGroup(ControlInfoDataGroup group)
        : base(group.Items)
    {
        Title = group.Title;
        Subtitle = group.Subtitle;
        Description = group.Description;
        UniqueId = group.UniqueId;
    }

    public string UniqueId { get; }
    public string Title { get; }
    public string Subtitle { get; }
    public string Description { get; }
}
