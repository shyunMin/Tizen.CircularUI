using System;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ScrollViewTest3 : ContentPage
    {
        public ScrollViewTest3()
        {
            StackLayout hLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsVisible = true,
            };

            var button = new Button
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Test ScrollTo",
                AutomationId = "button"
            };

            StackLayout vLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsVisible = true,
                Children =
            {
                button,
                hLayout,
            },
            };

            Color[] colors =
            {
            new Color(0, 0, 0),
            new Color(255, 0, 0),
            new Color(0, 255, 0),
            new Color(0, 0, 255),
            new Color(255, 255, 0),
            new Color(255, 0, 255),
            new Color(0, 255, 255),
            new Color(255, 255, 255),
        };

            for (int i = 1; i <= 60; ++i)
            {
                vLayout.Children.Add(new Label
                {
                    Text = String.Format("I am label #{0}", i),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                });

                hLayout.Children.Add(new BoxView
                {
                    Opacity = 0.5,
                    Color = colors[i % 8],
                    WidthRequest = 100,
                    HeightRequest = 100
                });
            }

            Label label = new Label
            {
                Text = "scroll X:0, Y:0",
                HeightRequest = 30,
            };

            ScrollView scrollView = new ScrollView
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Both,
                Content = vLayout,
                AutomationId = "scrollView"
            };

            button.Clicked += async (s, e) =>
            {
                await scrollView.ScrollToAsync(200, 200, true);
            };

            scrollView.Scrolled += (sender, args) =>
                {
                    label.Text = String.Format("scroll X:{0}, Y:{1}",
                        scrollView.ScrollX,
                        scrollView.ScrollY);
                };

            Layout wrap = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsVisible = true,
                Children =
                {
                    label,
                    scrollView,
                }
            };

            // Build the page.
            this.Content = wrap;
        }
    }
}