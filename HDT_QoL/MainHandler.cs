using System;
using System.Threading;
using System.Windows;
using static System.Windows.Visibility;

using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;

namespace HDT_QoL
{
    public class MainHandler
    {
        public static bool IsBannedTribeEnabled
        {
            get { return _isBannedTribeEnabled; }
            set
            {
                _isBannedTribeEnabled = value;
                if (_isBannedTribeEnabled)
                {
                    if (IsBattlegroundsMode)
                    {
                        EnableBannedTribeOverlay();
                    }
                }
                else
                {
                    DisableBannedTribeOverlay();
                }
            }
        }

        public static MainOverlay Overlay;
        public static Guid GameID;
        public static bool IsBattlegroundsMode;
        public static bool IsMissingTribeRetrieved;
        public static bool _isBannedTribeEnabled = Properties.Settings.Default.IsBannedTribeEnabled;

        public static InputManager Input;

        internal static void GameStart()
        {
            GameID = Core.Game.CurrentGameStats.GameId;
            IsBattlegroundsMode = Core.Game.CurrentGameMode == GameMode.Battlegrounds;
            IsMissingTribeRetrieved = false;

            int waitTime = 30000;

            while (!IsMissingTribeRetrieved && waitTime > 0)
            {
                Thread.Sleep(1500);
                waitTime -= 1500;
                IsMissingTribeRetrieved = RetrieveMissingTribe();
            }

            Overlay.ApplyAutoScaling();
        }

        internal static void GameEnd()
        {
            GameID = Guid.Empty;
            IsBattlegroundsMode = false;
            ResetBannedTribeOverlay();
        }

        internal static void TurnStart(ActivePlayer player)
        {
            if (IsBattlegroundsMode)
            {
                if (Core.Game.GetTurnNumber() == 1)
                {
                    Overlay.BannedTribeBorder.BorderThickness = new Thickness(0);
                }
            }
        }

        internal static void SetBannedTribeOverlay(string missingTribe)
        {
            Overlay.BannedTribeOverlay.UpdateTribe(missingTribe);
            Overlay.BannedTribeBorder.BorderThickness = new Thickness(5);
            if (IsBannedTribeEnabled)
            {
                EnableBannedTribeOverlay();
            }
        }

        internal static void ResetBannedTribeOverlay()
        {
            DisableBannedTribeOverlay();
            Overlay.BannedTribeOverlay.UpdateTribe("N/A");
        }

        internal static void EnableBannedTribeOverlay()
        {
            Overlay.BannedTribeBorder.Visibility = Visible;
        }

        internal static void DisableBannedTribeOverlay()
        {
            Overlay.BannedTribeBorder.Visibility = Collapsed;
        }

        internal static bool RetrieveMissingTribe()
        {
            string missingTribe = GetTribeName(GetMissingTribe(GameID));

            if (missingTribe != null)
            {
                SetBannedTribeOverlay(missingTribe);
                return true;
            }

            return false;
        }

        internal static int GetMissingTribe(Guid? gameId)
        {
            var tribes = BattlegroundsUtils.GetAvailableRaces(gameId);
            var total = 113;

            foreach (var tribe in tribes)
            {
                total -= (int)tribe;
            }

            return total;
        }

        internal static string GetTribeName(int tribeID)
        {
            switch (tribeID)
            {
                case 14:
                    return "Murlocs";
                case 15:
                    return "Demons";
                case 17:
                    return "Mechs";
                case 20:
                    return "Beasts";
                case 23:
                    return "Pirates";
                case 24:
                    return "Dragons";
                default:
                    return null;
            }
        }
    }
}