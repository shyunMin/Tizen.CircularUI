using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace FlyoutExample
{
    public class ScrollViewTest6 : ContentPage
    {
        public static readonly int initWidth = 400;
        public static readonly int initHeight = 300;
        public static Stack<Label> labelList = new Stack<Label>();

        public static int fontSize = 36;

        public ScrollViewTest6()
        {
            Label horizonlabel = new Label
            {
                Text = "This is long horizontal text. ",
                LineBreakMode = LineBreakMode.NoWrap,
                TextColor = Color.Olive,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            StackLayout scrollViewLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label {
                        Text = "This is what a ScrollView widget looks like.",
                        LineBreakMode = LineBreakMode.NoWrap,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = Color.Blue,
                    },
                    new Label {
                        Text = "Please use the widgets to test ScrollView's behavior.",
                        LineBreakMode = LineBreakMode.NoWrap,
                        TextColor = Color.Gray,
                    },
                    horizonlabel,
                },
                AutomationId = "scrollViewLayout"
            };

            ScrollView scrollView = new ScrollView
            {
                Content = scrollViewLayout,
                WidthRequest = initWidth,
                HeightRequest = initHeight,
                Orientation = ScrollOrientation.Both,
                AutomationId = "scrollView"
            };
            scrollView.HorizontalOptions = LayoutOptions.Center;
            scrollView.VerticalOptions = LayoutOptions.Center;

            scrollView.IsVisible = true;

            Slider widthSlider = new Slider(50, 360, initWidth);
            Label widthLabel = new Label
            {
                Text = String.Format("Width: {0:0.00}", widthSlider.Value),
                TextColor = Color.Gray,
            };
            widthSlider.ValueChanged += (object sender, ValueChangedEventArgs e) =>
            {
                widthLabel.Text = String.Format("Width: {0:0.00}", widthSlider.Value);
                scrollView.WidthRequest = widthSlider.Value;
            };

            Slider heightSlider = new Slider(50, 360, initHeight);
            Label heightLabel = new Label
            {
                Text = String.Format("Height: {0:0.00}", heightSlider.Value),
                TextColor = Color.Gray,
            };
            heightSlider.ValueChanged += (object sender, ValueChangedEventArgs e) =>
            {
                heightLabel.Text = String.Format("Height: {0:0.00}", heightSlider.Value);
                scrollView.HeightRequest = heightSlider.Value;
            };

            Button horizontalAddButton = new Button
            {
                Text = "Add more horizontal text",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                AutomationId = "horizontalAddButton"
            };
            horizontalAddButton.Clicked += (object sender, EventArgs e) =>
            {
                horizonlabel.Text += "More horizontal text. ";
            };

            Button verticallAddButton = new Button
            {
                Text = "Add more vertical text",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                AutomationId = "verticallAddButton"
            };
            verticallAddButton.Clicked += (object sender, EventArgs e) =>
            {
                fontSize = Math.Min((int)(fontSize * 1.2), 70);
                Label newlabel = new Label
                {
                    Text = "Next line of text.",
                    TextColor = Color.FromRgb(101, 151, 101),
                    LineBreakMode = LineBreakMode.NoWrap,
                };
                newlabel.FontSize = fontSize;
                scrollViewLayout.Children.Add(newlabel);
                labelList.Push(newlabel);
            };

            Button verticalRemoveButton = new Button
            {
                Text = "Remove more vertical text",
                AutomationId = "verticalRemoveButton"
            };
            verticalRemoveButton.Clicked += (object sender, EventArgs e) =>
            {
                scrollViewLayout.Children.Remove(labelList.Pop());
            };

            Label XYLabel = new Label
            {
                Text = "ScrollX: 0, ScrollY: 0",
                TextColor = Color.Black,
            };

            Button scrollButton = new Button
            {
                Text = "Async scroll to (0, 0) without animation",
                AutomationId = "scrollButton"
            };
            scrollButton.Clicked += (object sender, EventArgs e) =>
            {
                scrollView.ScrollToAsync(0, 0, false);
                XYLabel.Text = String.Format("ScrollX: {0}, ScrollY: {1}",
                    scrollView.ScrollX, scrollView.ScrollY
                );
            };

            Button scrollButtonAnimation = new Button
            {
                Text = "Async scroll to (0, 0) with animation",
                AutomationId = "scrollButtonAnimation"
            };
            scrollButtonAnimation.Clicked += (object sender, EventArgs e) =>
            {
                scrollView.ScrollToAsync(0, 0, true);
                XYLabel.Text = String.Format("ScrollX: {0}, ScrollY: {1}",
                    scrollView.ScrollX, scrollView.ScrollY
                );
            };

            Label orientationLabel = new Label
            {
                Text = "Orientation: Both",
                TextColor = Color.Black,
                AutomationId = "orientationLabel"
            };
            Button orientationButton = new Button
            {
                Text = "Set Orientation to Vertical",
                AutomationId = "orientationButton"
            };
            orientationButton.Clicked += (object sender, EventArgs e) =>
            {
                switch (scrollView.Orientation)
                {
                    case ScrollOrientation.Both:
                        orientationLabel.Text = "Orientation: Vertical";
                        scrollView.Orientation = ScrollOrientation.Vertical;
                        (sender as Button).Text = "Set Orientation to Horizontal";
                        break;

                    case ScrollOrientation.Vertical:
                        orientationLabel.Text = "Orientation: Horizontal";
                        scrollView.Orientation = ScrollOrientation.Horizontal;
                        (sender as Button).Text = "Set Orientation to Both";
                        break;

                    default:
                        orientationLabel.Text = "Orientation: Both";
                        scrollView.Orientation = ScrollOrientation.Both;
                        (sender as Button).Text = "Set Orientation to Vertical";
                        break;
                }
            };

            scrollView.Scrolled += (object sender, ScrolledEventArgs e) =>
            {
                XYLabel.Text = String.Format("ScrollX: {0}, ScrollY: {1}",
                    scrollView.ScrollX, scrollView.ScrollY
                );
            };

            var controlLayout = new StackLayout
            {
                Children = {
                    widthLabel,
                    widthSlider,
                    heightLabel,
                    heightSlider,
                    horizontalAddButton,
                    verticallAddButton,
                    verticalRemoveButton,
                    XYLabel,
                    scrollButton,
                    scrollButtonAnimation,
                    orientationLabel,
                    orientationButton,
                },
            };

            var contentView = new StackLayout
            {
                Children = {
                    scrollView,
                    controlLayout,
                },
            };

            Content = contentView;
        }
    }
}