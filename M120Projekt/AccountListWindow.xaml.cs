using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for AccountListWindow.xaml
    /// </summary>
    public partial class AccountListWindow : Window
    {
        public List<User> Users { get; set; }

        public AccountListWindow()
        {
            InitializeComponent();
            Users = User.All();

            this.dtgAccount.ItemsSource = Users;
        }

        private void DtgAccount_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Password")
            {
                e.Column = null;
            }
        }
    }
}
