using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewFooterTest1 : ContentPage
    {
        public class MyData
        {
            public string Text { get; set; }
        }

        public ListViewFooterTest1()
        {
            var myDatas = new List<MyData>();

            for (int i = 1; i <= 20; ++i)
            {
                myDatas.Add(new MyData() { Text = string.Format("{0}th list item", i) });
            }

            var listView = new ListView()
            {
                FooterTemplate = new DataTemplate(() =>
                {
                    var label = new Label()
                    {
                        Text = "Footer test",
                        TextColor = Color.Black,
                        BackgroundColor = Color.Lime,
                        MinimumHeightRequest = 100,
                    };
                    label.SetBinding(Label.TextProperty, new Binding("Text"));

                    var layout = new StackLayout
                    {
                        Children =
                        {
                            label,
                            new BoxView() { Color = Color.Yellow }
                        }
                    };
                    return layout;
                }),

                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new TextCell();
                    cell.SetBinding(TextCell.TextProperty, new Binding("Text"));
                    cell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(cell.AutomationId))
                            cell.AutomationId = cell.Text;
                    };
                    return cell;
                }),
                ItemsSource = myDatas,
                AutomationId = "list"
            };

            listView.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem != null)
                    listView.Footer = e.SelectedItem;
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
