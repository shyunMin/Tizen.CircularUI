using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AAASimplePage : CirclePage
	{
		public AAASimplePage()
		{
			InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                PageTitle.Text = Shell.Current.CurrentState.Location.ToString();
            });
        }

        void OnClicked(object sender, EventArgs args)
        {
            Console.WriteLine($"##### clicked!!!");
            Shell.Current.GoToAsync("test");
        }
    }
}