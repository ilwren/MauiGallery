using System.Text.Json.Serialization;

namespace MauiGallery.Models;

/// <summary>
/// Data model for the control catalog (Data/ControlInfoData.json).
/// Inspired by the <c>ControlInfoData</c> model used by the WinUI 3 Gallery.
/// </summary>
public sealed class ControlInfoCatalog
{
    [JsonPropertyName("groups")]
    public List<ControlInfoDataGroup> Groups { get; set; } = new();
}

/// <summary>A control category ("Basic input", "Collections", ...).</summary>
public sealed class ControlInfoDataGroup
{
    [JsonPropertyName("uniqueId")]
    public string UniqueId { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("iconGlyph")]
    public string IconGlyph { get; set; } = string.Empty;

    [JsonPropertyName("items")]
    public List<ControlInfoDataItem> Items { get; set; } = new();

    public override string ToString() => Title;
}

/// <summary>A single control showcased by the gallery.</summary>
public sealed class ControlInfoDataItem
{
    [JsonPropertyName("uniqueId")]
    public string UniqueId { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("apiNamespace")]
    public string ApiNamespace { get; set; } = string.Empty;

    [JsonPropertyName("baseClasses")]
    public List<string> BaseClasses { get; set; } = new();

    [JsonPropertyName("docs")]
    public List<ControlInfoDocLink> Docs { get; set; } = new();

    [JsonPropertyName("isNew")]
    public bool IsNew { get; set; }

    [JsonPropertyName("isLegacy")]
    public bool IsLegacy { get; set; }

    /// <summary>Search haystack: title + subtitle + description + tags.</summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    public string BadgeText => IsNew ? "NEW" : IsLegacy ? "LEGACY" : string.Empty;

    public bool HasBadge => BadgeText.Length > 0;

    /// <summary>Base classes flattened for display ("View → TemplatedView → ...").</summary>
    public string BaseClassesText =>
        BaseClasses.Count == 0 ? string.Empty : "继承自 / Inherits: " + string.Join(" → ", BaseClasses);

    public override string ToString() => Title;
}

/// <summary>A documentation link for a control.</summary>
public sealed class ControlInfoDocLink
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;
}
