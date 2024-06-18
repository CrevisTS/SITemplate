using CvsService.Camera.Core.Events;
using CvsService.Camera.Core.Utils;
using CvsService.Camera.CvsGigE.Models;
using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LGES_SVA.Camera.Datas
{
	public class CamData : BindableBase
	{
		private double _exposureTime;
		private double _gainAbs;

		public CvsGigECamera Cam { get; set; }
		public WriteableBitmapWrapper Bitmap { get; set; }
		public string Name { get; private set; }
		public string ModelName { get; private set; }
		public int Height { get; private set; }
		public int Width { get; private set; }
		public double AcquisitionFrameRate { get; private set; }
		public double Resolution { get; private set; }
		public string Vendor { get; private set; }

		public double ExposureTime
		{
			get => _exposureTime;

			set 
			{
				SetProperty(ref _exposureTime, value);
				Cam.CvsGigEFeatureController.SetFloatFeature("ExposureTime", value);
			}
		}

		public double GainAbs { get => _gainAbs;
			set
			{
				SetProperty(ref _gainAbs, value);
				Cam.CvsGigEFeatureController.SetFloatFeature("GainAbs", value);
			}
		}

		public CamData() { }

		public CamData(string name, CvsGigECamera cam)
		{
			Name = name;
			Cam = cam;

			ModelName = cam.CvsGigEFeatureController.GetStrFeature("DeviceModelName");
			Vendor = cam.CvsGigEFeatureController.GetStrFeature("DeviceVendorName");
			Height = cam.CvsGigEFeatureController.GetIntFeature("Height");
			Width = cam.CvsGigEFeatureController.GetIntFeature("Width");
			AcquisitionFrameRate = cam.CvsGigEFeatureController.GetFloatFeature("AcquisitionFrameRate");
			ExposureTime = cam.CvsGigEFeatureController.GetFloatFeature("ExposureTime");
			GainAbs = cam.CvsGigEFeatureController.GetFloatFeature("GainAbs");

			InitCallback();

			Bitmap = new WriteableBitmapWrapper(Width, Height, PixelFormats.Indexed8, BitmapPalettes.Gray256);
		}

		private void InitCallback()
		{
			Cam.GrabController.ImageCaptured += GrabController_ImageCaptured;
		}

		private void GrabController_ImageCaptured(object sender, GrabImageEventArgs e)
		{
			Application.Current.Dispatcher.BeginInvoke((Action) (() =>
			{
				Bitmap.UpdateWBmp(e.RawData);
			}));
		}

		public void AcqStart()
		{
			Cam.GrabController.GrabStart(Height * Width);
		}

		public void AcqStop()
		{
			try
			{
				Cam.GrabController.ImageCaptured -= GrabController_ImageCaptured;
				Cam.GrabController.GrabStop();
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
