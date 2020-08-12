using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        public static bool IsBorderEnabled
        {
            get { return _isBorderEnabled; }
            set
            {
                _isBorderEnabled = value;
                if (_isBorderEnabled)
                {
                    EnableBorder();
                }
                else
                {
                    DisableBorder();
                }
            }
        }

        public static bool IsColorsEnabled
        {
            get { return _isColorsEnabled; }
            set
            {
                _isColorsEnabled = value;
                if (_isColorsEnabled)
                {
                    EnableColors();
                }
                else
                {
                    DisableColors();
                }
            }
        }

        public static bool IsAlternateTextEnabled
        {
            get { return _isAlternateTextEnabled; }
            set
            {
                _isAlternateTextEnabled = value;
                if (_isAlternateTextEnabled)
                {
                    EnableAlternateText();
                }
                else
                {
                    DisableAlternateText();
                }
            }
        }

        public static bool IsScaleWithWindowEnabled
        {
            get { return _isScaleWithWindowEnabled; }
            set
            {
                _isScaleWithWindowEnabled = value;
                if (_isScaleWithWindowEnabled)
                {
                    Overlay.ApplyAutoScaling();
                }
            }
        }

        public static MainOverlay Overlay;
        public static Guid GameID = Guid.Empty;
        public static bool IsBattlegroundsMode;
        public static bool IsMissingTribeRetrieved;
        public static bool _isBannedTribeEnabled = Properties.Settings.Default.IsBannedTribeEnabled;
        public static bool _isScaleWithWindowEnabled = Properties.Settings.Default.IsScaleWithWindowEnabled;
        public static bool _isBorderEnabled = Properties.Settings.Default.IsBorderEnabled;
        public static bool _isColorsEnabled = Properties.Settings.Default.IsColorsEnabled;
        public static bool _isAlternateTextEnabled = Properties.Settings.Default.IsAlternateTextEnabled;
        public static int TurnNumber;

        public static InputManager Input;

        internal static void GameStart()
        {
            GameID = Core.Game.CurrentGameStats.GameId;
            IsBattlegroundsMode = Core.Game.CurrentGameMode == GameMode.Battlegrounds;
            IsMissingTribeRetrieved = false;
            TurnNumber = 0;

            int waitTime = 30000;

            while (!IsMissingTribeRetrieved && waitTime > 0)
            {
                Thread.Sleep(1500);
                waitTime -= 1500;
                IsMissingTribeRetrieved = RetrieveMissingTribe();
            }

            if (waitTime == 0)
            {
                ResetBannedTribeOverlay();
            }

            HandleSizeChangeEvent(null, null);
        }

        internal static void HandleSizeChangeEvent(object sender, SizeChangedEventArgs e)
        {
            if (IsScaleWithWindowEnabled)
            {
                Overlay.ApplyAutoScaling();
            }
        }

        internal static void GameEnd()
        {
            GameID = Guid.Empty;
            IsBattlegroundsMode = false;
            ResetBannedTribeOverlay();
        }

        internal static void TurnStart(ActivePlayer player)
        {
            TurnNumber = Core.Game.GetTurnNumber();

            if (IsBattlegroundsMode)
            {
                if (TurnNumber == 1)
                {
                    Overlay.BannedTribeBorder.BorderThickness = new Thickness(0);
                }
            }
        }

        internal static void ResetOverlay()
        {
            Properties.Settings.Default.OverlayTop = 0;
            Properties.Settings.Default.OverlayLeft = 0;
            Properties.Settings.Default.OverlayOpacity = 100;
            Overlay.ApplyAutoScaling();
            Canvas.SetTop(Overlay, 0);
            Canvas.SetLeft(Overlay, 0);
        }

        internal static void SetBannedTribeOverlay(string missingTribe)
        {
            Overlay.BannedTribeOverlay.UpdateTribe(missingTribe, IsAlternateTextEnabled);

            if (IsBannedTribeEnabled)
            {
                EnableBannedTribeOverlay();
            }

            if (IsBorderEnabled)
            {
                Overlay.BannedTribeBorder.BorderThickness = new Thickness(5);
            }

            if (IsColorsEnabled)
            {
                EnableColors();
            }
            else
            {
                DisableColors();
            }
        }

        internal static void ResetBannedTribeOverlay()
        {
            DisableBannedTribeOverlay();
            Overlay.BannedTribeOverlay.UpdateTribe("N/A", IsAlternateTextEnabled);
            DisableColors();
        }

        internal static void EnableBannedTribeOverlay()
        {
            Overlay.BannedTribeBorder.Visibility = Visible;
        }

        internal static void DisableBannedTribeOverlay()
        {
            Overlay.BannedTribeBorder.Visibility = Collapsed;
        }

        internal static void EnableBorder()
        {
            if (TurnNumber == 0)
            {
                Overlay.BannedTribeBorder.BorderThickness = new Thickness(5);
            }
        }

        internal static void DisableBorder()
        {
            Overlay.BannedTribeBorder.BorderThickness = new Thickness(0);
        }

        internal static void EnableColors()
        {
            if (GameID != Guid.Empty && GetTribeName(GetMissingTribe(GameID)) != null)
            {
                Overlay.BannedTribeOverlay.BorderBannedTribeText.Background = GetTribeColor(GetMissingTribe(GameID));
            }
        }

        internal static void DisableColors()
        {
            Overlay.BannedTribeOverlay.BorderBannedTribeText.Background = (Brush)(new BrushConverter().ConvertFrom("#23272A"));
        }

        internal static void EnableAlternateText()
        {
            if (GameID != Guid.Empty && GetTribeName(GetMissingTribe(GameID)) != null)
            {
                Overlay.BannedTribeOverlay.UpdateTribe(GetTribeName(GetMissingTribe(GameID)), IsAlternateTextEnabled);
            }
        }

        internal static void DisableAlternateText()
        {
            if (GameID != Guid.Empty && GetTribeName(GetMissingTribe(GameID)) != null)
            {
                Overlay.BannedTribeOverlay.UpdateTribe(GetTribeName(GetMissingTribe(GameID)), IsAlternateTextEnabled);
            }
        }

        internal static bool RetrieveMissingTribe()
        {
            if (GameID != Guid.Empty)
                return false;

            int tribeID = GetMissingTribe(GameID);
            string missingTribe = GetTribeName(tribeID);

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

        internal static Brush GetTribeColor(int tribeID)
        {
            switch (tribeID)
            {
                case 14:
                    return (Brush)(new BrushConverter().ConvertFrom("#A6A608")); // Yellow - Murlocs
                case 15:
                    return (Brush)(new BrushConverter().ConvertFrom("#800080")); // Purple - Demons
                case 17:
                    return (Brush)(new BrushConverter().ConvertFrom("#8A8A8A")); // Grey - Mechs
                case 20:
                    return (Brush)(new BrushConverter().ConvertFrom("#1D7A1D")); // Green - Beasts
                case 23:
                    return (Brush)(new BrushConverter().ConvertFrom("#960A00")); // Red - Pirates
                case 24:
                    return (Brush)(new BrushConverter().ConvertFrom("#0070A6")); // Blue - Dragons
                default:
                    return null;
            }
        }
    }
}