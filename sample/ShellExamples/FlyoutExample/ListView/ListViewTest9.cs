using System.Collections.Generic;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest9 : ContentPage
    {
        public ListViewTest9()
        {
            var myDatas = new List<MyData>();

            for (int i = 0; i < 30; ++i)
            {
                myDatas.Add(new MyData
                {
                    Idx = i,
                    Str = string.Format("list item #{0}", i)
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

            // Build the page.
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
            public int Idx { get; set; }
            public string Str { get; set; }
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
                    var viewCell = new ViewCell();
                    switch (myData.Idx % 3)
                    {
                        case 0: viewCell.View = CreateType1(myData); break;
                        case 1: viewCell.View = CreateType2(myData); break;
                        case 2: viewCell.View = CreateType3(myData); break;
                    }
                    return viewCell;
                });

                InvalidTemplate = new DataTemplate(typeof(TextCell));
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                _item = item;
                return ValidTemplate;
            }

            Color[] colors =
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Black,
                Color.Aqua,
                Color.Lime,
                Color.Maroon,
            };

            StackLayout CreateType1(MyData data)
            {
                return new StackLayout
                {
                    Padding = new Thickness(20),
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new BoxView
                        {
                            Color = colors[data.Idx % colors.Length]
                        },
                        new Label
                        {
                            Text = data.Str,
                        }
                    }
                };
            }

            StackLayout CreateType2(MyData data)
            {
                return new StackLayout
                {
                    Padding = new Thickness(20),
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new BoxView
                        {
                            Color = colors[data.Idx % colors.Length]
                        },
                        new Label
                        {
                            Text = data.Str,
                        }
                    }
                };
            }

            RelativeLayout CreateType3(MyData data)
            {
                RelativeLayout layout = new RelativeLayout
                {
                    Padding = new Thickness(20),
                    IsVisible = true,
                };

                BoxView box = new BoxView
                {
                    Color = colors[data.Idx % colors.Length],
                };

                Label label = new Label
                {
                    Text = data.Str
                };

                layout.Children.Add(box, Constraint.RelativeToParent((parent) =>
                {
                    return parent.X;
                }), Constraint.RelativeToParent((parent) =>
                {
                    return parent.Y;
                }));

                layout.Children.Add(label, Constraint.RelativeToParent((parent) =>
                {
                    return parent.X * .5;
                }), Constraint.RelativeToParent((parent) =>
                {
                    return parent.Y * .5;
                }));

                return layout;
            }
        }
    }
}
