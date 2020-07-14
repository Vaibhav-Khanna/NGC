using System;
using Xamarin.Forms;
using NGC.Helpers;
using System.Threading.Tasks;
using NGC.Resources;

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

        public Command LoginCommand => new Command(async() =>
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                ToastService.ShowLoading(null);

                var status = await StoreManager.LoginAsync(Email, Password);

                ToastService.HideLoading();

                if (!status)
                {
                    ToastService.ShowLoading(null);

                    await StoreManager.SyncAllAsync(true);

                    ToastService.HideLoading();

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var tabbedPage = TabNavigator.GenerateTabPage();

                        Application.Current.MainPage = tabbedPage;
                    });
                }
                else
                    await CoreMethods.DisplayAlert(AppResources.Error, AppResources.ErrorLogin, AppResources.Ok);
            }
            else
                await CoreMethods.DisplayAlert(AppResources.Error, AppResources.LoginCredentialsEmpty, AppResources.Ok);
        });


        public Command ConnectMicrosoft => new Command(() =>
        {

        });


        public Command ForgotCommand => new Command(async() =>
        {
            if (!string.IsNullOrWhiteSpace(Email))
            {
                ToastService.ShowLoading();

                var status = await StoreManager.ForgotPasswordAsync(Email);

                ToastService.HideLoading();

                if (status)
                {
                    await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PasswordSent, AppResources.Ok);
                }
            }
            else
                await CoreMethods.DisplayAlert(AppResources.Error, AppResources.LoginCredentialsEmpty, AppResources.Ok);
        });


        public Command NextCommand => new Command(() =>
        {
            FocusNext = !FocusNext;
        });

    }
}
