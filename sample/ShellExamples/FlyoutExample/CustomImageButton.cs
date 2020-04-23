//using Xamarin.Forms;
//using EBox = ElmSharp.Box;

//namespace Xamarin.Forms.Platform.Tizen
//{
//    //public class CustomImageButton : View
//    //{

//    //}

//	public class CustomImageButton : View
//    {
//        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(CustomImageButton), default(ImageSource));
//        public static readonly BindableProperty PressedImageSourceProperty = BindableProperty.Create("PressedImageSource", typeof(ImageSource), typeof(CustomImageButton), default(ImageSource));

//        public static readonly BindableProperty ImageBlendingColorProperty = BindableProperty.Create("ImageBlendingColor", typeof(Color), typeof(CustomImageButton), default(Color));

//        public static readonly BindableProperty ImageBorderLeftProperty = BindableProperty.Create("ImageBorderLeft", typeof(int), typeof(CustomImageButton), default(int));
//        public static readonly BindableProperty ImageBorderRightProperty = BindableProperty.Create("ImageBorderRight", typeof(int), typeof(CustomImageButton), default(int));
//        public static readonly BindableProperty ImageBorderTopProperty = BindableProperty.Create("ImageBorderTop", typeof(int), typeof(CustomImageButton), default(int));
//        public static readonly BindableProperty ImageBorderBottomProperty = BindableProperty.Create("ImageBorderBottom", typeof(int), typeof(CustomImageButton), default(int));


//        public static readonly BindableProperty IsPressedProperty = BindableProperty.Create("IsPressed", typeof(bool), typeof(CustomImageButton), default(bool));
//        public static readonly BindableProperty IsOpaqueProperty = ImageElement.IsOpaqueProperty;
//        public static readonly BindableProperty AspectProperty = ImageElement.AspectProperty;

//        public static readonly BindableProperty CommandProperty = ButtonElement.CommandProperty;
//        public static readonly BindableProperty CommandParameterProperty = ButtonElement.CommandParameterProperty;

//        public event EventHandler Clicked;
//        public event EventHandler Pressed;
//        public event EventHandler Released;

//        public CustomImageButton()
//		{
//		}

//        public int ImageBorderLeft
//        {
//            get { return (int)GetValue(ImageBorderLeftProperty); }
//            set { SetValue(ImageBorderLeftProperty, value); }
//        }

//        public int ImageBorderRight
//        {
//            get { return (int)GetValue(ImageBorderRightProperty); }
//            set { SetValue(ImageBorderRightProperty, value); }
//        }

//        public int ImageBorderTop
//        {
//            get { return (int)GetValue(ImageBorderTopProperty); }
//            set { SetValue(ImageBorderTopProperty, value); }
//        }

//        public int ImageBorderBottom
//        {
//            get { return (int)GetValue(ImageBorderBottomProperty); }
//            set { SetValue(ImageBorderBottomProperty, value); }
//        }

//        public Color ImageBlendingColor
//        {
//            get { return (Color)GetValue(ImageBlendingColorProperty); }
//            set { SetValue(ImageBlendingColorProperty, value); }
//        }
//    }

//	public class CustomButtonRenderer : ImageRenderer
//    {
//        EBox _box;
//        Native.Image _image;

//        public CustomButtonRenderer()
//        {
//            RegisterPropertyHandler(CustomImageButton.ImageBlendingColorProperty, UpdateBlendColor);
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
//        {
//            if(Control == null)
//            {
//                _box = new EBox(Forms.NativeParent);
//                SetNativeControl(_box);
//            }
//        }

//        void UpdateBlendColor()
//        {

//        }

//    }
//}
