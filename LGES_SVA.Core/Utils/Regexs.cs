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
