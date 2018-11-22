using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using SegmentedControl.FormsPlugin.iOS;
using SuaveControls.FloatingActionButton.iOS.Renderers;
using Syncfusion.ListView.XForms.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace NGC.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            var s = new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();

            global::Xamarin.Forms.Forms.Init();


            SfListViewRenderer.Init();

            FloatingActionButtonRenderer.InitRenderer();

            SegmentedControlRenderer.Init();

            LoadApplication(new App());


            UITabBar.Appearance.TintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["BottomTabTextTint"]).ToUIColor();


            var result = base.FinishedLaunching(app, options);

            UIApplication.SharedApplication.StatusBarHidden = false;

			return result;
        }
    }
}
