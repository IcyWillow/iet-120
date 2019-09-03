using System.Windows;
using M120Projekt.Data;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
            //Insert, read and update test data (user)
            APIDemo.DemoUserCreate();
            APIDemo.DemoUserRead();
            APIDemo.DemoUserUpdate();

            //Insert, read and update test data (word)
            APIDemo.DemoWordCreate();
            APIDemo.DemoWordRead();
            APIDemo.DemoUserUpdate();

            //Insert, read and update test data (highscore)
            APIDemo.DemoHighscoreCreate();
            APIDemo.DemoHighscoreRead();
            APIDemo.DemoHighscoreUpdate();

            //Delete all test data
            APIDemo.DemoHighscoreDelete();
            APIDemo.DemoWordDelete();
            APIDemo.DemoUserDelete();
            
        }

        private void BtnRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            RegistrationWindow registrationWindow = new RegistrationWindow(null) { Owner = null};
            registrationWindow.ShowDialog();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            //TODO implement login
            TryLogin(txtEmail.Text, txtPassword.Password);
            if (Session.IsAuthenticated())
            {
                Hide();
                MainMenuWindow mainMenuWindow = new MainMenuWindow { Owner = this };
                mainMenuWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Login fehlgeschlagen. Bitte überprüfen Sie Ihre Eingaben.");
            }
            
        }

        private void TryLogin(string email, string password)
        {
            User user = User.ReadByEmail(email);
            if (user != null && user.IsPasswordCorrect(password)) Session.Start(user);
        }

        private void BtnDebug_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DebugWindow debugWindow = new DebugWindow {Owner = null};
            debugWindow.ShowDialog();
        }
    }
}
