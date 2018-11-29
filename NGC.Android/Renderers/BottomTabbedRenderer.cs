using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Android.Support.Design.Internal;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;
using NGC.Droid.Renderers;
using static System.Console;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(BottomTabbedRenderer))]
namespace NGC.Droid.Renderers
{
    public class BottomTabbedRenderer : TabbedPageRenderer
    {
       
         private bool _isShiftModeSet;

        public BottomTabbedRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            try
            {
                if (!_isShiftModeSet)
                {
                    var children = GetAllChildViews(ViewGroup);

                    if (children.SingleOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
                    {
                       
                        bottomNav.Elevation = 60;
                      
                         bottomNav.SetShiftMode(false, false);
                        _isShiftModeSet = true;
                    }
                }
            }
            catch (Exception e)
            {
                WriteLine($"Error setting ShiftMode: {e}");
            }
        }



        private List<View> GetAllChildViews(View view)
        {
            if (!(view is ViewGroup group))
            {
                return new List<View> { view };
            }

            var result = new List<View>();

            for (int i = 0; i < group.ChildCount; i++)
            {
                var child = group.GetChildAt(i);

                var childList = new List<View> { child };
                childList.AddRange(GetAllChildViews(child));

                result.AddRange(childList);
            }

            return result.Distinct().ToList();
        }
    }


    public static class AndroidHelpers
    {
        public static void SetShiftMode(this BottomNavigationView bottomNavigationView, bool enableShiftMode, bool enableItemShiftMode)
        {
            try
            {
                var menuView = bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;
                if (menuView == null)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to find BottomNavigationMenuView");
                    return;
                }


                var shiftMode = menuView.Class.GetDeclaredField("mShiftingMode");

                shiftMode.Accessible = true;
                shiftMode.SetBoolean(menuView, enableShiftMode);
                shiftMode.Accessible = false;
                shiftMode.Dispose();


                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    var item = menuView.GetChildAt(i) as BottomNavigationItemView;
                    if (item == null)
                        continue;

                    item.SetShiftingMode(enableShiftMode);
                    item.SetChecked(item.ItemData.IsChecked);
                }

                menuView.UpdateMenuView();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unable to set shift mode: {ex}");
            }
        }
    }
}



  

