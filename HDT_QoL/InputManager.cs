using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Hearthstone_Deck_Tracker;
using Core = Hearthstone_Deck_Tracker.API.Core;

namespace HDT_QoL
{
    public class InputManager
    {
        private User32.MouseInput _mouseInput;
        private MainOverlay _overlay;

        private String _selected;

        public InputManager(MainOverlay overlay)
        {
            _overlay = overlay;
        }

        public bool Toggle()
        {
            if (Hearthstone_Deck_Tracker.Core.Game.IsRunning && _mouseInput == null)
            {
                _mouseInput = new User32.MouseInput();
                _mouseInput.LmbDown += MouseInputOnLmbDown;
                _mouseInput.LmbUp += MouseInputOnLmbUp;
                _mouseInput.MouseMoved += MouseInputOnMouseMoved;
                return true;
            }
            Dispose();
            return false;
        }

        public void Dispose()
        {
            _mouseInput?.Dispose();
            _mouseInput = null;
        }

        private void MouseInputOnLmbDown(object sender, EventArgs eventArgs)
        {
            var pos = User32.GetMousePos();
            var _mousePos = new Point(pos.X, pos.Y);
            if (PointInsideControl(_mousePos, _overlay))
            {
                _selected = "overlay";
            }
        }

        private void MouseInputOnLmbUp(object sender, EventArgs eventArgs)
        {
            var pos = User32.GetMousePos();
            var p = Core.OverlayCanvas.PointFromScreen(new Point(pos.X, pos.Y));
            if (_selected == "overlay")
            {
                Properties.Settings.Default.OverlayTop = p.Y;
                Properties.Settings.Default.OverlayLeft = p.X;
            }

            _selected = null;
        }

        private void MouseInputOnMouseMoved(object sender, EventArgs eventArgs)
        {
            if (_selected == null)
            {
                return;
            }

            var pos = User32.GetMousePos();
            var p = Core.OverlayCanvas.PointFromScreen(new Point(pos.X, pos.Y));
            if (_selected == "overlay")
            {
                Canvas.SetTop(_overlay, p.Y);
                Canvas.SetLeft(_overlay, p.X);
            }
        }

        private bool PointInsideControl(Point p, FrameworkElement control)
        {
            var pos = control.PointFromScreen(p);
            return pos.X > 0 && pos.X < control.ActualWidth && pos.Y > 0 && pos.Y < control.ActualHeight;
        }
    }
}