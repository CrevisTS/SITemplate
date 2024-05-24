using LGES_SVA.Core.Datas.Communicate;
using LGES_SVA.Core.Datas.Communicate.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Interfaces.Communicate
{
	public interface ICommunicateRepository
	{
		PLC PLC { get; set; }
		IO IO { get; set; }
		DB DB { get; set; }
		Light Light { get; set; }
		Cam Cam { get; set; }
	}
}
