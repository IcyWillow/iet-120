﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    ? new SolidColorBrush(Colors.IndianRed) : new SolidColorBrush(Colors.White);
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
    }
}
