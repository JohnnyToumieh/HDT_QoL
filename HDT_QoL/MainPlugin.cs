using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.API;

namespace HDT_QoL
{
    public class MainPlugin : IPlugin
    {
        private MainOverlay _overlay;
        private InputManager _inputManager;

        public string Name => "HDT_QoL";

        public string Description => "Some quality of life improvements for HDT.";

        public string ButtonText => "Settings";

        public string Author => "Lesterberne";

        public Version Version => new Version(0, 0, 12);

        public MenuItem MenuItem => null;

        public void OnButtonPress() => SettingsWindow.Flyout.IsOpen = true;

        public void OnLoad()
        {
            _overlay = new MainOverlay();
            MainHandler.Overlay = _overlay;

            _inputManager = new InputManager(_overlay);
            MainHandler.Input = _inputManager;
            
            Core.OverlayCanvas.Children.Add(_overlay);

            Canvas.SetZIndex(_overlay, -100);
            Canvas.SetTop(_overlay, Properties.Settings.Default.OverlayTop);
            Canvas.SetLeft(_overlay, Properties.Settings.Default.OverlayLeft);

            GameEvents.OnGameStart.Add(MainHandler.GameStart);
            GameEvents.OnGameEnd.Add(MainHandler.GameEnd);
            GameEvents.OnTurnStart.Add(MainHandler.TurnStart);

            Core.OverlayWindow.SizeChanged += new SizeChangedEventHandler(MainHandler.HandleSizeChangeEvent);

            Properties.Settings.Default.PropertyChanged += SettingsChanged;
            SettingsChanged(null, null);
        }

        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _overlay.RenderTransform = new ScaleTransform(Properties.Settings.Default.OverlayScale / 100, Properties.Settings.Default.OverlayScale / 100);
            _overlay.Opacity = Properties.Settings.Default.OverlayOpacity / 100;
        }

        public void MountOverlay()
        {

        }

        public void UnmountOverlay()
        {

        }

        public void OnUnload()
        {
            Properties.Settings.Default.Save();

            Core.OverlayCanvas.Children.Remove(_overlay);
            _inputManager.Dispose();
        }

        public void OnUpdate()
        {

        }

        public void SetWindowLeft()
        {

        }
    }
}
