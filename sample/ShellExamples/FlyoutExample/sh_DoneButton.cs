using System;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class DoneButton : ContentButton
    {
        AbsoluteLayout _layout;
        Image _bg;
        Label _label;
        string _text;
        FormattedString _formattedText;
        Color _pressedColor;
        Color _releasedColor;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public FormattedString FormattedText
        {
            get
            {
                return _formattedText;
            }
            set
            {
                _formattedText = value;
            }
        }

        public Color PressedColor
        {
            get
            {
                return _pressedColor;
            }
            set
            {
                _pressedColor = value;
            }
        }

        public Color ReleasedColor
        {
            get
            {
                return _releasedColor;
            }
            set
            {
                _releasedColor = value;
            }
        }

        public DoneButton()
        {
            Initialize();
        }

        void Initialize()
        {
            _layout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            _bg = new Image
            {
                Source = ImageSource.FromFile("w_13_0_daily_activity_bg.png")
            };

            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(_bg, _releasedColor);

            AbsoluteLayout.SetLayoutBounds(_bg, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_bg, AbsoluteLayoutFlags.SizeProportional);

            _label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = _text,
                FormattedText = _formattedText
            };

            AbsoluteLayout.SetLayoutBounds(_label, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_label, AbsoluteLayoutFlags.SizeProportional);

            _layout.Children.Add(_bg);
            _layout.Children.Add(_label);

            Content = _layout;

            Pressed += OnPressed;
            Released += OnReleased;
        }

        void OnPressed(object sender, EventArgs args)
        {
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(_bg, _pressedColor);
        }

        void OnReleased(object sender, EventArgs args)
        {
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(_bg, _releasedColor);
        }
    }
}
