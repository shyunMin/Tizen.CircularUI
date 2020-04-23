using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyTargetPage : CirclePage
    {
        public DailyTargetPage()
        {
            InitializeComponent();
            //BindingContext = new DailyTargetViewModel();
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

        void OnActivated(object sender, EventArgs args)
        {
            unitButton.Opacity = 0;
        }

        void OnDeactivated(object sender, EventArgs args)
        {
            unitButton.Opacity = 1;
        }
    }
}