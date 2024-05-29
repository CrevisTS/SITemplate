using LGES_SVA.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Datas
{
	public sealed class DialogNames
	{
		// 일반 설정
		public static string LoginDialog => nameof(EDialogType.LoginDialog);
		public static string SimulationDialog => nameof(EDialogType.SimulationDialog);
		public static string LogDialog => nameof(EDialogType.LogDialog);
		public static string RecipeDialog => nameof(EDialogType.RecipeDialog);
		public static string SearchDialog => nameof(EDialogType.SearchDialog);
		public static string LiveDialog => nameof(EDialogType.LiveDialog);
		public static string SettingDialog => nameof(EDialogType.SettingDialog);

		// 통신 설정
		public static string PLCDialog => nameof(EDialogType.PLCDialog);
		public static string IODialog => nameof(EDialogType.IODialog);
		public static string DBDialog => nameof(EDialogType.DBDialog);
		public static string LightDialog => nameof(EDialogType.LightDialog);
		public static string CamDialog => nameof(EDialogType.CamDialog);

	}
}
