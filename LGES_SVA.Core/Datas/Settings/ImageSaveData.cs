using LGES_SVA.Core.Datas.Settings.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prism.Mvvm;

namespace LGES_SVA.Core.Datas.Settings
{
	public class ImageSaveData : BindableBase
	{
		private string _type;
		private int _rate;

		public string Type
		{
			get => _type;
			set
			{
				SetProperty(ref _type, value);
				// BMP는 압축할 필요가 없음
				if (_type == "BMP")
				{
					Rate = 0;
				}
			}
		}
		public int Rate { get => _rate; set => SetProperty(ref _rate, value); }

		[JsonIgnore]
		public string[] TypeArray { get; set; } = new string[] { "BMP", "PNG", "JPG" };
		[JsonIgnore]
		public int[] RateArray { get; set; } = new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

		public ImageSaveData()
		{
			Type = "BMP";
			Rate = 0;
		}
	}
}
