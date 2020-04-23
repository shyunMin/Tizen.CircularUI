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
	public partial class SimplePage : ContentPage
	{
		public SimplePage()
		{
			InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                TitleLabel.Text = Shell.Current.CurrentState.Location.ToString();
            });
        }
    }
}