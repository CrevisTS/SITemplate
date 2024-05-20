using LGES_SVA.Core.Datas.Communicate.PLC;
using LGES_SVA.Core.Interfaces.Communicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Repository.Services
{
	public class CommunicateRepository : ICommunicateRepository
	{
		public PLC PLC { get; set; }

		public CommunicateRepository()
		{
			PLC = new PLC();
		}
	}
}
