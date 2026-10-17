using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace MauiGallery.Platforms.Android;

/// <summary>
/// ShellFlyoutRenderer whose onMeasure never lets the AndroidX DrawerLayout see a
/// non-EXACTLY measure spec (which throws
/// "DrawerLayout must be measured with MeasureSpec.EXACTLY").
/// </summary>
public sealed class FixedShellFlyoutRenderer : ShellFlyoutRenderer
{
	public FixedShellFlyoutRenderer(IShellContext shellContext, Context context)
		: base(shellContext, context)
	{
	}

	protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
	{
		base.OnMeasure(ForceExactly(widthMeasureSpec), ForceExactly(heightMeasureSpec));
	}

	static int ForceExactly(int spec)
	{
		var size = MeasureSpec.GetSize(spec);
		return MeasureSpec.MakeMeasureSpec(size, MeasureSpecMode.Exactly);
	}
}

/// <summary>ShellRenderer that swaps in the measure-safe flyout renderer.</summary>
public sealed class FixedShellRenderer : ShellRenderer
{
	public FixedShellRenderer(Context context)
		: base(context)
	{
	}

	protected override IShellFlyoutRenderer CreateShellFlyoutRenderer() =>
		new FixedShellFlyoutRenderer(this, AndroidContext);
}
