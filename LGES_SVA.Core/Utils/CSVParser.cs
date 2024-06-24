using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Utils
{
	public class CSVParser
	{
		public void WriteCSV<T>(List<T> data, string path)
		{
			try
			{
				if (!File.Exists(path)) { new FileNotFoundException(); }

				using (var writer = new StreamWriter(path))
				{
					var headerData = typeof(T).GetProperties();

					string[] header = new string[headerData.Length];

					for (int i = 0; i< headerData.Length; i++)
					{
						header[0] = headerData[i].PropertyType.Name;
					}

					writer.WriteLine(string.Join(",", header));
					writer.Flush();
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<T> ReadCSV<T>(string path) where T : new()
		{
	
			List<T> list = new List<T>();

			try
			{
				if (!File.Exists(path)) { new FileNotFoundException(); }

				using (var reader = new StreamReader(path))
				{
					reader.ReadLine();

					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						string[] values = line.Split(',');

						T data = new T();
						var properties = typeof(T).GetProperties();
						for (int i = 0; i < properties.Length; i++)
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
				return list;
			}
			catch (Exception)
			{

				throw;
			}
			

			
		}
	}
}
