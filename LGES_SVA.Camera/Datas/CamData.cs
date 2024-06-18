using CvsService.Camera.Core.Events;
using CvsService.Camera.Core.Utils;
using CvsService.Camera.CvsGigE.Models;
using LGES_SVA.Core.Utils;
using Prism.Mvvm;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Pen = System.Windows.Media.Pen;
using Point = System.Windows.Point;

namespace LGES_SVA.Camera.Datas
{
	public class CamData : BindableBase
	{
		private double _exposureTime;
		private double _gainAbs;
		private ConcurrentQueue<Bitmap> _bitmaps;

		public CvsGigECamera Cam { get; set; }
		public WriteableBitmapWrapper WBitmap { get; set; }
		public ConcurrentQueue<Bitmap> Bitmaps { get => _bitmaps; set => SetProperty(ref _bitmaps, value); }
		public BitmapSource CrossLineOverlay { get; set; }

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

			WBitmap = new WriteableBitmapWrapper(Width, Height, PixelFormats.Indexed8, BitmapPalettes.Gray256);

			DrawCrossLineOverlay();

			Bitmaps = new ConcurrentQueue<Bitmap>();
		}

		private void DrawCrossLineOverlay()
		{
			// 이미지 사이즈로 도형을 새로 그린다.
			RenderTargetBitmap rtb = new RenderTargetBitmap(Width, Height, 96d, 96d, PixelFormats.Pbgra32);
			DrawingVisual dv = new DrawingVisual();
			using (DrawingContext dc = dv.RenderOpen())
			{
				// 투명한 Background의 도형이미지 그리기
				dc.DrawLine(new Pen(Brushes.Red, 10), new Point(Width / 2, 0), new Point(Width / 2, Height));
				dc.DrawLine(new Pen(Brushes.Red, 10), new Point(0, Height / 2), new Point(Width, Height / 2));
				dc.Close();
				rtb.Render(dv);
				if (rtb.IsFrozen)
					rtb.Freeze();

			}
			CrossLineOverlay = rtb;
		}

		private void InitCallback()
		{
			Cam.GrabController.ImageCaptured += GrabController_ImageCaptured;
		}

		private void GrabController_ImageCaptured(object sender, GrabImageEventArgs e)
		{

			Application.Current.Dispatcher.BeginInvoke((Action)(() =>
		   {
				WBitmap.UpdateWBmp(e.RawData);
			}));

			Bitmaps.Enqueue(ImageHelper.ByteToBitmap(e.RawData, Width, Height));
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
