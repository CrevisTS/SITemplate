using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Interfaces.Modules.VisionPro
{
	public interface IVisionProService
	{
		object Load(string path);
		void Save(object toolBlock, string path);
	}
}
