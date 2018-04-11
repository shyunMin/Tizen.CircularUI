﻿/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ElmSharp;
using ElmSharp.Wearable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CircularUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

using XToolbarItem = Xamarin.Forms.ToolbarItem;

[assembly: ExportRenderer(typeof(CirclePage), typeof(CircularUI.Tizen.CirclePageRenderer))]

namespace CircularUI.Tizen
{
    public class CirclePageRenderer : VisualElementRenderer<CirclePage>
    {
        NBox _box;

        ElmSharp.Rectangle _bgColorObject;
        ElmSharp.EvasImage _bgImageObject;
        ElmSharp.Layout _surfaceLayout;
        ElmSharp.Button _actionButton;
        string _bgImage;

        ElmSharp.Wearable.CircleSurface _surface;
        IRotaryFocusable _currentRotaryFocusObject;

        ElmSharp.Wearable.MoreOption _moreOption;
        Dictionary<XToolbarItem, ElmSharp.Wearable.MoreOptionItem> _toolbarItemMap;
        Dictionary<ICircleSurfaceItem, ElmSharp.Wearable.ICircleWidget> _circleSurfaceItems;

        public CirclePageRenderer()
        {
            RegisterPropertyHandler(Xamarin.Forms.Page.BackgroundImageProperty, UpdateBackgroundImage);
            RegisterPropertyHandler(CirclePage.ActionButtonProperty, UpdateActionButton);
            RegisterPropertyHandler(CirclePage.RotaryFocusObjectProperty, UpdateRotaryFocusObject);
        }

        public ElmSharp.Wearable.CircleSurface CircleSurface => _surface;

