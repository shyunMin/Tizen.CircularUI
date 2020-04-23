using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest11 : ContentPage
    {
        public ListViewTest11()
        {
            var myDatas = new List<MyData>();

            for (int i = 1; i <= 9; ++i)
            {
                myDatas.Add(new MyData
                {
                    idx = i,
                    str = string.Format("list item {0}", i)
                });
            }

            var listView = new ListView()
            {
                ItemTemplate = new TemplateSelector(),
                ItemsSource = myDatas,
                AutomationId = "listView"
            };

            Switch check = new Switch()
            {
                AutomationId = "check"
            };
            Label checkLabel = new Label
            {
                Text = "Has uneven rows"
            };

            Slider slider = new Slider
            {
                Minimum = 0,
                Maximum = 300,
                Value = 50,
                AutomationId = "slider"
            };

            listView.SetBinding(ListView.HasUnevenRowsProperty, new Binding("IsToggled", source: check));
            listView.SetBinding(ListView.RowHeightProperty, new Binding("Value", source: slider));

            StackLayout layout = new StackLayout
            {
                Spacing = 10,
                Children =
                {
                    check,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            check,
                            checkLabel
                        }
                    },
                    slider,
                    listView
                }
            };

            // Build the page.
            if (Device.Idiom == TargetIdiom.Watch)
            {
                ScrollView scrollView = new ScrollView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Orientation = ScrollOrientation.Vertical,
                    Content = layout,
                };

                this.Content = scrollView;
            }
            else
            {
                this.Content = layout;
            }
        }

        class MyData
        {
            public int idx { get; set; }
            public string str { get; set; }
        }

        public class TemplateSelector : DataTemplateSelector
        {
            public DataTemplate ValidTemplate { get; set; }
            public DataTemplate InvalidTemplate { get; set; }

            private object _item;

            public TemplateSelector()
            {
                ValidTemplate = new DataTemplate(() =>
                {
                    var myData = (MyData)(_item);
                    var entryCell = new EntryCell();
                    switch (myData.idx % 3)
                    {
                        case 0: entryCell.Text = myData.str; entryCell.HorizontalTextAlignment = TextAlignment.End; break;
                        case 1: entryCell.Text = myData.str; entryCell.HorizontalTextAlignment = TextAlignment.Start; break;
                        case 2: entryCell.Text = myData.str; entryCell.HorizontalTextAlignment = TextAlignment.Center; break;
                    }
                    entryCell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(entryCell.AutomationId))
                            entryCell.AutomationId = entryCell.Text;
                    };
                    return entryCell;
                });

                InvalidTemplate = new DataTemplate(typeof(TextCell));
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                _item = item;
                return ValidTemplate;
            }
        }
    }
}
