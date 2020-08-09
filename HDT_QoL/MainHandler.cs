using System;
using System.Threading;
using static System.Windows.Visibility;

using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;
using System.Windows;

namespace HDT_QoL
{
    public class MainHandler
    {
        public static MainOverlay Overlay;
        public static Guid GameID;
        public static bool IsBattlegroundsMode;
        public static bool IsMissingTribeRetrieved;

        internal static void GameStart()
        {
            GameID = Core.Game.CurrentGameStats.GameId;
            IsBattlegroundsMode = Core.Game.CurrentGameMode == GameMode.Battlegrounds;
            IsMissingTribeRetrieved = false;

            int waitTime = 15000;

            while (!IsMissingTribeRetrieved && waitTime > 0)
            {
                Thread.Sleep(1500);
                waitTime -= 1500;
                IsMissingTribeRetrieved = RetrieveMissingTribe();
            }
        }

        internal static void GameEnd()
        {
            GameID = Guid.Empty;
            DisableOverlays();
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

        internal static void EnableOverlays(string missingTribe)
        {
            Overlay.BannedTribeOverlay.UpdateTribe(missingTribe);
            Overlay.BannedTribeBorder.BorderThickness = new Thickness(5);
            Overlay.BannedTribeBorder.Visibility = Visible;
        }

        internal static void DisableOverlays()
        {
            Overlay.BannedTribeBorder.Visibility = Collapsed;
            Overlay.BannedTribeOverlay.UpdateTribe("N/A");
        }

        internal static bool RetrieveMissingTribe()
        {
            string missingTribe = GetTribeName(GetMissingTribe(GameID));

            if (missingTribe != null)
            {
                EnableOverlays(missingTribe);
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