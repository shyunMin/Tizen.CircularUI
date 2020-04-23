using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest6 : ContentPage
    {
        public ListViewTest6()
        {
            var myItems = new MyItem[]
            {
                new MyItem { Type = typeof(TextCell), Item1 = "This is primary text", Item2 = "This is secondary text" },
                new MyItem { Type = typeof(SwitchCell), Item1 = "Check cell", Item3 = "false" },
                new MyItem { Type = typeof(SwitchCell), Item1 = "Check cell", Item3 = "true" },
                new MyItem { Type = typeof(ImageCell), Item1 = "Image cell", Item2 = "a.jpg", Item3 = "Image Cell Detail1" },
                new MyItem { Type = typeof(ImageCell), Item1 = "Image cell", Item2 = "b.jpg", Item3 = "Image Cell Detail2" },
                new MyItem { Type = typeof(ViewCell), Item1 = "Colored Text (view cell)", Item2 = "#FF0000" },
                new MyItem { Type = typeof(EntryCell), Item1 = "Entry Label", Item2 = "entry text", Item3 = "placeholder" },
                new MyItem { Type = typeof(EntryCell), Item1 = "Entry Label", Item2 = "", Item3 = "placeholder" },
            };
            // Build the page.
            this.Content = new ListView
            {
                ItemTemplate = new MyDataTemplateSelector(),
                ItemsSource = myItems,
                AutomationId = "listView"
            };
        }

        public class MyItem
        {
            public Type Type { get; set; }
            public string Item1 { get; set; }
            public string Item2 { get; set; }
            public object Item3 { get; set; }
        }

        class MyDataTemplateSelector : DataTemplateSelector
        {
            private readonly DataTemplate _textTemplate;
            private readonly DataTemplate _imageTemplate;
            private readonly DataTemplate _viewTemplate;
            private readonly DataTemplate _checkTemplate;
            private readonly DataTemplate _entryTemplate;

            public MyDataTemplateSelector()
            {
                _textTemplate = new DataTemplate(typeof(TextCell));
                _textTemplate.SetBinding(TextCell.TextProperty, new Binding("Item1"));
                _textTemplate.SetBinding(TextCell.DetailProperty, new Binding("Item2"));

                _checkTemplate = new DataTemplate(typeof(SwitchCell));
                _checkTemplate.SetBinding(SwitchCell.TextProperty, new Binding("Item1"));
                _checkTemplate.SetBinding(SwitchCell.OnProperty, new Binding("Item3"));

                _entryTemplate = new DataTemplate(typeof(EntryCell));
                _entryTemplate.SetBinding(EntryCell.LabelProperty, new Binding("Item1"));
                _entryTemplate.SetBinding(EntryCell.TextProperty, new Binding("Item2"));
                _entryTemplate.SetBinding(EntryCell.PlaceholderProperty, new Binding("Item3"));

                _imageTemplate = new DataTemplate(typeof(ImageCell));
                _imageTemplate.SetBinding(ImageCell.TextProperty, new Binding("Item1"));
                _imageTemplate.SetBinding(ImageCell.ImageSourceProperty, new Binding("Item2"));
                _imageTemplate.SetBinding(ImageCell.DetailProperty, new Binding("Item3"));

                _viewTemplate = new DataTemplate(() =>
                {
                    Label label = new Label
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
                    };
                    label.SetBinding(Label.TextProperty, new Binding("Item1"));
                    label.SetBinding(Label.TextColorProperty, new Binding("Item2"));
                    //var myItem = data as MyItem;
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children =
                            {
                                label
                            }
                        }
                    };
                });
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                var myItem = item as MyItem;
                if (myItem == null)
                {
                    return null;
                }

                if (myItem.Type == typeof(TextCell))
                {
                    return _textTemplate;
                }
                else if (myItem.Type == typeof(SwitchCell))
                {
                    return _checkTemplate;
                }
                else if (myItem.Type == typeof(EntryCell))
                {
                    return _entryTemplate;
                }
                else if (myItem.Type == typeof(ImageCell))
                {
                    return _imageTemplate;
                }
                else if (myItem.Type == typeof(ViewCell))
                {
                    return _viewTemplate;
                }
                return null;
            }
        }
    }
}
