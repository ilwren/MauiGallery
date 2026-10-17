namespace MauiGallery.Samples.Styles;

public partial class DataTemplateSample : ContentView
{
	public DataTemplateSample()
	{
		InitializeComponent();
		var items = new[] { "项目 A / Item A", "项目 B / Item B", "项目 C / Item C", "项目 D / Item D" };
		BindableLayout.SetItemsSource(TemplateHost, items);
		BindableLayout.SetItemsSource(SelectorHost, items);
	}
}

/// <summary>Picks alternating templates based on the item's index parity (demo).</summary>
public sealed class AlternatingTemplateSelector : DataTemplateSelector
{
	public DataTemplate? EvenTemplate { get; set; }
	public DataTemplate? OddTemplate { get; set; }

	private int _index;

	protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
	{
		var template = _index % 2 == 0 ? EvenTemplate : OddTemplate;
		_index++;
		return template ?? base.OnSelectTemplate(item, container);
	}
}

/// <summary>Combines multiple bound values into one (here: the smaller width).</summary>
public sealed class MinValueConverter : IMultiValueConverter
{
	public object? Convert(object[] values, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
	{
		var nums = values.OfType<double>().ToArray();
		return nums.Length > 0 ? nums.Min() : 100d;
	}

	public object[] ConvertBack(object? value, Type[] targetTypes, object? parameter, System.Globalization.CultureInfo culture)
		=> throw new NotSupportedException();
}
