using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using M120Projekt.Data;
using M120Projekt.Helper;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        private int _timerCounter = 0;

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

            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;
        }

        private void BtnRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow() { Owner = this};
            registrationWindow.ShowDialog();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            TryLogin(txtEmail.Text, txtPassword.Password);
            if (Session.IsAuthenticated())
            {
                Hide();
                txtEmail.Text = string.Empty;
                txtPassword.Password = string.Empty;
                lblErrorLogin.Visibility = Visibility.Hidden;
                MainMenuWindow mainMenuWindow = new MainMenuWindow { Owner = this };
                mainMenuWindow.ShowDialog();
            }
            else
            {
                _timer.Start();
                ValidationHelper.ShowErrors(lblErrorLogin, "Kombination nicht gefunden.");
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
            DebugWindow debugWindow = new DebugWindow {Owner = this};
            debugWindow.ShowDialog();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnLogin_Click(null, null);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ValidationHelper.BlinkForm(txtEmail, _timerCounter);
            ValidationHelper.BlinkForm(txtPassword, _timerCounter);
            _timerCounter = _timerCounter <= 4 ? _timerCounter + 1 : 0;
            if(_timerCounter == 0) _timer.Stop();
        }
    }
}
