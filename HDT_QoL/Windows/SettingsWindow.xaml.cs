using System.Windows;

using MahApps.Metro.Controls;

namespace HDT_QoL
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void BtnUnlock_Click(object sender, RoutedEventArgs e)
        {
            BtnUnlock.Content = MainHandler.Input.Toggle() ? "Lock Overlay" : "Unlock Overlay";
        }

        private void ToggleBannedTribe(object sender, RoutedEventArgs e)
        {
            if (CheckboxEnableBannedTribe.IsChecked == true)
            {
                MainHandler.IsBannedTribeEnabled = true;
            }
            else
            {
                MainHandler.IsBannedTribeEnabled = false;
            }
        }
    }
}
