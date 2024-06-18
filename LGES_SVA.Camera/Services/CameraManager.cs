using CvsService.Camera.CvsGigE.Services;
using LGES_SVA.Camera.Datas;
using Prism.Mvvm;
using System.Collections.Generic;

namespace LGES_SVA.Camera.Services
{
	public class CameraManager : BindableBase
	{
		private Dictionary<string, CamData> _cameras;
		public CvsGigEManager CvsGigEManager { get; set; }
		public Dictionary<string, CamData> Cameras { get => _cameras; set => SetProperty(ref _cameras, value); }
		public CameraManager(CvsGigEManager cvsGigEManager)
		{
			CvsGigEManager = cvsGigEManager;

			Cameras = new Dictionary<string, CamData>();

			InitCam();
		}

		private void InitCam()
		{
			foreach(var cam in CvsGigEManager.CameraDic)
			{
				Cameras.Add(
					cam.Key,
					new CamData(cam.Key, cam.Value));
			}
		}

		public void AllReConnect()
		{
			foreach (var item in CvsGigEManager.CameraDic.Values)
			{
				item.ReConnectUseHDevice();
			}
		}

		public void AllAcqStart()
		{
			foreach(var cam in Cameras)
			{
				cam.Value.AcqStart();
			}
		}

		public void AllAcqStop()
		{
			foreach (var cam in Cameras)
			{
				cam.Value.AcqStop();
			}
		}

		public void AllHWTriggerSet()
		{
			foreach (var item in CvsGigEManager.CameraDic.Values)
			{
				item.CvsGigEFeatureController.SetEnumFeature("TriggerMode", "On");
				item.CvsGigEFeatureController.SetEnumFeature("TriggerSource", "Line1");
			}
		}

		public void AllSWTriggerSet()
		{
			foreach (var item in CvsGigEManager.CameraDic.Values)
			{
				item.CvsGigEFeatureController.SetEnumFeature("TriggerMode", "On");
				item.CvsGigEFeatureController.SetEnumFeature("TriggerSource", "Software");
			}
		}
		public void AllSWTriggerCmd()
		{
			foreach (var item in CvsGigEManager.CameraDic.Values)
			{
				item.CvsGigEFeatureController.SetCmdFeature("TriggerSoftware");
			}
		}

		public void AllFreeRunSet()
		{
			foreach (var item in CvsGigEManager.CameraDic.Values)
			{
				item.CvsGigEFeatureController.SetEnumFeature("TriggerMode", "Off");
			}
		}
	}
}
