using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Utils
{
	public class JsonParser
	{
		/// <summary>
		/// 클래스를 Json으로 저장합니다.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="folderPath"></param>
		/// <param name="fileFullPath"></param>
		public static void Save<T>(T data, string folderPath, string fileFullPath) where T : class
		{
			if(!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }

			string jsonData = JsonConvert.SerializeObject(data);

			using (StreamWriter sw = new StreamWriter(fileFullPath))
			{
				sw.Write(jsonData);
			}

		}

		/// <summary>
		/// Json을 클래스로 불러옵니다.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileFullPath"></param>
		/// <returns></returns>
		public static T Load<T>(string fileFullPath) where T : class, new()
		{
			if (File.Exists(fileFullPath)) 
			{
				string jsonData;
				using(StreamReader sr = new StreamReader(fileFullPath))
				{
					jsonData = sr.ReadToEnd();
				}

				return JsonConvert.DeserializeObject<T>(jsonData);
			}
			else
			{
				return new T();
			}
		}

		public static T DeepCopy<T>(object json)
		{
			string serial = JsonConvert.SerializeObject(json);
			return JsonConvert.DeserializeObject<T>(serial);
		}
	}
}
