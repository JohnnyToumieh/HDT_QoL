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

        private Point mousePos0;
        private Point overlayPos0;

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
            mousePos0 = new Point(pos.X, pos.Y);
            overlayPos0 = new Point(Properties.Settings.Default.OverlayLeft, Properties.Settings.Default.OverlayTop);

            if (PointInsideControl(mousePos0, _overlay))
            {
                _selected = "overlay";
            }
        }

        private void MouseInputOnLmbUp(object sender, EventArgs eventArgs)
        {
            var pos = User32.GetMousePos();

            if (_selected == "overlay")
            {
                Properties.Settings.Default.OverlayTop = overlayPos0.Y + (pos.Y - mousePos0.Y);
                Properties.Settings.Default.OverlayLeft = overlayPos0.X + (pos.X - mousePos0.X);
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

            if (_selected == "overlay")
            {
                Canvas.SetTop(_overlay, overlayPos0.Y + (pos.Y - mousePos0.Y));
                Canvas.SetLeft(_overlay, overlayPos0.X + (pos.X - mousePos0.X));
            }
        }

        private bool PointInsideControl(Point p, FrameworkElement control)
        {
            var pos = control.PointFromScreen(p);
            return pos.X > 0 && pos.X < control.ActualWidth && pos.Y > 0 && pos.Y < control.ActualHeight;
        }
    }
}