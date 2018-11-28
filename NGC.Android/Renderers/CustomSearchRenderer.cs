using System;
using System.Collections.Generic;
using System.Linq;
using NGC.Controls;
using NGC.Droid.Renderers;
using NGC.Pages;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using Android.Support.V4.Content;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Widget;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;
using Android.Content.Res;
using AView = Android.Views.View;
using AViewGroup = Android.Views.ViewGroup;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchRenderer))]
namespace NGC.Droid.Renderers
{
    public class CustomSearchRenderer : SearchBarRenderer
    {
        Context _context;

        public CustomSearchRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

          

            if (this.Control == null) 
                return;

  
            var editText = this.Control.GetChildrenOfType<EditText>().FirstOrDefault();
           
            if (editText != null)
            {
                var shape = new ShapeDrawable(new RectShape());
                shape.Paint.Color = Android.Graphics.Color.White;
                shape.Paint.StrokeWidth = 0;
                shape.Paint.SetStyle(Paint.Style.Stroke);
                editText.Background = shape;
            }

          
            var gradient = new GradientDrawable();
            gradient.SetCornerRadius(30.0f);
           
            int[][] states =
            {

                new[] {Android.Resource.Attribute.StateEnabled}, // enabled

                new[] {-Android.Resource.Attribute.StateEnabled} // disabled

            };

            int[] colors =
            {
                Xamarin.Forms.Color.White.ToAndroid(),
                Xamarin.Forms.Color.White.ToAndroid()

            };

            var stateList = new ColorStateList(states: states, colors: colors);
            gradient.SetStroke((int)this.Context.ToPixels(1.0f), stateList);
            gradient.SetColor(stateList);

            this.Control.SetBackground(gradient);
        }

    }

    internal static class ViewGroupExtensions
    {
        internal static IEnumerable<T> GetChildrenOfType<T>(this AViewGroup self) where T : AView
        {
            for (var i = 0; i < self.ChildCount; i++)
            {
                var child = self.GetChildAt(i);
                if (child is T typedChild)
                    yield return typedChild;

                if (!(child is AViewGroup)) continue;
                var myChildren = (child as AViewGroup).GetChildrenOfType<T>();
                foreach (var nextChild in myChildren)
                    yield return nextChild;
            }
        }
    }
}
