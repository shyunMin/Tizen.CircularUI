using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewHeaderTest1 : ContentPage
    {
        public class MyData
        {
            public string Text { get; set; }
        }

        public ListViewHeaderTest1()
        {
            var myDatas = new List<MyData>();

            for (int i = 1; i <= 20; ++i)
            {
                //myDatas.Add(new MyData() { Text = string.Format("list item #{0}", i) });
                myDatas.Add(new MyData() { Text = string.Format("list item {0}", i) });
            }

            var listView = new ListView()
            {
                HeaderTemplate = new DataTemplate(() =>
                {
                    var label = new Label()
                    {
                        Text = "Header test",
                        TextColor = Color.Green,
                        MinimumHeightRequest = 100,
                    };
                    label.SetBinding(Label.TextProperty, new Binding("Text"));

                    var layout = new StackLayout
                    {
                        Children =
                        {
                            new BoxView() { Color = Color.Red },
                            label,
                            new BoxView() { Color = Color.Blue },
                        }
                    };
                    return layout;
                }),

                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new TextCell();
                    cell.SetBinding(TextCell.TextProperty, new Binding("Text"));
                    return cell;
                }),
                ItemsSource = myDatas,
                AutomationId = "list"
            };

            listView.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem != null)
                    listView.Header = e.SelectedItem;
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Spacing = 10,
                Children =
                {
                    listView
                }
            };
        }
    }
}
