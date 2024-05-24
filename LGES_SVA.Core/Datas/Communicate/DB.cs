using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Datas.Communicate
{
	public class DB
	{
		public bool IsConnected { get; set; }

		public DB()
		{
			IsConnected = false;
		}
	}
}
