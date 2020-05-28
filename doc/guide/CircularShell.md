---
uid: Tizen.Wearable.CircularUI.doc.CircularShell
summary: CircularShell guide
---
# CircularShell

`CircularShell` is a subclass of `Xamarin.Forms.Shell` that provides some extended properties and feature for Tizen wearable.

![](data/CircularShell.png)

## How different `CircularShell` and `Xamarin.Forms.Shell` on Tizen wearable.

`Shell` is a major feature from Xamarin.Forms 4.0.0. that includes a visual hierarchy of the application and provides common navigation user experience.
Basically it designed for mobile devices, but we have re-designed `Shell` for Tizen wearable and published `CircularShell` from Tizen.CircularUI 1.5.0-pre3
You can check how `Shell` shows on Tizen wearable, and what features are available on Tizen wearable here.

Since `Xamarin.Forms 4.6.800` or later, the renderers for `CircularShell` have been added to `Xamarin.Form.Platform.Tizen`, so there is no difference between `Xamarin.Forms.Shell` and `CircualrShell` in appearance.

However, `CircularShell` provides some extended properties and behavior that fits for Tizen wearable deivce.

Extended properties:

- FlyoutIconBackgroundColor: This property gets or sets the background color of `Flyout`.
- FlyoutForegroundColor: This property gets or sets the text color of `Flyout`.



For more information, see the following links:

- [Xamarin.Forms.Shell API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.Shell/)
- [CircularShell API reference](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.CircularShell.html)
- [Using Xamarin.Form Shell on Galaxy Watch](https://developer.samsung.com/tizen/blog/en-us/2020/03/09/using-xamarinform-shell-on-galaxy-watch)


## Create CircleStepper

You can easily add Check control with C# or XAML file. 

_The code example of this guide uses WearableUIGallery's TCContentButton code. The code is available in test\WearableUIGallery\WearableUIGallery\TC\TCContentButton.xaml_

**C# file**

```cs
public partial class ContentButtonTestPage : ContentPage
{
    public ContentButtonTestPage()
    {
        InitializeComponent();

        ClickCommand = new Command(execute: () =>
        {
            label.Text = "clicked";
        });
    }

    public ICommand ClickCommand { get; private set; }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        Console.WriteLine($"ContentButton clicked event is invoked!!");
    }

    private void OnButtonPressed(object sender, EventArgs e)
    {
        Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Gray);
    }

    private void OnButtonReleased(object sender, EventArgs e)
    {
        Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Transparent);
    }
}
```

**XAML file**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
             xmlns:tizen="clr-namespace:Xamarin.Forms.PlatformConfiguration.TizenSpecific;assembly=Xamarin.Forms.Core"
             x:Class="WearableUIGallery.TC.ContentButtonTestPage">
    <ContentPage.Content>
        <StackLayout VerticalOptions="CenterAndExpand"
                     HorizontalOptions="CenterAndExpand">
            <Label x:Name="label"
                   HorizontalOptions="CenterAndExpand"
                   HorizontalTextAlignment="Center"
                   Text="Test"/>
            <w:ContentButton x:Name="button"
                             Clicked="OnButtonClicked"
                             Pressed="OnButtonPressed"
                             Released="OnButtonReleased"
                             Command="{Binding ClickCommand}">
                <w:ContentButton.Content>
                    <AbsoluteLayout VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand">
                        <Image x:Name="buttonBg" Source="button_bg.png" Opacity="0.25" Aspect="AspectFill" tizen:Image.BlendColor="Transparent" AbsoluteLayout.LayoutBounds=".5,.5,89,66" AbsoluteLayout.LayoutFlags="PositionProportional" />
                        <Image x:Name="buttonBorder" Source="button_border.png" Aspect="AspectFill" tizen:Image.BlendColor="DarkGreen" AbsoluteLayout.LayoutBounds=".5,.5,89,66" AbsoluteLayout.LayoutFlags="PositionProportional" />
                        <Image x:Name="buttonIcon" Source="home.png" tizen:Image.BlendColor="DarkGreen" AbsoluteLayout.LayoutBounds=".5,.5,36,36" AbsoluteLayout.LayoutFlags="PositionProportional" />
                    </AbsoluteLayout>
                </w:ContentButton.Content>
            </w:ContentButton>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
