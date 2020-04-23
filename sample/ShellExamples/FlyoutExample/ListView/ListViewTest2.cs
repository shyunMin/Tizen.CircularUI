using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest2 : ContentPage
    {
        public ListViewTest2()
        {
            var myDatas = new List<MyData>
            {
                new MyData { No = 1, Name = "Kim" },
                new MyData { No = 2, Name = "Lee" },
                new MyData { No = 3, Name = "Park" },
                new MyData { No = 4, Name = "Choi" },
                new MyData { No = 5, Name = "Cho "},
                new MyData { No = 6, Name = "Hur" },
            };

            ListView listView = new ListView
            {
                ItemTemplate = new MyDataTemplateSelector(),
                ItemsSource = myDatas,
                AutomationId = "listView"
            };

            // Build the page.
            this.Content = listView;
        }

        class MyData
        {
            public int No { get; set; }
            public string Name { get; set; }
        }

        class MyDataTemplateSelector : DataTemplateSelector
        {
            private readonly DataTemplate _textTemplate;
            private readonly DataTemplate _viewTemplate;

            public MyDataTemplateSelector()
            {
                _textTemplate = new DataTemplate(typeof(TextCell));
                _textTemplate.SetBinding(TextCell.TextProperty, new Binding("Name"));
                _textTemplate.SetBinding(TextCell.DetailProperty, new Binding("No", stringFormat: "{0}"));

                _viewTemplate = new DataTemplate(() =>
                {
                    var label1 = new Label();
                    var label2 = new Label();
                    var check = new Switch { IsToggled = false };
                    var layout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            label1,
                            label2,
                            check,
                        }
                    };

                    label1.SetBinding(Label.TextProperty, "Name");
                    label2.SetBinding(Label.TextProperty, "No");

                    var cell = new ViewCell { View = layout };
                    cell.BindingContextChanged += (s, e) =>
                    {
                        if (String.IsNullOrEmpty(cell.AutomationId) && (!String.IsNullOrEmpty(label1.Text)))
                            cell.AutomationId = label1.Text;
                    };

                    return cell;
                });
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                var myData = item as MyData;
                if (myData == null)
                {
                    return null;
                }
                return myData.No % 2 == 0 ? _textTemplate : _viewTemplate;
            }
        }
    }
}
