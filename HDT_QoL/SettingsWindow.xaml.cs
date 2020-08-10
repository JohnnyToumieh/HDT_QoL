using System.Windows;
using System.Windows.Controls;

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
