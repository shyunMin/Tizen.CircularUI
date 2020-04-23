using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class RandomColorPage : ContentPage
    {
        Label TitleLabel;
        public RandomColorPage()
        {
            Console.WriteLine("Create RandomColorPage");
            var rand = new Random();
            TitleLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            var color = Color.FromRgb(rand.Next(255), rand.Next(255), rand.Next(255));
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = color,
                Children =
                {
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Text = $"Color : {color.ToHex()}"
                    },
                    TitleLabel,
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                TitleLabel.Text = Shell.Current.CurrentState.Location.ToString();
            });
        }
    }
}
