using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Paint.Droid;
using System.IO;

namespace PaintTest.Droid
{
	public class DrawView : View
	{
		public DrawView(Context context)
			: base(context)
		{
			Start();
		}

		public Color CurrentLineColor { get; set; }

		public float PenWidth { get; set; }

		private Android.Graphics.Path DrawPath;
		private Android.Graphics.Paint DrawPaint;
		private Android.Graphics.Paint CanvasPaint;
		private Canvas DrawCanvas;
		private Bitmap CanvasBitmap;

		private void Start()
		{		
			CurrentLineColor = Color.Black;
			PenWidth = 5.0f;

			DrawPath = new Android.Graphics.Path();
			DrawPaint = new Android.Graphics.Paint
			{
				Color = CurrentLineColor,
				AntiAlias = true,
				StrokeWidth = PenWidth
			};

			DrawPaint.SetStyle(Android.Graphics.Paint.Style.Stroke);
			DrawPaint.StrokeJoin = Android.Graphics.Paint.Join.Round;
			DrawPaint.StrokeCap = Android.Graphics.Paint.Cap.Round;

			CanvasPaint = new Android.Graphics.Paint
			{
				Dither = true
			};
		}
		private void Stop()
		{
			byte[] bitmapData;
			using (var stream = new MemoryStream())
			{
				CanvasBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
				bitmapData = stream.ToArray();
			}
		}
		protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged(w, h, oldw, oldh);

			CanvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
			DrawCanvas = new Canvas(CanvasBitmap);
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			DrawPaint.Color = CurrentLineColor;
			canvas.DrawBitmap(CanvasBitmap, 0, 0, CanvasPaint);
			canvas.DrawPath(DrawPath, DrawPaint);
			App.Signature = CanvasBitmap;
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			var touchX = e.GetX();
			var touchY = e.GetY();

			switch (e.Action)
			{
				case MotionEventActions.Down:
					DrawPath.MoveTo(touchX, touchY);
					break;
				case MotionEventActions.Move:
					DrawPath.LineTo(touchX, touchY);
					break;
				case MotionEventActions.Up:
					DrawCanvas.DrawPath(DrawPath, DrawPaint);
					DrawPath.Reset();
					break;
				default:
					return false;
			}

			Invalidate();

			return true;
		}
	}
}