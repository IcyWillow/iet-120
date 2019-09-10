using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using M120Projekt.Data;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
            lblWelcome.Content = $"Willkommen {Session.User.Salutation} {Session.User.Lastname}";
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Owner.Show();
            Session.End();
        }

        private void BtnAccountList_Click(object sender, RoutedEventArgs e)
        {
            AccountListWindow accountListWindow = new AccountListWindow {Owner = this};
            Hide();
            accountListWindow.ShowDialog();

        }

        private void BtnWords_Click(object sender, RoutedEventArgs e)
        {
            WordsWindow wordsWindow = new WordsWindow { Owner = this };
            wordsWindow.ShowDialog();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            HideGridElements();
            grdContainer.Children.Clear();
            grdContainer.Children.Add(new DifficultyControl());
        }

        private void HideGridElements()
        {
            foreach (UIElement c in grdMainMenu.Children)
            {
                if(c.GetType() != typeof(Grid)) c.Visibility = Visibility.Hidden;
            }
        }

        private void ShowGridElements()
        {
            grdContainer.Children.Clear();
            foreach (UIElement c in grdMainMenu.Children)
            {
                c.Visibility = Visibility.Visible;
            }
        }

        public void RecoverState()
        {
            ShowGridElements();
        }
    }
}
