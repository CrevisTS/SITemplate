using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Datas.Communicate.PLC
{
	public class PLC
	{
		public bool IsConnected { get; set; }

		public PLC()
		{
			IsConnected = false;
		}
	}
}
