using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest14 : ContentPage
    {
        static ObservableCollection<MyData> myList = new ObservableCollection<MyData>();
        static MyData selected;
        //static Cell selected;

        public ListViewTest14()
        {
            for (var i = 0; i < 50; i++)
            {
                myList.Add(new MyData { Enabled = true, Title = string.Format("{0} item", i) });
            }

            Debug.WriteLine("$$$$$$$$$$$$$$$$$$$$$ " + myList[0].Title);

            Button enableButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Enable",
                AutomationId = "enableButton"
            };

            Button disableButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Disable",
                AutomationId = "disableButton"
            };

            ListView list = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new ViewCell();

                    var label = new Label
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                    };
                    label.SetBinding(Label.TextProperty, new Binding("Title"));
                    var check = new Xamarin.Forms.Switch();
                    var view = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            label,
                            check
                        }
                    };
                    cell.SetBinding(Cell.IsEnabledProperty, new Binding("Enabled"));
                    cell.View = view;
                    cell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(cell.AutomationId) && (!String.IsNullOrEmpty(label.Text)))
                            cell.AutomationId = label.Text;
                    };
                    return cell;
                }),
                ItemsSource = myList
            };

            StackLayout layout = new StackLayout
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        Spacing = 20,
                        Children =
                        {
                            enableButton,
                            disableButton,
                        }
                    },
                    list,
                },
            };

            list.ItemTapped += (s, e) =>
            {
                selected = e.Item as MyData;
                Debug.WriteLine("################# " + s.GetType().ToString());  //listview
                Debug.WriteLine("################# " + e.Item.GetType().ToString()); //MyData
                Debug.WriteLine("@@@@@@@@@@@@@@@@@ " + list.SelectedItem.GetType().ToString()); //MyData
            };

            enableButton.Clicked += (s, e) =>
            {
                if (selected != null)
                {
                    selected.Enabled = true;
                }
            };

            disableButton.Clicked += (s, e) =>
            {
                if (selected != null)
                {
                    selected.Enabled = false;
                }
            };

            // Build the page.
            this.Content = layout;
        }

        class MyData : BindableObject
        {
            public static readonly BindableProperty EnabledProperty = BindableProperty.Create("Enabled", typeof(bool), typeof(MyData), true);

            public bool Enabled
            {
                get { return (bool)GetValue(EnabledProperty); }
                set { SetValue(EnabledProperty, value); }
            }

            public string Title { get; set; }
        }
    }
}
