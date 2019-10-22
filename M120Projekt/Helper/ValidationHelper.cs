using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace M120Projekt.Helper
{
    public class ValidationHelper
    {
        public static void ShowErrors(Label label, string error, object sender = null)
        {
            label.Content = error;
            label.Visibility = !string.IsNullOrEmpty(label.Content.ToString())
                ? Visibility.Visible : Visibility.Hidden;
            if (sender != null)
            {
                ((Control)sender).Background = !string.IsNullOrEmpty(label.Content.ToString())
                    ? new SolidColorBrush(Colors.DarkRed) : new SolidColorBrush(Colors.DarkGreen);
            }
        }

        public static bool ValidateForm(Grid grid)
        {
            //Check if there are any errors on the screen
            foreach (Control c in grid.Children)
            {
                if (c.GetType() == typeof(Label) && c.Tag != null && c.Tag.Equals("error") && c.Visibility == Visibility.Visible) return false;
            }

            return true;
        }

        public static void HideErrorLabels(UIElementCollection childrenCollection)
        {
            foreach (Control c in childrenCollection)
            {
                if (c.GetType() == typeof(Label))
                {
                    if (c.Tag != null && c.Tag.Equals("error")) c.Visibility = Visibility.Hidden;
                }
            }
        }

        public static void BlinkForm(Control control, int interval)
        {
            int round = interval % 2;
            control.Background = round == 0 ? new SolidColorBrush(Colors.DarkRed) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));
        }
    }
}
