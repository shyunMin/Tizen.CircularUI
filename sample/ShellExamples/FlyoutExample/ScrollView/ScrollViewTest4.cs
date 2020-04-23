using System;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ScrollViewTest4 : ContentPage
    {
        public ScrollViewTest4()
        {
            StackLayout hLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                AutomationId = "hLayout"
            };

            StackLayout vLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                AutomationId = "vLayout"
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
                    Color = colors[i % 8],
                    WidthRequest = 100,
                    HeightRequest = 100
                });
            }

            Label label = new Label
            {
                Text = "scroll X:0, Y:0",
                HeightRequest = 30,
                AutomationId = "label"
            };

            Button button = new Button
            {
                Text = "change",
                AutomationId = "button"
            };

            ScrollView scrollView = new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Both,
                Content = vLayout,
                AutomationId = "scrollView"
            };

            scrollView.Scrolled += (sender, args) =>
            {
                label.Text = String.Format("scroll X:{0}, Y:{1}",
                    scrollView.ScrollX,
                    scrollView.ScrollY);
            };

            button.Clicked += (sender, args) =>
            {
                if (scrollView.Content == vLayout)
                {
                    scrollView.Content = hLayout;
                }
                else
                {
                    scrollView.Content = vLayout;
                }
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
                    button,
                    scrollView,
                }
            };

            // Build the page.
            this.Content = wrap;
        }
    }
}