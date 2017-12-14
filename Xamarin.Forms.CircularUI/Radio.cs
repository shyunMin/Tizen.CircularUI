﻿using System;
using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    public class Radio : View
    {
        /// <summary>
        /// BindableProperty. Identifies the IsSelected bindable property.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create("IsSelected", typeof(bool), typeof(Radio), false,
             propertyChanged: IsSelectedPropertyChanged);

        /// <summary>
        /// BindableProperty. Identifies the GroupName bindable property.
        /// </summary>
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create("GroupName", typeof(string), typeof(Radio), default(string));

        /// <summary>
        /// Gets or sets the name that specifies which RadioButton controls are mutually exclusive.
        /// It can be set to null.
        /// </summary>
        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this RadioButton is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Occurs when the RadioButton selection was changed.
        /// </summary>
        public event EventHandler<SelectedEventArgs> Selected;


        static void IsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radioButton = (Radio)bindable;
            radioButton.Selected?.Invoke(radioButton, new SelectedEventArgs((bool)newValue));
        }
    }
}
