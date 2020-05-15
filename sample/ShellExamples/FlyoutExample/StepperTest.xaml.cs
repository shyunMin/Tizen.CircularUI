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
	public partial class StepperTest : CirclePage
	{
		public StepperTest()
		{
			InitializeComponent();
			GetValue();
		}

		public void GetValue()
		{
			Console.WriteLine($"@@@@@@@@@@@@@@@@@@@@@ stepper.value {stepper.Value}");
		}
    }
}