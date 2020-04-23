using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateOfBirthPage : CirclePage
    {
        public DateOfBirthPage()
        {
            InitializeComponent();
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