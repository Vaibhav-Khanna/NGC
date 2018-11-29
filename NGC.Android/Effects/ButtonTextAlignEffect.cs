using System;
using Xamarin.Forms;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using NGC.Droid.Effects;

[assembly: Xamarin.Forms.ResolutionGroupName("NGC")]
[assembly: Xamarin.Forms.ExportEffect(typeof(ButtonTextAlignEffect), "ButtonTextAlignmentEffect")]
namespace NGC.Droid.Effects
{
	public class ButtonTextAlignEffect : PlatformEffect
    {
        public ButtonTextAlignEffect()
        {
        }

        protected override void OnAttached()
        {
            var bt = this.Control as Android.Widget.Button;

            bt.Gravity = Android.Views.GravityFlags.Left | Android.Views.GravityFlags.CenterVertical;
            
            bt.TextAlignment = Android.Views.TextAlignment.Gravity;
        }

        protected override void OnDetached()
        {

        }
    }
}
