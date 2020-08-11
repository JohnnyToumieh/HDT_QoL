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
            Properties.Settings.Default.PropertyChanged += (sender, e) => Properties.Settings.Default.Save();
        }

        private void BtnUnlockOverlay_Click(object sender, RoutedEventArgs e)
        {
            BtnUnlockOverlay.Content = MainHandler.Input.Toggle() ? "Lock All Overlays" : "Unlock All Overlays";
        }

        private void BtnResetBannedTribeOverlay_Click(object sender, RoutedEventArgs e)
        {
            MainHandler.ResetOverlayPosition();
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

        private void ToggleScaleWithWindow(object sender, RoutedEventArgs e)
        {
            if (CheckboxEnableScaleWithWindow.IsChecked == true)
            {
                SliderOverlayScale.IsEnabled = false;
                MainHandler.IsScaleWithWindowEnabled = true;
            }
            else
            {
                SliderOverlayScale.IsEnabled = true;
                MainHandler.IsScaleWithWindowEnabled = false;
            }
        }

        private void ToggleEnableBorder(object sender, RoutedEventArgs e)
        {
            if (CheckboxEnableBorder.IsChecked == true)
            {
                MainHandler.IsBorderEnabled = true;
            }
            else
            {
                MainHandler.IsBorderEnabled = false;
            }
        }

        private void ToggleEnableColors(object sender, RoutedEventArgs e)
        {
            if (CheckboxEnableColors.IsChecked == true)
            {
                MainHandler.IsColorsEnabled = true;
            }
            else
            {
                MainHandler.IsColorsEnabled = false;
            }
        }
    }
}
