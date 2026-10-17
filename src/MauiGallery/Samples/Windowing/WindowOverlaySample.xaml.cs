using Microsoft.Maui.Graphics;

namespace MauiGallery.Samples.Windowing;

public partial class WindowOverlaySample : ContentView
{
	private WindowOverlay? _overlay;

	public WindowOverlaySample()
	{
		InitializeComponent();
	}

	private void OnAdd(object? sender, EventArgs e)
	{
		var window = Application.Current?.Windows.FirstOrDefault();
		if (window is null)
			return;

		if (_overlay is not null)
		{
			window.RemoveOverlay(_overlay);
			_overlay = null;
		}

		_overlay = new WindowOverlay(window);
		_overlay.AddWindowElement(new WatermarkElement());
		window.AddOverlay(_overlay);
		_overlay.Invalidate();
		OverlayOutput.Text = "覆盖层已添加 / Overlay added（查看窗口右上角）";
	}

	private void OnRemove(object? sender, EventArgs e)
	{
		var window = Application.Current?.Windows.FirstOrDefault();
		if (window is null || _overlay is null)
			return;

		window.RemoveOverlay(_overlay);
		_overlay = null;
		OverlayOutput.Text = "覆盖层已移除 / Overlay removed";
	}
}

/// <summary>A drawable element painted on the window overlay (a timestamp watermark).</summary>
internal sealed class WatermarkElement : IWindowOverlayElement
{
	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		canvas.FontColor = Colors.Gray;
		canvas.FontSize = 11;
		var text = $"MAUI Gallery · {System.DateTime.Now:HH:mm:ss}";
		canvas.DrawString(text, dirtyRect.Right - 190, dirtyRect.Top + 8, 180, 20,
			HorizontalAlignment.Right, VerticalAlignment.Top);
	}

	public bool Contains(Point point) => false;
}
