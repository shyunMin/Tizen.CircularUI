using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest7 : ContentPage
    {
        static ObservableCollection<object> myData = new ObservableCollection<object>();
        static object selected;
        static int clickCount;

        public ListViewTest7()
        {
            Button insertButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Insert",
                AutomationId = "insertButton"
            };

            Button removeButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Remove",
                AutomationId = "removeButton"
            };

            Button removeAllButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "RemoveAll",
                AutomationId = "removeAllButton"
            };

            ListView list = new ListView
            {
                ItemsSource = myData,
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
                AutomationId = "list"
            };

            StackLayout buttonLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Spacing = 20,
                Children =
                {
                    insertButton,
                    removeButton,
                    removeAllButton,
                }
            };

            if (Device.Idiom == TargetIdiom.Watch)
            {
                buttonLayout.Spacing = 0;
            }

            StackLayout layout = new StackLayout
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Children =
                {
                    buttonLayout,
                    list,
                },
            };

            list.ItemSelected += (s, e) =>
            {
                selected = e.SelectedItem;
            };

            insertButton.Clicked += (s, e) =>
            {
                clickCount++;
                string str = string.Format("Item added {0}", clickCount);

                if (selected == null)
                {
                    myData.Add(str);
                }
                else
                {
                    myData.Insert(myData.IndexOf(selected), str);
                }
            };

            removeButton.Clicked += (s, e) =>
            {
                if (selected != null)
                {
                    myData.Remove(selected);
                    selected = null;
                }
            };

            removeAllButton.Clicked += (s, e) =>
            {
                myData.Clear();
                selected = null;
            };

            // Build the page.
            this.Content = layout;
        }
    }
}
