using LGES_SVA.Core.Enums.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Datas.Login
{
	public class User
	{
		public string ID { get; set; }
		public string Password { get; set; }
		public ELevel Level { get; set; }

		public User() { }

		public User(ELevel level)
		{
			Level = level;
		}
	}
}
