using System.Reflection;
using System.Text.Json;

namespace MauiGallery.Helpers;

/// <summary>
/// Loads and caches the control catalog (Data/ControlInfoData.json), which is embedded
/// in the assembly as an embedded resource.
/// </summary>
public static class ControlInfoDataSource
{
    private static ControlInfoCatalog? _catalog;

    public static ControlInfoCatalog Catalog
    {
        get
        {
            if (_catalog is null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "MauiGallery.Data.ControlInfoData.json";
                using var stream = assembly.GetManifestResourceStream(resourceName)
                    ?? throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
                _catalog = JsonSerializer.Deserialize<ControlInfoCatalog>(stream)
                    ?? throw new InvalidOperationException("Control catalog JSON is invalid.");
            }

            return _catalog;
        }
    }

    public static IReadOnlyList<ControlInfoDataGroup> Groups => Catalog.Groups;

    public static IEnumerable<ControlInfoDataItem> AllItems =>
        Catalog.Groups.SelectMany(g => g.Items);

    public static ControlInfoDataItem? FindItem(string? uniqueId) =>
        string.IsNullOrEmpty(uniqueId) ? null : AllItems.FirstOrDefault(i => i.UniqueId == uniqueId);

    public static ControlInfoDataGroup? FindGroup(string? uniqueId) =>
        string.IsNullOrEmpty(uniqueId) ? null : Groups.FirstOrDefault(g => g.UniqueId == uniqueId);

    public static ControlInfoDataGroup? GroupOf(ControlInfoDataItem item) =>
        Groups.FirstOrDefault(g => g.Items.Any(i => i.UniqueId == item.UniqueId));

    public static IEnumerable<ControlInfoDataItem> Search(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Enumerable.Empty<ControlInfoDataItem>();

        var terms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return AllItems.Where(item =>
            terms.All(term =>
                item.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                item.Subtitle.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                item.Description.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                item.ApiNamespace.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                item.Tags.Any(t => t.Contains(term, StringComparison.OrdinalIgnoreCase))));
    }
}
