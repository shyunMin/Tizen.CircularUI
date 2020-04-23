using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest12 : ContentPage
    {
        public ListViewTest12()
        {
            var myData = new List<TextCell>();
            for (int i = 0; i < 100; ++i)
            {
                myData.Add(new TextCell() { Text = string.Format("{0} List Item", i) });
            }

            ListView listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = myData,
                ItemTemplate = new DataTemplate(() =>
                {
                    TextCell cell = new TextCell();
                    cell.SetBinding(TextCell.TextProperty, new Binding("Text"));
                    cell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(cell.AutomationId))
                            cell.AutomationId = cell.Text;
                    };
                    return cell;
                }),
                AutomationId = "listView"
            };

            // Build the page.
            this.Content = listView;
        }
    }
}
