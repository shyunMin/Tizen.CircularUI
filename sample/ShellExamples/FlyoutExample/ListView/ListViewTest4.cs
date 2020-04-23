using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest4 : ContentPage
    {
        public ListViewTest4()
        {
            var myData = new List<MyGroup>
            {
                new MyGroup("group1") { "item1-1", "item1-2", "item1-3", "item1-4", "item1-5" },
                new MyGroup("group2") { "item2-1", "item2-2", "item2-3", "item2-4", "item2-5" },
                new MyGroup("group3") { "item3-1", "item3-2", "item3-3", "item3-4", "item3-5" },
            };

            ListView list = new ListView()
            {
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("Name"),
                ItemsSource = myData,
                AutomationId = "list"
            };

            list.ItemSelected += (s, e) =>
            {
                var item = e.SelectedItem;
            };

            var setButton = new Button()
            {
                Text = "Sellect item1-3 in group1",
                AutomationId = "setButton"
            };

            var setButton2 = new Button()
            {
                Text = "Sellect group2",
                AutomationId = "setButton2"
            };

            var setButton3 = new Button()
            {
                Text = "Sellect group2-1",
                AutomationId = "setButton3"
            };

            setButton.Clicked += (s, e) =>
            {
                list.SelectedItem = myData[0][2];
            };

            setButton2.Clicked += (s, e) =>
            {
                list.SelectedItem = myData[1]; // group header cannot selected in Xamarin
            };

            setButton3.Clicked += (s, e) =>
            {
                list.SelectedItem = myData[1][0];
            };

            // Build the page.
            if (Device.Idiom == TargetIdiom.Watch)
            {
                this.Content = new StackLayout
                {
                    Children =
                    {
                        new ScrollView
                        {
                            Orientation = ScrollOrientation.Horizontal,
                            Content = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    setButton,
                                    setButton2,
                                    setButton3
                                }
                            }
                        },
                        list
                    }
                };
            }
            else
            {
                this.Content = new StackLayout
                {
                    Children =
                    {
                        list,
                        setButton,
                        setButton2,
                        setButton3
                    }
                }; ;
            }
        }
        class MyGroup : List<string>
        {
            public string Name { get; set; }
            public MyGroup(string name) { Name = name; }
        }
    }
}