        protected override void OnElementChanged(ElementChangedEventArgs<CirclePage> e)
        {
            if (_box == null)
            {
                OnRealized();
            }
            if (e.NewElement != null)
            {
                e.NewElement.Appearing += OnPageAppearing;
                e.NewElement.Disappearing += OnPageDisappearing;
                var toolbarItems = e.NewElement.ToolbarItems as ObservableCollection<XToolbarItem>;
                if (toolbarItems != null)
                    toolbarItems.CollectionChanged += OnToolbarItemChanged;
                var circleSurfaceItems = e.NewElement.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
                if (circleSurfaceItems != null)
                {
                    circleSurfaceItems.CollectionChanged += OnCircleSurfaceItemsChanged;

                    foreach (var item in circleSurfaceItems)
                    {
                        AddCircleSurfaceItem(item);
                    }
                }
            }
            if (e.OldElement != null)
            {
                e.OldElement.Appearing -= OnPageAppearing;
                e.OldElement.Disappearing -= OnPageDisappearing;
                var toolbarItems = e.NewElement.ToolbarItems as ObservableCollection<XToolbarItem>;
                if (toolbarItems != null)
                    toolbarItems.CollectionChanged -= OnToolbarItemChanged;
                var circleSurfaceItems = e.NewElement.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
                if (circleSurfaceItems != null)
                    circleSurfaceItems.CollectionChanged -= OnCircleSurfaceItemsChanged;
            }
            base.OnElementChanged(e);
        }
        protected override void UpdateBackgroundColor(bool initialize)
        {
            if (initialize && Element.BackgroundColor.IsDefault) return;

            if (Element.BackgroundColor.A == 0)
            {
                _bgColorObject.Color = ElmSharp.Color.Transparent;
            }
            else
            {
                _bgColorObject.Color = Element.BackgroundColor.ToNative();
            }
            UpdateBackground();
        }
        protected void UpdateBackgroundImage(bool initialize)
        {
            if (initialize && string.IsNullOrWhiteSpace(Element.BackgroundImage))
                return;
            if (string.IsNullOrWhiteSpace(Element.BackgroundImage))
            {
                _bgImageObject.File = null;
                _bgImage = null;
            }
            else
            {
                _bgImageObject.File = ResourcePath.GetPath(Element.BackgroundImage);
                _bgImage = Element.BackgroundImage;
            }
            UpdateBackground();
        }
        protected override void Dispose(bool disposing)
        {
            if (Element != null)
            {
                Element.Appearing -= OnPageAppearing;
                Element.Disappearing -= OnPageDisappearing;
                if (Element.ActionButton != null)
                {
                    Element.ActionButton.PropertyChanged -= OnActionButtonItemChanged;
                }
                var toolbarItems = Element.ToolbarItems as ObservableCollection<XToolbarItem>;
                if (toolbarItems != null)
                    toolbarItems.CollectionChanged -= OnToolbarItemChanged;

                var circleSurfaceItems = Element.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
                if (circleSurfaceItems != null)
                    circleSurfaceItems.CollectionChanged -= OnCircleSurfaceItemsChanged;
            }
            base.Dispose(disposing);
        }
        void OnRealized()
        {
            _box = new NBox(Xamarin.Forms.Platform.Tizen.Forms.NativeParent);
            _box.SetLayoutCallback(OnLayout);

            _bgColorObject = new ElmSharp.Rectangle(_box)
            {
                Color = ElmSharp.Color.Transparent
            };
            _bgImageObject = new EvasImage(_box);
            _surfaceLayout = new ElmSharp.Layout(_box);
            _surface = new ElmSharp.Wearable.CircleSurface(_surfaceLayout);


            _toolbarItemMap = new Dictionary<XToolbarItem, ElmSharp.Wearable.MoreOptionItem>();
            _circleSurfaceItems = new Dictionary<ICircleSurfaceItem, ICircleWidget>();

            _box.PackEnd(_bgColorObject);
            _box.PackEnd(_bgImageObject);
            _box.PackEnd(_surfaceLayout);

            _bgColorObject.Show();
            _bgImageObject.Hide();
            _surfaceLayout.Show();

            if (Element.ToolbarItems.Count > 0)
            {
                SetVisibleMoreOption(true);
                foreach (var item in Element.ToolbarItems)
                {
                    AddToolbarItem(item);
                }
            }

            SetNativeView(_box);
        }
        void OnLayout()
        {
            var rect = _box.Geometry;
            Element.Layout(rect.ToDP());
            _bgColorObject.Geometry = rect;
            _bgImageObject.Geometry = rect;

            _bgImageObject.StackAbove(_bgColorObject);
            EvasObject prev = _bgImageObject;

            IContainable<EvasObject> container = _box;
            foreach (var obj in container.Children)
            {
                obj.StackAbove(prev);
                prev = obj;
            }

            if (_actionButton != null)
            {
                var btnRect = _actionButton.Geometry;
                var btnW = Math.Max(_actionButton.MinimumWidth, btnRect.Width);
                var btnH = Math.Max(_actionButton.MinimumHeight, btnRect.Height);
                var btnX = (rect.Width - btnW) / 2;
                var btnY = rect.Height - btnH;
                _actionButton.Geometry = new Rect(btnX, btnY, btnW, btnH);
                _actionButton.StackAbove(prev);
                prev = _actionButton;
            }

            _surfaceLayout.Geometry = rect;
            _surfaceLayout.StackAbove(prev);
            prev = _surfaceLayout;

            if (_moreOption != null)
            {
                _moreOption.Geometry = Xamarin.Forms.Platform.Tizen.Forms.NativeParent.Geometry;
                _moreOption.StackAbove(prev);
            }
        }

