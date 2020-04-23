using System;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ScrollViewTest1 : ContentPage
    {
        public ScrollViewTest1()
        {
            StackLayout vLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsVisible = true,
            };

            ScrollView scrollView = new ScrollView
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Vertical,
                Content = vLayout,
                AutomationId = "scrollView"
            };

            var button = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Test ScrollTo",
                AutomationId = "button"
            };

            button.Clicked += (s, e) =>
            {
                //await scrollView.ScrollToAsync(0, 100, true);

                if (scrollView.VerticalScrollBarVisibility == ScrollBarVisibility.Always)
                {
                    scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
                }
                else
                {
                    scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
                }

                if (scrollView.HorizontalScrollBarVisibility == ScrollBarVisibility.Always)
                {
                    scrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
                }
                else
                {
                    scrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Always;
                }

            };

            vLayout.Children.Add(button);

            for (int i = 1; i <= 60; ++i)
            {
                vLayout.Children.Add(new Label
                {
                    Text = String.Format("I am label #{0}", i),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                });
            }

            // Build the page.
            this.Padding = new Thickness(10);
            this.Content = scrollView;
        }
    }
}
