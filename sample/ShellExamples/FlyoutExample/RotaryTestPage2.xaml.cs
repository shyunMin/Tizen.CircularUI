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
    public partial class RotaryTestPage2 : ContentPage
    {
        public RotaryTestPage2()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.View.SetUseRotaryInteraction(stepper1, false);
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.View.SetUseRotaryInteraction(stepper2, false);
        }
    }
}