using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LGES_SVA.Core.Utils
{
	public static class Regexs
	{
		/// <summary>
		/// 입력된 키가 숫자만 입력되게 합니다.(백스페이스 추가)
		/// </summary>
		/// <param name="e"></param>
		public static void AcceptOnlyNumbers(KeyEventArgs e)
		{
			if (IsNumber(e) || IsNumberPad(e) || IsBacksapce(e))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 입력된 키가 숫자인지 확인합니다.(키패드)
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		private static bool IsNumber(KeyEventArgs e)
		{
			if (e.Key >= Key.D0 && e.Key <= Key.D9)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 입력된 키가 숫자인지 확인합니다.(넘버패드)
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		private static bool IsNumberPad(KeyEventArgs e)
		{
			if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 입력된 키가 백스페이스인지 확인합니다.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		private static bool IsBacksapce(KeyEventArgs e)
		{
			if (e.Key == Key.Back)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
