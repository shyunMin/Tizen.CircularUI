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
    public partial class ButtonTestPage : ContentPage
    {
        public ButtonTestPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBorder, Color.DarkGreen);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Green);
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            // run action for clicked evnet
        }
        private void OnButtonPressed(object sender, EventArgs e)
        {
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Gray);
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Green);
        }
    }
}