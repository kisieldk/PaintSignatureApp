using Paint.ViewModel;
using PaintTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Paint.View
{
	public partial class SignaturePage : ContentPage
	{
		private SignatureViewModel _signatureViewModel;
		public SignaturePage()
		{
			InitializeComponent();
			_signatureViewModel = new SignatureViewModel();
			DrawingFrame.Content = _signatureViewModel.CreateDrawingSurface();
		}
		private void btnSave_Clicked(object sender, EventArgs e)
		{
			_signatureViewModel.SaveToBase64();
		}
		private void btnClean_Clicked(object sender, EventArgs e)
		{
			DrawingFrame.Content = _signatureViewModel.CreateDrawingSurface();
		}
	}
}
