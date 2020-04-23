using ElmSharp;
using FlyoutExample;
using System;
using System.ComponentModel;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(CustomStepper), typeof(CustomStepperRenderer))]
namespace FlyoutExample
{
    public interface ICircleStepperController
    {
        void SendActivated();
        void SendDeactivated();
    }

    public class CustomStepper : CircleStepper, ICircleStepperController
    {
        public event EventHandler Activated;
        public event EventHandler Deactivated;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendActivated()
        {
            Activated?.Invoke(this, EventArgs.Empty);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendDeactivated()
        {
            Deactivated?.Invoke(this, EventArgs.Empty);
        }
    }

    public class CustomStepperRenderer : CircleStepperRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CircleStepper> e)
        {
            base.OnElementChanged(e);

            var evt = new SmartEvent(Control, "genlist,show");
            evt.On += OnListShow;

            var evt2 = new SmartEvent(Control, "genlist,hide");
            evt2.On += OnListHide;
        }

        void OnListShow(object sender, EventArgs args)
        {
            (Element as ICircleStepperController)?.SendActivated();
        }

        void OnListHide(object sender, EventArgs args)
        {
            (Element as ICircleStepperController)?.SendDeactivated();
        }
    }
}
