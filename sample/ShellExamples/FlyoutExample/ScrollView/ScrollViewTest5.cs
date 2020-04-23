using System;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ScrollViewTest5 : ContentPage
    {
        public ScrollViewTest5()
        {
            // Build the page.
            this.Padding = new Thickness(10);
            this.Content = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Content = CreateContent(),
                AutomationId = "scrollView"
            };
        }

        StackLayout CreateContent()
        {
            var layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            for (int size = 7; size < 16; ++size)
            {
                layout.Children.Add(new Label
                {
                    Text = "a b c d e f g h i j k",
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) * 10,
                    LineBreakMode = LineBreakMode.CharacterWrap,
                });
            }
            return layout;
        }
    }
}
