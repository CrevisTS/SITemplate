using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LGES_SVA.Dialogs.Login.ViewModels
{
	public class PasswordBoxBehavior : Behavior<PasswordBox>
	{
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxBehavior), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += PasswordBox_PasswordChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= PasswordBox_PasswordChanged;
            base.OnDetaching();
        }

        private bool updatingPassword = false;

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!updatingPassword)
            {
                updatingPassword = true;
                Password = AssociatedObject.Password;
                updatingPassword = false;
            }
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as PasswordBoxBehavior;
            if (behavior != null && !behavior.updatingPassword)
            {
                if (behavior.AssociatedObject != null)
                {
                    behavior.AssociatedObject.PasswordChanged -= behavior.PasswordBox_PasswordChanged;
                    behavior.AssociatedObject.Password = behavior.Password;
                    behavior.AssociatedObject.PasswordChanged += behavior.PasswordBox_PasswordChanged;
                }
            }
        }
    }
}
