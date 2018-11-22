using System;
using Xamarin.Forms;
using NGC.Helpers;

namespace NGC.ViewModels
{
    public class LoginPageModel : BaseViewModel
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public bool FocusNext { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

        }

        public Command LoginCommand => new Command(() =>
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                var tabbedPage = TabNavigator.GenerateTabPage();

                Application.Current.MainPage = tabbedPage;
            });

        });

        public Command ConnectMicrosoft => new Command(() =>
        {

        });

        public Command ForgotCommand => new Command(() =>
        {

        });

        public Command NextCommand => new Command(() =>
        {
            FocusNext = !FocusNext;
        });

    }
}
