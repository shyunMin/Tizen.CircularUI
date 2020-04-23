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
    public partial class CircleListViewPage : ContentPage
    {
        public CircleListViewPage()
        {
            InitializeComponent();

            var list = new List<string>();
            list.Add("c - aaa");
            list.Add("c - bbb");
            list.Add("c - ccc");
            list.Add("c - ddd");
            list.Add("c - eee");
            list.Add("c - fff");
            list.Add("c - ggg");

            this.BindingContext = list;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}