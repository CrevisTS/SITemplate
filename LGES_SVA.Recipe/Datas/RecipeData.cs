using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGES_SVA.Recipe.Datas
{
	public class RecipeData : BindableBase
	{
		private string _name;
		private string _path;

		public string Name { get => _name; set => SetProperty(ref _name, value); }

		public string Path { get => _path; set => SetProperty(ref _path, value); }
		public RecipeData(string name)
		{
			_name = name;
		}
	}
}
