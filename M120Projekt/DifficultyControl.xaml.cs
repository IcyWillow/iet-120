using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using M120Projekt.Enum;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for DifficultyControl.xaml
    /// </summary>
    public partial class DifficultyControl : UserControl
    {
        private Difficulty _difficulty = Difficulty.Easy;
        private DispatcherTimer _timer = new DispatcherTimer();
        private MainMenuWindow _window;

        public DifficultyControl()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromMilliseconds(15);
            _timer.Tick += TimerTick;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            btnCancel.IsEnabled = false;
            btnStart.IsEnabled = false;
            foreach (RadioButton rb in stkPanel.Children) rb.IsEnabled = false;
            psbLoad.Visibility = Visibility.Visible;
            lblLoad.Visibility = Visibility.Visible;
            _window = (MainMenuWindow)Window.GetWindow(this);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            psbLoad.Value++;
            if (psbLoad.Value >= 100)
            {
                _timer.Stop();
                StartGame(_window);
            }
        }

        private void StartGame(MainMenuWindow window)
        {
            window.StartGame(new GameControl(_difficulty));
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = (MainMenuWindow)Window.GetWindow(this);
            mainMenuWindow.RecoverState();
        }

        private void RdbEasy_Checked(object sender, RoutedEventArgs e)
        {
            _difficulty = Difficulty.Easy;
        }

        private void RdbMedium_Checked(object sender, RoutedEventArgs e)
        {
            _difficulty = Difficulty.Medium;
        }

        private void RdbHard_Checked(object sender, RoutedEventArgs e)
        {
            _difficulty = Difficulty.Hard;
        }
    }

}
