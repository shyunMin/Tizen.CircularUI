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
    public partial class ListViewPage : ContentPage
    {
        public ListViewPage()
        {
            InitializeComponent();

            var list = new List<string>();
            list.Add("list aaa");
            list.Add("list bbb");
            list.Add("list ccc");
            list.Add("list ddd");
            list.Add("list eee");
            list.Add("list fff");
            list.Add("list ggg");

            this.BindingContext = list;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnScrolled(object sender, EventArgs args)
        {
            Console.WriteLine($"@@@ scolled");
        }

        void OnClicked(object sender, EventArgs args)
        {
            if (mylist.VerticalScrollBarVisibility == ScrollBarVisibility.Always)
            {
                mylist.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
            }
            else
            {
                mylist.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            }

            if (mylist.HorizontalScrollBarVisibility == ScrollBarVisibility.Always)
            {
                mylist.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
            }
            else
            {
                mylist.HorizontalScrollBarVisibility = ScrollBarVisibility.Always;
            }
        }

    }
}