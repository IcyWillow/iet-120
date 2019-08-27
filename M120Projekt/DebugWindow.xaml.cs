﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        private List<Button> _buttons = new List<Button>();
        public DebugWindow()
        {
            InitializeComponent();
        }

        private void BtnShowDialog_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDialog.Text))
            {
                MessageBox.Show($"Sie haben '{txtDialog.Text}' eingegeben.", "Dialog Test");
            }
            else
            {
                MessageBox.Show("Der Wert ist leer. Bitte überprüfen Sie Ihre Eingabe.", "Dialog Test");
            }
           
        }

        private void BtnCreateButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = new Button();
            b.Tag = _buttons.Count + 1;
            b.Height = 25;
            b.Width = 140;
            b.Content = $"Dynamischer Button ({b.Tag})";

            b.Click += (s, args) => {
                MessageBox.Show($"Hallo, ich bin der Button Nr. {((Button) s).Tag}!",
                    "Dynamische Methode");
            };
            _buttons.Add(b);
            stcButtons.Children.Add(b);
        }

        private void TxtCopy_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCopyCat.Text = txtCopy.Text;
        }

        private void BtnDestroyButtons_Click(object sender, RoutedEventArgs e)
        {
            _buttons.Clear();
            stcButtons.Children.Clear();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
