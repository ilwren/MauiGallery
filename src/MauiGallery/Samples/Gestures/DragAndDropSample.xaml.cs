namespace MauiGallery.Samples.Gestures;

public partial class DragAndDropSample : ContentView
{
	public DragAndDropSample()
	{
		InitializeComponent();
	}

	private void OnDragStarting(object? sender, DragStartingEventArgs e)
	{
		e.Data.Properties.Add("text", "来自画廊的拖动数据 / Drag payload from gallery");
		DragOutput.Text = "DragStarting：数据已放入 DataPackage";
	}

	private void OnDragOver(object? sender, DragOverEventArgs e)
	{
		e.AcceptedOperation = DataPackageOperation.Copy;
	}

	private void OnDrop(object? sender, DropEventArgs e)
	{
		var text = e.Data.Properties.TryGetValue("text", out var value) ? value?.ToString() : "(无数据)";
		DragOutput.Text = $"Drop ✓：{text}";
	}

	private void OnPointerEntered(object? sender, PointerEventArgs e) =>
		PointerOutput.Text = "PointerEntered（悬停开始）";

	private void OnPointerMoved(object? sender, PointerEventArgs e)
	{
		var p = e.GetPosition(PointerBox);
		if (p is not null)
			PointerOutput.Text = $"PointerMoved: {p.Value.X:F0}, {p.Value.Y:F0}";
	}

	private void OnPointerExited(object? sender, PointerEventArgs e) =>
		PointerOutput.Text = "PointerExited（离开）";
}
