using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest5 : ContentPage
    {
        public ListViewTest5()
        {
            var myData = new List<object>
            {
                new MyGroup("group1") { "item1-1", "item1-2", "item1-3", "item1-4", "item1-5" },
                new MyGroup("group2") { "item2-1", "item2-2", "item2-3", "item2-4", "item2-5" },
                new MyGroup("group3") { "item3-1", "item3-2", "item3-3", "item3-4", "item3-5" },
                new MyGroupGroup("group4")
                {
                    new MyGroup("group4-1") { "item4-1-1", "item4-1-2" },
                    new MyGroup("group4-2") { "item4-2-1", "item4-2-2" },
                }
            };

            Label label = new Label
            {
                Text = "Selected : ",
                AutomationId = "label"
            };

            Label pressLabel = new Label
            {
                Text = "Pressed : ",
                AutomationId = "pressLabel"
            };

            ListView list = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("Name"),
                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new TextCell();
                    cell.BindingContextChanged += (s, e) =>
                    {
                        cell.Text = cell.BindingContext?.ToString();
                        if (String.IsNullOrEmpty(cell.AutomationId))
                            cell.AutomationId = cell.Text;
                    };
                    return cell;
                }),
                ItemsSource = myData,
                AutomationId = "list"
            };

            StackLayout layout = new StackLayout
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    label,
                    pressLabel,
                    list,
                },
            };

            list.ItemSelected += (s, e) =>
            {
                label.Text = String.Format("Selected : {0}", e.SelectedItem);
            };

            object previousItem = null;
            int cnt = 0;

            list.ItemTapped += (s, e) =>
            {
                if (previousItem != e.Item)
                {
                    pressLabel.Text = String.Format("Pressed : {0}", e.Item);
                    cnt = 1;
                }
                else
                {
                    cnt++;
                    pressLabel.Text = String.Format("Pressed : {0} at {1} times", e.Item, cnt);
                }
                previousItem = e.Item;
            };

            // Build the page.
            this.Content = layout;
        }

        class MyGroup : List<string>
        {
            public string Name { get; set; }

            public MyGroup(string name)
            {
                Name = name;
            }
        }

        class MyGroupGroup : List<MyGroup>
        {
            public string Name { get; set; }

            public MyGroupGroup(string name)
            {
                Name = name;
            }
        }
    }
}
