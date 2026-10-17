namespace MauiGallery.Samples.Styles;

public partial class AnimationSample : ContentView
{
	private bool _loop;

	public AnimationSample()
	{
		InitializeComponent();
	}

	// ---- ViewExtensions ----
	private async void OnFade(object? sender, EventArgs e)
	{
		await AnimBox.FadeTo(0.2, 400, Easing.SinInOut);
		await AnimBox.FadeTo(1, 400, Easing.SinInOut);
	}

	private async void OnScale(object? sender, EventArgs e)
	{
		await AnimBox.ScaleTo(1.5, 300, Easing.CubicOut);
		await AnimBox.ScaleTo(1, 300, Easing.CubicIn);
	}

	private async void OnTranslate(object? sender, EventArgs e)
	{
		await AnimBox.TranslateTo(60, 20, 350, Easing.CubicInOut);
		await AnimBox.TranslateTo(0, 0, 350, Easing.CubicInOut);
	}

	private async void OnRotate(object? sender, EventArgs e)
	{
		await AnimBox.RotateTo(AnimBox.Rotation + 360, 700, Easing.CubicOut);
	}

	private void OnReset(object? sender, EventArgs e)
	{
		AnimBox.AbortAnimation();
		AnimBox.Opacity = 1;
		AnimBox.Scale = 1;
		AnimBox.TranslationX = 0;
		AnimBox.TranslationY = 0;
		AnimBox.Rotation = 0;
	}

	// ---- Animation class ----
	private void OnCommitAnim(object? sender, EventArgs e)
	{
		_loop = true;
		var animation = new Animation(v => SpinBox.Rotation = v, 0, 360, Easing.Linear);
		animation.Insert(0, 0.5, new Animation(v => SpinBox.Scale = v, 1, 1.3, Easing.SinInOut));
		animation.Insert(0.5, 1, new Animation(v => SpinBox.Scale = v, 1.3, 1, Easing.SinInOut));
		animation.WithConcurrent(v => SpinBox.Opacity = 0.4 + (0.6 * v), 0, 1);
		animation.Commit(this, "Spin", 16, 1200, Easing.Linear,
			finished: (_, _) => AnimOutput.Text = "本轮完成 / cycle finished",
			repeat: () => _loop);
		AnimOutput.Text = "动画运行中…（Commit 提交）";
	}

	private void OnAbortAnim(object? sender, EventArgs e)
	{
		_loop = false;
		this.AbortAnimation("Spin");
		SpinBox.Rotation = 0;
		SpinBox.Scale = 1;
		SpinBox.Opacity = 1;
		AnimOutput.Text = "已停止 / aborted";
	}

	// ---- Easing ----
	private async void OnEaseLinear(object? sender, EventArgs e) => await RunEase(Easing.Linear);
	private async void OnEaseSin(object? sender, EventArgs e) => await RunEase(Easing.SinInOut);
	private async void OnEaseCubic(object? sender, EventArgs e) => await RunEase(Easing.CubicOut);
	private async void OnEaseBounce(object? sender, EventArgs e) => await RunEase(Easing.BounceOut);

	private async Task RunEase(Easing easing)
	{
		EaseBox.TranslationX = 0;
		await EaseBox.TranslateTo(220, 0, 900, easing);
		await EaseBox.TranslateTo(0, 0, 300, Easing.Linear);
	}
}
