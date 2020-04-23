using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileSettingPage : CirclePage
    {
        public ProfileSettingPage()
        {
            InitializeComponent();
        }

        void OnStepper1Focused(object sender, EventArgs args)
        {
            page.RotaryFocusObject = stepper1;
        }

        void OnStepper2Focused(object sender, EventArgs args)
        {
            page.RotaryFocusObject = stepper2;
        }

        void OnActivated(object sender, EventArgs args)
        {
            unitButton2.Opacity = 0;
            unitButton2.IsEnabled = false;
        }

        void OnDeactivated(object sender, EventArgs args)
        {
            unitButton2.Opacity = 1;
            unitButton2.IsEnabled = true;
        }

        void OnUnitButtonPressed(object sender, EventArgs e)
        {
            //unitButton2.Scale = 1.5;
            unitButtonBg.Opacity = 0.15;
        }

        void OnUnitButtonReleased(object sender, EventArgs e)
        {
            unitButtonBg.Opacity = 0;
        }

        void OnUnitButtonClicked(object sender, EventArgs e)
        {
            //change unit
        }
        void OnButtonPressed(object sender, EventArgs e)
        {
            buttonBg.Opacity = 0.25;
        }

        void OnButtonReleased(object sender, EventArgs e)
        {
            buttonBg.Opacity = 0.5;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            //go to previous page
        }

    }
}