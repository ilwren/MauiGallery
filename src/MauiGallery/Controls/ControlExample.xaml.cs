using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace MauiGallery.Controls;

/// <summary>
/// Hosts one live control example together with its XAML / C# source, modeled on the
/// <c>ControlExample</c> control used by the WinUI 3 Gallery.
/// </summary>
public partial class ControlExample : ContentView
{
	private View? _pendingExample;
	private View? _pendingOutput;
	private View? _pendingOptions;

	public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), typeof(string), typeof(ControlExample), string.Empty);

	public static readonly BindableProperty DescriptionProperty =
		BindableProperty.Create(nameof(Description), typeof(string), typeof(ControlExample), string.Empty, propertyChanged: OnTextPropertyChanged);

	public static readonly BindableProperty BadgeProperty =
		BindableProperty.Create(nameof(Badge), typeof(string), typeof(ControlExample), string.Empty, propertyChanged: OnTextPropertyChanged);

	public static readonly BindableProperty ExampleProperty =
		BindableProperty.Create(nameof(Example), typeof(View), typeof(ControlExample), null, propertyChanged: OnExampleChanged);

	public static readonly BindableProperty OutputProperty =
		BindableProperty.Create(nameof(Output), typeof(View), typeof(ControlExample), null, propertyChanged: OnOutputChanged);

	public static readonly BindableProperty OptionsProperty =
		BindableProperty.Create(nameof(Options), typeof(View), typeof(ControlExample), null, propertyChanged: OnOptionsChanged);

	public static readonly BindableProperty XamlProperty =
		BindableProperty.Create(nameof(Xaml), typeof(string), typeof(ControlExample), string.Empty, propertyChanged: OnSourcePropertyChanged);

	public static readonly BindableProperty CSharpProperty =
		BindableProperty.Create(nameof(CSharp), typeof(string), typeof(ControlExample), string.Empty, propertyChanged: OnSourcePropertyChanged);

	public ControlExample()
	{
		InitializeComponent();
		ApplyPending();
		Loaded += (_, _) => ApplyPending();
	}

	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public string Description
	{
		get => (string)GetValue(DescriptionProperty);
		set => SetValue(DescriptionProperty, value);
	}

	public string Badge
	{
		get => (string)GetValue(BadgeProperty);
		set => SetValue(BadgeProperty, value);
	}

	public View? Example
	{
		get => (View?)GetValue(ExampleProperty);
		set => SetValue(ExampleProperty, value);
	}

	public View? Output
	{
		get => (View?)GetValue(OutputProperty);
		set => SetValue(OutputProperty, value);
	}

	public View? Options
	{
		get => (View?)GetValue(OptionsProperty);
		set => SetValue(OptionsProperty, value);
	}

	public string Xaml
	{
		get => (string)GetValue(XamlProperty);
		set => SetValue(XamlProperty, value);
	}

	public string CSharp
	{
		get => (string)GetValue(CSharpProperty);
		set => SetValue(CSharpProperty, value);
	}

	public bool HasDescription => !string.IsNullOrWhiteSpace(Description);
	public bool HasBadge => !string.IsNullOrWhiteSpace(Badge);
	public bool HasOptions => Options is not null;
	public bool HasSource => !string.IsNullOrWhiteSpace(Xaml) || !string.IsNullOrWhiteSpace(CSharp);

	private static void OnExampleChanged(BindableObject bindable, object? oldValue, object? newValue)
	{
		var self = (ControlExample)bindable;
		self._pendingExample = newValue as View;
		self.ApplyPending();
	}

	private static void OnOutputChanged(BindableObject bindable, object? oldValue, object? newValue)
	{
		var self = (ControlExample)bindable;
		self._pendingOutput = newValue as View;
		self.ApplyPending();
	}

	private static void OnOptionsChanged(BindableObject bindable, object? oldValue, object? newValue)
	{
		var self = (ControlExample)bindable;
		self._pendingOptions = newValue as View;
		self.ApplyPending();
	}

	/// <summary>
	/// Hosts are named elements inside the control's own XAML; property-changed callbacks can
	/// fire while that XAML is still loading, so defer touching them until they exist.
	/// </summary>
	private void ApplyPending()
	{
		if (ExampleHost is null)
			return;

		ExampleHost.Content = _pendingExample;
		if (OutputHost is not null)
		{
			OutputHost.Content = _pendingOutput;
			OutputHost.IsVisible = _pendingOutput is not null;
		}
		if (OptionsHost is not null)
		{
			OptionsHost.Content = _pendingOptions;
			OnPropertyChanged(nameof(HasOptions));
		}
		OnPropertyChanged(nameof(HasSource));
	}

	private static void OnTextPropertyChanged(BindableObject bindable, object? oldValue, object? newValue)
	{
		var self = (ControlExample)bindable;
		self.OnPropertyChanged(nameof(HasDescription));
		self.OnPropertyChanged(nameof(HasBadge));
	}

	private static void OnSourcePropertyChanged(BindableObject bindable, object? oldValue, object? newValue)
	{
		((ControlExample)bindable).OnPropertyChanged(nameof(HasSource));
	}

	private void OnToggleSourceClicked(object? sender, EventArgs e)
	{
		var show = !SourceText.IsVisible;
		SourceText.IsVisible = show;
		SourceText.Text = BuildSourceText();
		ToggleButton.Text = show ? "隐藏源码 / Hide source" : "显示源码 / Show source";
	}

	private void OnCopySourceClicked(object? sender, EventArgs e)
	{
		var text = BuildSourceText();
		if (text.Length > 0)
			Clipboard.Default.SetTextAsync(text);
	}

	private string BuildSourceText()
	{
		var parts = new List<string>();
		if (!string.IsNullOrWhiteSpace(Xaml))
			parts.Add(Xaml.Trim());
		if (!string.IsNullOrWhiteSpace(CSharp))
			parts.Add("// ===== C# =====\n" + CSharp.Trim());
		return string.Join("\n\n", parts);
	}
}
