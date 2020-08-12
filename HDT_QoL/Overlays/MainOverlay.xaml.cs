using System;
using System.Windows.Controls;

using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Windows;
using Hearthstone_Deck_Tracker.API;

namespace HDT_QoL
{
    /// <summary>
    /// Interaction logic for MainOverlay.xaml
    /// </summary>
    public partial class MainOverlay : UserControl
    {
        private OverlayElementBehavior _bgsTopBar2Behavior;
        public double AutoScaling { get; set; } = Properties.Settings.Default.OverlayScale / 100;

        public MainOverlay()
        {
            InitializeComponent();

            _bgsTopBar2Behavior = new OverlayElementBehavior(BgsTopBar2)
            {
                GetScaling = () => AutoScaling,
                AnchorSide = Side.Top,
                EntranceAnimation = AnimationType.Slide,
                ExitAnimation = AnimationType.Slide,
            };
        }

        public void ApplyAutoScaling()
        {
            AutoScaling = Math.Max(0.8, Math.Min(1.3, Core.OverlayWindow.Height / 1080));

            Properties.Settings.Default.OverlayScale = AutoScaling * 100;

            _bgsTopBar2Behavior.UpdateScaling();
            _bgsTopBar2Behavior.UpdatePosition();
        }
    }
}