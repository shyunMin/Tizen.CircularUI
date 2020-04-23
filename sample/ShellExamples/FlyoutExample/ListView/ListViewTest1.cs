using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class MyData
    {
        public int No { get; set; }
        public string Name { get; set; }
    }

    //public class MyObservableCollection : List<MyData>, INotifyCollectionChanged
    //{
    //    public MyObservableCollection()
    //    {
    //    }

    //    public event NotifyCollectionChangedEventHandler CollectionChanged;

    //    public void Add(MyData item)
    //    {
    //        base.Add(item);
    //        CollectionChanged?.Invoke(this,
    //            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    //    }

    //    public void InsertRange(int index, IEnumerable<MyData> collection)
    //    {
    //        base.InsertRange(index, collection);
    //        CollectionChanged?.Invoke(this,
    //            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<MyData>(collection), index));
    //    }

    //    public void Remove(MyData item)
    //    {
    //        base.Remove(item);
    //        CollectionChanged?.Invoke(this,
    //            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
    //    }
    //}

    public class ListViewTest1 : ContentPage
    {
        public ListViewTest1()
        {
            var myData = new List<MyData>();
            for (int i = 0; i < 3; ++i)
            {
                myData.Add(new MyData { No = i, Name = "list item" });
            }

            ListView listView = new ListView
            {
                HasUnevenRows = false,
                ItemsSource = myData,
                ItemTemplate = new DataTemplate(() =>
                {
                    var textcell = new TextCell();
                    textcell.SetBinding(TextCell.TextProperty, new Binding("No", stringFormat: "item {0}"));
                    textcell.SetBinding(TextCell.DetailProperty, "Name");

                    textcell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(textcell.AutomationId))
                            textcell.AutomationId = textcell.Text;
                    };

                    return textcell;
                }),
                AutomationId = "listView"
            };
            listView.BackgroundColor = Color.Purple;

            Button addBtn = new Button()
            {
                Text = "Append new item",
                AutomationId = "addBtn"
            };

            Button removeBtn = new Button()
            {
                Text = "Remove last item",
                AutomationId = "removeBtn"
            };

            addBtn.Clicked += (s, e) =>
            {
                var items = new List<MyData>();
                for (int t = 0; t < 2; t++)
                {
                    items.Add(new MyData()
                    {
                        No = myData.Count + t,
                        Name = "new Added Item",
                    });
                }
                myData.InsertRange(0, items);
            };

            removeBtn.Clicked += (s, e) =>
            {
                myData.Remove(myData[myData.Count - 1]);
            };

            if (Device.Idiom == TargetIdiom.Watch)
            {
                //NavigationPage.SetHasNavigationBar(this, false);
                addBtn.FontSize = removeBtn.FontSize = 6;
                this.Content = new StackLayout
                {
                    Children =
                    {
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                addBtn,
                                removeBtn
                            }
                        },
                        listView
                    }
                };
            }
            else
            {
                this.Content = new StackLayout
                {
                    Children =
                    {
                        listView,
                        addBtn,
                        removeBtn
                    }
                };
            }
        }
    }
}
