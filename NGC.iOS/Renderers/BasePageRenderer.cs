using System;
using System.Collections.Generic;
using System.Linq;
using NGC.iOS.Renderers;
using NGC.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BasePage), typeof(BasePageRenderer))]
namespace NGC.iOS.Renderers
{
    public class BasePageRenderer : PageRenderer
    {

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);


            if (NavigationController == null)
                return;


            var itemsInfo = (this.Element as ContentPage).ToolbarItems.ToList();


            var navigationItem = this.NavigationController.TopViewController.NavigationItem;

            var rightnewlist = new List<UIBarButtonItem>();
            var leftnewlist = new List<UIBarButtonItem>();

            itemsInfo.ForEach((obj) =>
            {
                if (obj.Priority == 1)
                {
                    rightnewlist.Add(obj.ToUIBarButtonItem());
                }
                else
                {
                    leftnewlist.Add(obj.ToUIBarButtonItem());
                }
            });

            navigationItem.RightBarButtonItems = rightnewlist.ToArray();
            navigationItem.LeftBarButtonItems = leftnewlist.ToArray();

        }


    }
}
