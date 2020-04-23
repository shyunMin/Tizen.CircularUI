using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using ElmSharp;
using FlyoutExample;
using System.ComponentModel;
using EBox = ElmSharp.Box;
using EColor = ElmSharp.Color;

namespace FlyoutExample
{
    public class ViewButton : ContentView, IButtonController
    {
        View _view;

        public View View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
                base.Content = _view;
            }
        }

       public ViewButton() : base()
        {
        }

        public event EventHandler Clicked;
        public event EventHandler Pressed;
        public event EventHandler Released;

        public void SendClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void SendPressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        public void SendReleased()
        {
            Released?.Invoke(this, EventArgs.Empty);
        }
    }
}
