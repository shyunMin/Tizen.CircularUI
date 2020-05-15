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
	public partial class CircleStepperTest : CirclePage
	{
		public CircleStepperTest()
		{
			InitializeComponent();
		}

		public void OnWheelAppeared(object sender, EventArgs args)
		{
			Console.WriteLine($"+++++ OnWheelAppeared");
		}

		public void OnWheelDisappeared(object sender, EventArgs args)
		{
			Console.WriteLine($"+++++ OnWheelDisappeared");
		}
	}
}