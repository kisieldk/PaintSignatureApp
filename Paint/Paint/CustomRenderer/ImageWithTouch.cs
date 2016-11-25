using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PaintTest
{
	public class ImageWithTouch : Image
	{
		public static readonly BindableProperty CurrentLineColorProperty =
			BindableProperty.Create((ImageWithTouch w) => w.CurrentLineColor, Color.Default);

		public Color CurrentLineColor
		{
			get
			{
				return (Color)GetValue(CurrentLineColorProperty);
			}
			set
			{
				SetValue(CurrentLineColorProperty, value);
			}
		}
	
	}
}
