using Paint.Model;
using PaintTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Paint.ViewModel
{
	public class SignatureViewModel : BindableObjectBase
	{
		private ImageWithTouch DrawingImage;
		public SignatureViewModel()
		{
			DrawingImage = new ImageWithTouch
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.White,
				CurrentLineColor = Color.Black
			};

			DrawingImage.SetBinding(ImageWithTouch.CurrentLineColorProperty, "CurrentLineColor");
		}

		public Frame CreateDrawingSurface()
		{
			var surface = new Frame
			{
				BackgroundColor = Color.White,
				HasShadow = false,
				Padding=1,
				OutlineColor = Color.Red,
				Content = DrawingImage
			};			
			return surface;
		}

		public void SaveToBase64()
		{
			using (MemoryStream stream = new MemoryStream())
			{
				App.Signature.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 90, stream);
				var bytes = stream.ToArray();
				var str = Convert.ToBase64String(bytes);
				var b = 1;
			}
		}
	}
}
