using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class ListViewTest3 : ContentPage
    {
        public ListViewTest3()
        {
            var colors = new List<Color>();

            for (int r = 0; r < 255; r += 32)
            {
                for (int g = 0; g < 255; g += 32)
                {
                    for (int b = 0; b < 255; b += 32)
                    {
                        colors.Add(new Color((double)r / 255, (double)g / 255, (double)b / 255));
                    }
                }
            }

            ListView listView = new ListView
            {
                ItemTemplate = new TemplateSelector(),
                ItemsSource = colors,
                AutomationId = "listView"
            };

            // Build the page.
            this.Content = listView;
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
                   Color color = (Color)_item;
                   var cell = new TextCell
                   {
                       Text = color.ToString(),
                       TextColor = color
                   };
                   cell.BindingContextChanged += (s, e) =>
                   {
                       if (String.IsNullOrEmpty(cell.AutomationId))
                           cell.AutomationId = cell.Text;
                   };

                   return cell;
               });

                InvalidTemplate = new DataTemplate(typeof(TextCell));
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                _item = item;
                return (item is Color) ? ValidTemplate : InvalidTemplate;
            }
        }
    }
}
