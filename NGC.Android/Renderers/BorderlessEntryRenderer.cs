using System;
using System.Collections.Generic;
using System.Linq;
using NGC.Controls;
using NGC.Droid.Renderers;
using NGC.Pages;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace NGC.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {

        public BorderlessEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control!=null)
            {
                Control.Background = null;
            }
        }

    }
}
