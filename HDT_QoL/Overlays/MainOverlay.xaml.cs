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
        public double AutoScaling { get; set; } = 1;
        public double OverlayHeight = 0;

        public MainOverlay()
        {
            InitializeComponent();

            _bgsTopBar2Behavior = new OverlayElementBehavior(BgsTopBar2)
            {
                GetLeft = () => 0,
                GetTop = () => 0,
                GetScaling = () => AutoScaling,
                AnchorSide = Side.Top,
                EntranceAnimation = AnimationType.Slide,
                ExitAnimation = AnimationType.Slide,
            };
        }

        public void ApplyAutoScaling()
        {
            if (OverlayHeight != Core.OverlayWindow.Height)
            {
                OverlayHeight = Core.OverlayWindow.Height;
                AutoScaling = Math.Max(0.8, Math.Min(1.3, OverlayHeight / 1080));

                _bgsTopBar2Behavior.UpdateScaling();
                _bgsTopBar2Behavior.UpdatePosition();
            }
        }
    }
}