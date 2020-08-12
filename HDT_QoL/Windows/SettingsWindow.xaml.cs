using System.Windows;
using System.Windows.Controls;

using MahApps.Metro.Controls;

using Hearthstone_Deck_Tracker;

namespace HDT_QoL
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsWindow : ScrollViewer
    {
        private static Flyout _flyout;

        public static Flyout Flyout
        {
            get
            {
                if (_flyout == null)
                {
                    _flyout = CreateSettingsFlyout();
                }
                return _flyout;
            }
        }

        private static Flyout CreateSettingsFlyout()
        {
            var settings = new Flyout();
            settings.Position = Position.Left;
            Panel.SetZIndex(settings, 100);
            settings.Header = "HDT QoL Settings";
            settings.Content = new SettingsWindow();
            Core.MainWindow.Flyouts.Items.Add(settings);
            return settings;
        }

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

        private void ToggleEnableAlternateText(object sender, RoutedEventArgs e)
        {
            if (CheckboxEnableAlternateText.IsChecked == true)
            {
                MainHandler.IsAlternateTextEnabled = true;
            }
            else
            {
                MainHandler.IsAlternateTextEnabled = false;
            }
        }
    }
}
