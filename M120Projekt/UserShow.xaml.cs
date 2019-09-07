using M120Projekt.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for UserShow.xaml
    /// </summary>
    public partial class UserShow : UserControl
    {
        public User User;
        public UserShow(User user)
        {
            InitializeComponent();

            User = user;
            ShowData();
        }

        private void ShowData()
        {
            txtId.Text = User.Id.ToString();
            txtAnrede.Text = User.Salutation;
            txtEmail.Text = User.Email;
            txtFirstname.Text = User.Firstname;
            txtLastname.Text = User.Lastname;
            txtCreatedAt.Text = User.CreatedAt.ToLocalTime().ToString();
            txtUpdatedAt.Text = User.UpdatedAt.ToLocalTime().ToString();
        }
    }
}
