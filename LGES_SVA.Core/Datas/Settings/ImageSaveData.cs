using LGES_SVA.Core.Datas.Settings.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGES_SVA.Core.Datas.Settings
{
	public class ImageSaveData
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public EImageType Type { get; set; }
		public int Rate { get; set; }

		public ImageSaveData()
		{
			Type = EImageType.BMP;
			Rate = 0;
		}
	}
}
