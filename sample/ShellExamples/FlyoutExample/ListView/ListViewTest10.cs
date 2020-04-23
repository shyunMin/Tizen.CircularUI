using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest10 : ContentPage
    {
        public ListViewTest10()
        {
            var myDatas = new MyData[]
            {
                new MyData { Height = -1 },
                new MyData { Height = 50 },
                new MyData { Height = 60 },
                new MyData { Height = 70 },
                new MyData { Height = 80 },
                new MyData { Height = 90 },
                new MyData { Height = 100 },
                new MyData { Height = 200 },
                new MyData { Height = 400 },
            };

            var listView = new ListView
            {
                ItemTemplate = new TemplateSelector(),
                HasUnevenRows = true,
                ItemsSource = myDatas,
                AutomationId = "listView"
            };

            // Build the page.
            this.Content = listView;
        }

        struct MyData
        {
            public int Height;
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
                    var height = ((MyData)_item).Height;

                    var cell = new ViewCell
                    {
                        View = new Label
                        {
                            Text = string.Format("Height {0}", height),
                        },
                        Height = height,
                    };
                    cell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(cell.AutomationId))
                            cell.AutomationId = string.Format("Height {0}", height);
                    };
                    return cell;
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