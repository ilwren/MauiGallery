namespace MauiGallery.Samples.Gestures;

public partial class SwipeViewSample : ContentView
{
	public SwipeViewSample()
	{
		InitializeComponent();
	}

	private void OnFavorite(object? sender, EventArgs e) => SwipeOutput.Text = "收藏 / Favorite ✓";
	private void OnDelete(object? sender, EventArgs e) => SwipeOutput.Text = "删除 / Delete（示例中不真正删除）";
	private void OnRead(object? sender, EventArgs e) => SwipeOutput.Text = "标记已读 / Marked as read";
	private void OnArchive(object? sender, EventArgs e) => SwipeOutput.Text = "归档 / Archived";
	private void OnPin(object? sender, EventArgs e) => SwipeOutput.Text = "置顶 / Pinned";
	private void OnMute(object? sender, EventArgs e) => SwipeOutput.Text = "静音 / Muted";
	private void OnCustomSwipeItem(object? sender, TappedEventArgs e) => SwipeOutput.Text = "自定义滑动项被点击 / Custom item tapped";
	private void OnRemainOpen(object? sender, EventArgs e) => SwipeOutput.Text = "点击后保持打开 / RemainOpen";
}
