using System.Globalization;

namespace MauiGallery.Converters;

/// <summary>Returns <see cref="TrueValue"/> or <see cref="FalseValue"/> depending on the bound boolean.</summary>
public sealed class BoolToObjectConverter : IValueConverter
{
    public object? TrueValue { get; set; }
    public object? FalseValue { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? TrueValue : FalseValue;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value?.Equals(TrueValue) ?? false;
}

/// <summary>Inverts a boolean value.</summary>
public sealed class InvertedBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b ? !b : value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b ? !b : value;
}
