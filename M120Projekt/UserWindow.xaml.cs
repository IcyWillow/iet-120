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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using M120Projekt.Data;
using M120Projekt.Enum;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for UserShowWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        User User;

        private UserUpdate _userUpdate;

        public UserWindow(int userId)
        {
            InitializeComponent();
            User = User.ReadById(userId);
            UserShow userShow = new UserShow(User);
            grdElement.Children.Add(userShow);
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(MachineState.Read);          
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(MachineState.Update);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Element wirklich löschen?", "Löschen", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                User.Delete();
                Close();
            }
        }

        private void ChangeState(MachineState state)
        {
            grdElement.Children.Clear();
            UserControl uc = null;

            switch (state)
            {
                case MachineState.Read:
                    uc = new UserShow(User);
                    break;
                case MachineState.Update:
                    if (_userUpdate == null) _userUpdate = new UserUpdate(User);
                    uc = _userUpdate;
                    break;
            }

            grdElement.Children.Add(uc);
           }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
