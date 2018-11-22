using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using NGC.iOS.Effects;

[assembly: Xamarin.Forms.ResolutionGroupName("NGC")]
[assembly: Xamarin.Forms.ExportEffect(typeof(ButtonAlignmentEffect), "ButtonTextAlignmentEffect")]
namespace NGC.iOS.Effects
{
    public class ButtonAlignmentEffect : PlatformEffect
    {
       
        protected override void OnAttached()
        {
            if (Control != null)
            {
                var native_button = Control as UIButton;

                native_button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            }
        }

        protected override void OnDetached()
        {
           
        }
    }
}
