namespace MauiGallery.Samples.Styles;

public partial class BehaviorsSample : ContentView
{
	public BehaviorsSample()
	{
		InitializeComponent();
	}
}

/// <summary>Behavior that strips non-digit characters from an Entry.</summary>
public sealed class NumericValidationBehavior : Behavior<Entry>
{
	protected override void OnAttachedTo(Entry entry)
	{
		entry.TextChanged += OnTextChanged;
		base.OnAttachedTo(entry);
	}

	protected override void OnDetachingFrom(Entry entry)
	{
		entry.TextChanged -= OnTextChanged;
		base.OnDetachingFrom(entry);
	}

	private static void OnTextChanged(object? sender, TextChangedEventArgs e)
	{
		if (sender is not Entry entry)
			return;

		var digits = new string((e.NewTextValue ?? string.Empty).Where(char.IsDigit).ToArray());
		if (digits != e.NewTextValue)
			entry.Text = digits;
	}
}

/// <summary>Behavior with a bindable property: caps the input length.</summary>
public sealed class MaxLengthBehavior : Behavior<Entry>
{
	public static readonly BindableProperty MaxLengthProperty =
		BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaxLengthBehavior), 10);

	public int MaxLength
	{
		get => (int)GetValue(MaxLengthProperty);
		set => SetValue(MaxLengthProperty, value);
	}

	protected override void OnAttachedTo(Entry entry)
	{
		entry.TextChanged += OnTextChanged;
		base.OnAttachedTo(entry);
	}

	protected override void OnDetachingFrom(Entry entry)
	{
		entry.TextChanged -= OnTextChanged;
		base.OnDetachingFrom(entry);
	}

	private void OnTextChanged(object? sender, TextChangedEventArgs e)
	{
		if (sender is Entry entry && (e.NewTextValue?.Length ?? 0) > MaxLength)
			entry.Text = e.NewTextValue?[..MaxLength];
	}
}
