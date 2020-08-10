using System;
using System.Windows.Controls;

using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Windows;

namespace HDT_QoL
{
    /// <summary>
    /// Interaction logic for MainOverlay.xaml
    /// </summary>
    public partial class MainOverlay : UserControl
    {
        private OverlayElementBehavior _bgsTopBar2Behavior;
        public double AutoScaling { get; set; } = 1;

        public MainOverlay()
        {
            InitializeComponent();

            _bgsTopBar2Behavior = new OverlayElementBehavior(BgsTopBar2)
            {
                GetRight = () => 0,
                GetTop = () => 0,
                GetScaling = () => AutoScaling,
                AnchorSide = Side.Top,
                EntranceAnimation = AnimationType.Slide,
                ExitAnimation = AnimationType.Slide,
            };
        }

        public void ApplyAutoScaling()
        {
            AutoScaling = Math.Max(0.8, Math.Min(1.3, System.Windows.SystemParameters.PrimaryScreenHeight / 1080));

            _bgsTopBar2Behavior.UpdateScaling();
            _bgsTopBar2Behavior.UpdatePosition();
        }
    }
}