using System;
using System.Windows.Controls;

using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.API;

namespace HDT_QoL
{
    public class MainPlugin : IPlugin
    {
        private MainOverlay _overlay;

        public string Name => "HDT_QoL";

        public string Description => "Some quality of life improvements for HDT.";

        public string ButtonText => "Settings";

        public string Author => "Lesterberne";

        public Version Version => new Version(0, 0, 1);

        public MenuItem MenuItem => CreateMenu();

        private MenuItem CreateMenu()
        {
            return new MenuItem { Header = "HDT_QoL Settings" }; ;
        }

        public void OnLoad()
        {
            _overlay = new MainOverlay();

            MainHandler.Overlay = _overlay;

            Core.OverlayCanvas.Children.Add(_overlay);
            Canvas.SetTop(_overlay, 0);
            Canvas.SetRight(_overlay, 410);

            GameEvents.OnGameStart.Add(MainHandler.GameStart);
            GameEvents.OnGameEnd.Add(MainHandler.GameEnd);
            GameEvents.OnTurnStart.Add(MainHandler.TurnStart);
        }

        public void MountOverlay()
        {

        }

        public void UnmountOverlay()
        {
            
        }

        public void OnUnload()
        {
            Core.OverlayCanvas.Children.Remove(_overlay);
        }

        public void OnButtonPress()
        {

        }

        public void OnUpdate()
        {

        }

        public void SetWindowLeft()
        {

        }
    }
}
