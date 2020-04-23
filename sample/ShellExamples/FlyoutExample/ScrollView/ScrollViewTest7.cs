using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ScrollViewTest7 : ContentPage
    {
        public ScrollViewTest7()
        {
            StackLayout vLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };

            for (int i = 1; i <= 60; ++i)
            {
                vLayout.Children.Add(new Label
                {
                    Text = String.Format("I am label #{0}", i),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
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
                Text = "Random vertical scroll ",
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

            button.Clicked += async (sender, args) =>
            {
                await scrollView.ScrollToAsync(0, new Random(DateTime.UtcNow.Millisecond).Next((int)vLayout.Height), true);
                Debug.WriteLine($"@@@@@ ScrollToAscyn test complate test");
                await DisplayAlert("test", "scroll end", "cancel");
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