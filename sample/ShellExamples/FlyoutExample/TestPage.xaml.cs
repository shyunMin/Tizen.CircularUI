using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(testImage, Color.Blue);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(testImage2, Color.Red);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(testImage3, Color.Green);

            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(border, Color.Blue);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(button, Color.Green);
        }
    }
}