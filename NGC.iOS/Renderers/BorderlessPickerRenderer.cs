using System;
using System.Collections.Generic;
using System.Linq;
using NGC.Controls;
using NGC.iOS.Renderers;
using NGC.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace NGC.iOS.Renderers
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
        public BorderlessPickerRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if(Control!=null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

            }
        }
    }
}
