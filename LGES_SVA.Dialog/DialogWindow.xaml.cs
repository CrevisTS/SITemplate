using LGES_SVA.Core.Events;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LGES_SVA.Dialog
{
	/// <summary>
	/// DialogWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class DialogWindow : Window, IDialogWindow
	{
		private IEventAggregator _eventAggregator;

		// 튜플로 사용하려면 필드여야함
		public (string, CancelEventArgs) Item;

		public DialogWindow(IEventAggregator eventAggregator)
		{
			InitializeComponent();

			_eventAggregator = eventAggregator;

		}

		private void DialogWindow_Closing(object sender, CancelEventArgs e)
		{
			Item.Item1 = Content.GetType().Name;
			Item.Item2 = e;

			// Event Aggregator 구현?
			_eventAggregator.GetEvent<DialogClosingEvent>().Publish(Item);
		}

		public IDialogResult Result { get; set; }


	}
}
