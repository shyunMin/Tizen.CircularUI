using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest13 : ContentPage
    {
        static ObservableCollection<MyData> myList = new ObservableCollection<MyData>();
        static MyData selected;
        //static Cell selected;

        public ListViewTest13()
        {
            for (var i = 0; i < 50; i++)
            {
                myList.Add(new MyData { IsEnabled = true, Title = string.Format("{0} item", i) });
            }
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
                    var cell = new SwitchCell();
                    cell.SetBinding(SwitchCell.TextProperty, new Binding("Title"));
                    cell.SetBinding(SwitchCell.OnProperty, new Binding("IsEnabled", BindingMode.TwoWay));
                    cell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(cell.AutomationId))
                            cell.AutomationId = cell.Text;
                    };
                    return cell;
                }),
                ItemsSource = myList,
                AutomationId = "list"
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
                    selected.IsEnabled = true;
                }
            };

            disableButton.Clicked += (s, e) =>
            {
                if (selected != null)
                {
                    selected.IsEnabled = false;
                }
            };

            // Build the page.
            this.Content = layout;
        }

        class MyData : BindableObject
        {
            public static readonly BindableProperty EnabledProperty = BindableProperty.Create("IsEnabled", typeof(bool), typeof(MyData), true);

            public bool IsEnabled
            {
                get { return (bool)GetValue(EnabledProperty); }
                set { SetValue(EnabledProperty, value); }
            }

            public string Title { get; set; }
        }
    }
}
