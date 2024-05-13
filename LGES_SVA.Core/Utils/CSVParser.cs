using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Utils
{
	public class CSVParser
	{

		public List<T> ReadCSV<T>(string path) where T : new()
		{
			List<T> list = new List<T>();

			if (File.Exists(path))
			{
				using(var reader = new StreamReader(path))
				{
					reader.ReadLine();

					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						string[] values = line.Split(',');

						T data = new T();
						var properties = typeof(T).GetProperties();
						for(int i=0; i<properties.Length; i++)
						{
							var propertyType = properties[i].PropertyType;

							if (propertyType.IsEnum)
							{
								properties[i].SetValue(data, Enum.Parse(propertyType, values[i]));

							}
							else
							{
								properties[i].SetValue(data, Convert.ChangeType(values[i], properties[i].PropertyType));

							}
						}

						list.Add(data);
					}
				}
			}
			return list;
		}
	}
}
