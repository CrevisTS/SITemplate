using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGES_SVA.VisionPro.Managers
{
	public class VisionProManager
	{
		/// <summary>
		/// Cam1, Cam2 Cailbration
		/// </summary>
		public CogToolBlock CailbrationTool1 { get; set; }
		/// <summary>
		/// Cam3, Cam4 Cailbration
		/// </summary>
		public CogToolBlock CailbrationTool2 { get; set; }

	}
}
