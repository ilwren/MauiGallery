using Microsoft.Maui.Graphics;

namespace MauiGallery.Samples.Media;

public partial class GraphicsViewSample : ContentView
{
	private readonly TouchDrawable _touchDrawable = new();

	public GraphicsViewSample()
	{
		InitializeComponent();
		ShapesCanvas.Drawable = new DemoDrawable();
		GradientCanvas.Drawable = new GradientDrawable();
		TouchCanvas.Drawable = _touchDrawable;
	}

	private void OnStart(object? sender, PointerEventArgs e)
	{
		var p = e.GetPosition(TouchCanvas);
		if (p is not null)
		{
			_touchDrawable.AddPoint(new PointF((float)p.Value.X, (float)p.Value.Y));
			TouchCanvas.Invalidate();
			TouchOutput.Text = $"Start: {p.Value.X:F0}, {p.Value.Y:F0}";
		}
	}

	private void OnMove(object? sender, PointerEventArgs e)
	{
		var p = e.GetPosition(TouchCanvas);
		if (p is not null)
		{
			_touchDrawable.AddPoint(new PointF((float)p.Value.X, (float)p.Value.Y));
			TouchCanvas.Invalidate();
			TouchOutput.Text = $"Move: {p.Value.X:F0}, {p.Value.Y:F0}";
		}
	}

	private void OnEnd(object? sender, PointerEventArgs e) =>
		TouchOutput.Text = "End ✓（继续拖动可继续绘制）";
}

/// <summary>Draws basic shapes to demonstrate ICanvas.</summary>
internal sealed class DemoDrawable : IDrawable
{
	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		canvas.FillColor = Color.FromArgb("#512BD4");
		canvas.FillCircle(dirtyRect.Center.X - 70, dirtyRect.Center.Y, 40);

		canvas.FillColor = Color.FromArgb("#D600AA");
		canvas.FillRoundedRectangle(dirtyRect.Center.X - 20, dirtyRect.Center.Y - 40, 90, 80, 16);

		canvas.StrokeColor = Colors.Black;
		canvas.StrokeSize = 3;
		canvas.DrawLine(dirtyRect.Left + 10, dirtyRect.Top + 10, dirtyRect.Right - 10, dirtyRect.Bottom - 10);

		canvas.FontColor = Colors.DarkSlateGray;
		canvas.FontSize = 12;
		canvas.DrawString("ICanvas", dirtyRect.Left + 10, dirtyRect.Bottom - 24, 80, 20, HorizontalAlignment.Left, VerticalAlignment.Top);
	}
}

/// <summary>Draws a linear gradient background and a path.</summary>
internal sealed class GradientDrawable : IDrawable
{
	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		var gradient = new LinearGradientPaint(
			new PaintGradientStop[]
			{
				new(0f, Color.FromArgb("#512BD4")),
				new(1f, Color.FromArgb("#D600AA")),
			},
			new Point(0, 0),
			new Point(1, 1));

		canvas.SetFillPaint(gradient, dirtyRect);
		canvas.FillRectangle(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);

		var path = new PathF();
		path.MoveTo(dirtyRect.Left + 20, dirtyRect.Bottom - 20);
		path.LineTo(dirtyRect.Center.X, dirtyRect.Top + 20);
		path.LineTo(dirtyRect.Right - 20, dirtyRect.Bottom - 20);
		path.Close();
		canvas.StrokeColor = Colors.White;
		canvas.StrokeSize = 4;
		canvas.DrawPath(path);
	}
}

/// <summary>Collects touch points and draws them as connected lines.</summary>
internal sealed class TouchDrawable : IDrawable
{
	private readonly List<PointF> _points = new();

	public void AddPoint(PointF point)
	{
		_points.Add(point);
		if (_points.Count > 200)
			_points.RemoveAt(0);
	}

	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		canvas.StrokeColor = Color.FromArgb("#2B0B98");
		canvas.StrokeSize = 3;
		for (var i = 1; i < _points.Count; i++)
			canvas.DrawLine(_points[i - 1], _points[i]);

		canvas.FillColor = Color.FromArgb("#D600AA");
		foreach (var p in _points)
			canvas.FillCircle(p, 4);
	}
}
