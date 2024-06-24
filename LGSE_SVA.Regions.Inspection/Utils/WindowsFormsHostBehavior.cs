using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace LGES_SVA.Regions.Inspection.Utils
{
	class WindowsFormsHostBehavior : Behavior<WindowsFormsHost>
	{
		public Control Child
		{
			get { return (Control)GetValue(ChildProperty); }
			set { SetValue(ChildProperty, value); }
		}

		public static readonly DependencyProperty ChildProperty = DependencyProperty.Register(
			"Child", 
			typeof(Control), 
			typeof(WindowsFormsHostBehavior), 
			new PropertyMetadata(null, OnChildPropertyChanged));

		private static void OnChildPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var behavior = (WindowsFormsHostBehavior)d;
			behavior.OnChildChanged((Control)e.OldValue, (Control)e.NewValue);

		}

		private void OnChildChanged(Control oldValue, Control newValue)
		{
			AssociatedObject.Child = newValue;
		}

	}
}
