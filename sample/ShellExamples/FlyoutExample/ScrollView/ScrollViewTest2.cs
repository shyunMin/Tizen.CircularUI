using System;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ScrollViewTest2 : ContentPage
    {
        public ScrollViewTest2()
        {
            StackLayout hLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsVisible = true,
            };

            ScrollView scrollView = new ScrollView
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Horizontal,
                Content = hLayout,
                AutomationId = "scrollView"
            };

            var button = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Test ScrollTo",
                AutomationId = "button"
            };

            button.Clicked += async (s, e) =>
            {
                await scrollView.ScrollToAsync(100, 0, true);
            };

            hLayout.Children.Add(button);

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
                hLayout.Children.Add(new BoxView
                {
                    Opacity = 0.5,
                    Color = colors[i % 8],
                    WidthRequest = 100,
                    HeightRequest = 100,
                });
            }

            // Build the page.
            this.Content = scrollView;
        }
    }
}