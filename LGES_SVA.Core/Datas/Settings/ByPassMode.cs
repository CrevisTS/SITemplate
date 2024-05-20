using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Datas.Settings
{
	public class ByPassMode : BindableBase
	{
		private bool _isByPass_PouchAngle;
		private bool _isByPass_AlignResult;

		public bool IsByPass_PouchAngle { get => _isByPass_PouchAngle; set => SetProperty(ref _isByPass_PouchAngle, value); }
		public bool IsByPass_AlignResult { get => _isByPass_AlignResult; set => SetProperty(ref _isByPass_AlignResult, value); }

		public ByPassMode()
		{
			IsByPass_PouchAngle = false;
			IsByPass_AlignResult = false;
		}
	}
}
