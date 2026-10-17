using Microsoft.UI;
using Microsoft.UI.Windowing;

namespace MauiGallery.WinUI;

/// <summary>
/// Colors the native Windows title bar to match the app palette (Windows 11;
/// silently ignored elsewhere or when unpackaged builds disallow it).
/// </summary>
internal static class TitleBarHelper
{
	public static void ApplyTo(Microsoft.UI.Xaml.Window? nativeWindow, bool dark)
	{
		if (nativeWindow is null)
			return;

		try
		{
			var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
			var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
			var appWindow = AppWindow.GetFromWindowId(windowId);
			var titleBar = appWindow.TitleBar;

			var accent = dark ? ColorHelper.FromArgb(255, 0x2A, 0x1A, 0x66) : ColorHelper.FromArgb(255, 0x51, 0x2B, 0xD4);
			var text = Microsoft.UI.Colors.White;
			var hover = dark ? ColorHelper.FromArgb(255, 0x3B, 0x2A, 0x80) : ColorHelper.FromArgb(255, 0x6A, 0x4B, 0xE0);
			var pressed = dark ? ColorHelper.FromArgb(255, 0x4A, 0x36, 0x99) : ColorHelper.FromArgb(255, 0x7A, 0x5C, 0xF0);

			titleBar.BackgroundColor = accent;
			titleBar.ForegroundColor = text;
			titleBar.InactiveBackgroundColor = accent;
			titleBar.InactiveForegroundColor = text;

			titleBar.ButtonBackgroundColor = accent;
			titleBar.ButtonForegroundColor = text;
			titleBar.ButtonInactiveBackgroundColor = accent;
			titleBar.ButtonInactiveForegroundColor = text;
			titleBar.ButtonHoverBackgroundColor = hover;
			titleBar.ButtonHoverForegroundColor = text;
			titleBar.ButtonPressedBackgroundColor = pressed;
			titleBar.ButtonPressedForegroundColor = text;
		}
		catch
		{
			// Unpackaged/older Windows: system default title bar is fine.
		}
	}
}
