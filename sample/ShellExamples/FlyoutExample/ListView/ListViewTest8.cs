using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest8 : ContentPage
    {
        static ListView list;

        public ListViewTest8()
        {
            Button button = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Text = "Create or Remove ListView",
                AutomationId = "button"
            };

            StackLayout layout = new StackLayout
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Children =
            {
                button,
            },
            };

            var myData = new List<string>();
            for (int i = 0; i < 1000; ++i)
            {
                myData.Add(string.Format("item {0}", i));
            }

            button.Clicked += (sender, e) =>
            {
                if (list == null)
                {
                    list = new ListView
                    {
                        ItemsSource = myData,
                        AutomationId = "list"
                    };
                    layout.Children.Add(list);
                }
                else
                {
                    layout.Children.Remove(list);
                    list = null;
                }
            };

            // Build the page.
            this.Content = layout;
        }
    }
}
