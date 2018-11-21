using System;
using System.Collections.Generic;
using System.Linq;
using NGC.Controls;
using NGC.iOS.Renderers;
using NGC.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace NGC.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control!=null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }

    }
}
