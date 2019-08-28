using System.Windows;

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

            this.Hide();
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Owner = this;
            registrationWindow.ShowDialog();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            this.Hide();
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.Owner = this;
            mainMenuWindow.ShowDialog();
        }

        private void BtnDebug_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DebugWindow debugWindow = new DebugWindow();
            debugWindow.Owner = this;
            debugWindow.ShowDialog();
        }
    }
}