        void UpdateBackground()
        {
            if (string.IsNullOrEmpty(_bgImage))
            {
                _bgImageObject.Hide();
            }
            else
            {
                _bgImageObject.Show();
            }
        }
        void UpdateActionButton(bool initialize)
        {
            if (initialize && Element.ActionButton == null) return;

            if (Element.ActionButton != null)
            {
                SetVisibleActionButton(Element.ActionButton.IsVisible);

                Element.ActionButton.PropertyChanged += OnActionButtonItemChanged;
                _actionButton.Text = Element.ActionButton.Text;
                _actionButton.IsEnabled = Element.ActionButton.IsEnable;
                if (Element.ActionButton.Icon != null)
                {
                    var path = ResourcePath.GetPath(Element.ActionButton.Icon);
                    var buttonImage = new ElmSharp.Image(_actionButton);
                    buttonImage.LoadAsync(path);
                    buttonImage.Show();
                    _actionButton.SetPartContent("elm.swallow.content", buttonImage);
                }
            }
        }
        void OnActionButtonItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == MenuItem.TextProperty.PropertyName)
            {
                _actionButton.Text = Element.ActionButton.Text;
            }
            else if (e.PropertyName == ActionButtonItem.IsEnableProperty.PropertyName)
            {
                _actionButton.IsEnabled = Element.ActionButton.IsEnable;
            }
            else if (e.PropertyName == ActionButtonItem.IsVisibleProperty.PropertyName)
            {
                SetVisibleActionButton(Element.ActionButton.IsVisible);
            }
        }
        void OnActionButtonClicked(object sender, EventArgs e)
        {
            if (Element.ActionButton != null)
            {
                Element.ActionButton.Activate();
            }
        }
        void OnToolbarItemChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetVisibleMoreOption(Element.ToolbarItems.Count > 0);
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (XToolbarItem item in e.NewItems) AddToolbarItem(item);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (XToolbarItem item in e.OldItems) RemoveToolbarITem(item);
            }
        }
        void AddToolbarItem(XToolbarItem item)
        {
            var moreOptionItem = new ActionMoreOptionItem();
            var icon = item.Icon;
            if (!string.IsNullOrEmpty(icon.File))
            {
                var img = new ElmSharp.Image(_moreOption);
                img.LoadAsync(ResourcePath.GetPath(icon.File));
                moreOptionItem.Icon = img;
            }
            var text = item.Text;
            if (!string.IsNullOrEmpty(text))
            {
                moreOptionItem.MainText = text;
            }
            if (item is CircleToolbarItem)
            {
                var subText = ((CircleToolbarItem)item).SubText;
                if (!string.IsNullOrEmpty(subText))
                {
                    moreOptionItem.SubText = subText;
                }
            }
            moreOptionItem.Action = () => item.Activate();
            _moreOption.Items.Add(moreOptionItem);
            _toolbarItemMap[item] = moreOptionItem;
        }
        void RemoveToolbarITem(XToolbarItem item)
        {
            if (_toolbarItemMap.TryGetValue(item, out var moreOptionItem))
            {
                _moreOption?.Items.Remove(moreOptionItem);
                _toolbarItemMap.Remove(item);
            }
        }
        void OnMoreOptionClicked(object sender, ElmSharp.Wearable.MoreOptionItemEventArgs e)
        {
            var item = e.Item as ActionMoreOptionItem;
            if (item != null)
            {
                item.Action?.Invoke();
            }
            _moreOption.IsOpened = false;
        }
        void UpdateRotaryFocusObject(bool initialize)
        {
            if (initialize)
            {
                _currentRotaryFocusObject = Element.RotaryFocusObject;
            }
            else
            {
                DeactivateRotaryWidget();
                _currentRotaryFocusObject = Element.RotaryFocusObject;
                ActivateRotaryWidget();
            }
        }
        void OnPageDisappearing(object sender, EventArgs e)
        {
            DeactivateRotaryWidget();
        }
        void OnPageAppearing(object sender, EventArgs e)
        {
            ActivateRotaryWidget();
        }
        void ToolbarClosed(object sender, EventArgs e)
        {
            ActivateRotaryWidget();
        }
        void ToolbarOpened(object sender, EventArgs e)
        {
            DeactivateRotaryWidget();
        }

        void ActivateRotaryWidget()
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                RotaryEventManager.Rotated += OnRotaryEventChanged;
            }
            else if (_currentRotaryFocusObject is IRotaryFocusable)
            {
                GetRotaryWidget(_currentRotaryFocusObject)?.Activate();
            }
        }
        void DeactivateRotaryWidget()
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                RotaryEventManager.Rotated -= OnRotaryEventChanged;
            }
            else if (_currentRotaryFocusObject is IRotaryFocusable)
            {
                GetRotaryWidget(_currentRotaryFocusObject)?.Deactivate();
            }
        }
        void OnRotaryEventChanged(ElmSharp.Wearable.RotaryEventArgs e)
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                var receiver = _currentRotaryFocusObject as IRotaryEventReceiver;
                receiver.Rotate(new RotaryEventArgs { IsClockwise = e.IsClockwise });
            }
        }

        void OnCircleSurfaceItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ICircleSurfaceItem item in e.NewItems)
                    AddCircleSurfaceItem(item);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ICircleSurfaceItem item in e.OldItems)
                    RemoveCircleSurfaceItem(item);
            }
        }

        void AddCircleSurfaceItem(ICircleSurfaceItem item)
        {
            if (item is CircleProgressBarSurfaceItem)
            {
                var widget = new CircleProgressBarSurfaceItemImplements(item as CircleProgressBarSurfaceItem, _surfaceLayout, _surface);
                _circleSurfaceItems[item] = widget;
            }
            else if (item is CircleSliderSurfaceItem)
            {
                var widget = new CircleSliderSurfaceItemImplements(item as CircleSliderSurfaceItem, _surfaceLayout, _surface);
                _circleSurfaceItems[item] = widget;
            }
        }
        void RemoveCircleSurfaceItem(ICircleSurfaceItem item)
        {
            if (_circleSurfaceItems.TryGetValue(item, out var widget))
            {
                ElmSharp.EvasObject obj = widget as ElmSharp.EvasObject;
                obj?.Unrealize();
                _circleSurfaceItems.Remove(item);
            }
        }

        IRotaryActionWidget GetRotaryWidget(IRotaryFocusable focusable)
        {
            var consumer = focusable as BindableObject;
            IRotaryActionWidget rotaryWidget = null;
            if (consumer != null)
            {
                if (consumer is CircleSliderSurfaceItem)
                {
                    ICircleSurfaceItem item = consumer as ICircleSurfaceItem;
                    rotaryWidget = GetCircleWidget(item) as IRotaryActionWidget;
                }
                else
                {
                    var consumerRenderer = Xamarin.Forms.Platform.Tizen.Platform.GetRenderer(consumer);
                    rotaryWidget = consumerRenderer?.NativeView as IRotaryActionWidget;
                }
            }
            return rotaryWidget;
        }

        ICircleWidget GetCircleWidget(ICircleSurfaceItem item)
        {
            ElmSharp.Wearable.ICircleWidget widget;
            if (_circleSurfaceItems.TryGetValue(item, out widget))
            {
                return widget;
            }
            return null;
        }

        void SetVisibleActionButton(bool visible)
        {
            if (_actionButton == null)
            {
                _actionButton = new ElmSharp.Button(_box)
                {
                    Style = "bottom"
                };
                _actionButton.Clicked += OnActionButtonClicked;
                _box.PackEnd(_actionButton);
            }
            if (visible) _actionButton.Show();
            else _actionButton.Hide();
        }

        void SetVisibleMoreOption(bool visible)
        {
            if (_moreOption == null)
            {
                _moreOption = new ElmSharp.Wearable.MoreOption(_box);
                _moreOption.Clicked += OnMoreOptionClicked;
                _moreOption.Opened += ToolbarOpened;
                _moreOption.Closed += ToolbarClosed;
                _box.PackEnd(_moreOption);
            }
            if (visible) _moreOption.Show();
            else _moreOption.Hide();
        }

        class NBox : ElmSharp.Box, IContainable<EvasObject>
        {
            ReObservableCollection<EvasObject> _children;
            public NBox(EvasObject parent) : base(parent)
            {
                _children = new ReObservableCollection<EvasObject>();
                _children.CollectionChanged += OnChildrenChanged;
            }
            IList<EvasObject> IContainable<EvasObject>.Children => _children;

            void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var child in e.NewItems)
                    {
                        if (child is EvasObject)
                        {
                            PackEnd(child as EvasObject);
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var child in e.OldItems)
                    {
                        if (child is EvasObject)
                        {
                            UnPack(child as EvasObject);
                        }
                    }
                }
            }
        }

        class ActionMoreOptionItem : MoreOptionItem
        {
            public Action Action { get; set; }
        }

        class ReObservableCollection<T> : ObservableCollection<T>
        {
            protected override void ClearItems()
            {
                var oldItems = Items.ToList();
                Items.Clear();
                using (BlockReentrancy())
                {
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems));
                }
                base.ClearItems();
            }
        }
    }
}
