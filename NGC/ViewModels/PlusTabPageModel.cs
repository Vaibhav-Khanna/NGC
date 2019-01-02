using System;
using FreshMvvm;
using NGC.Helpers;
using Xamarin.Forms;

namespace NGC.ViewModels
{
    public class PlusTabPageModel : BaseViewModel
    {
        public PlusTabPageModel()
        {

        }


        bool currentLocationAvailable;


        public Command MapCommand => new Command(async() =>
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                currentLocationAvailable = await Permissions.CheckPermissionLocation();

                if (currentLocationAvailable)
                    await CoreMethods.PushPageModel<MapPageModel>();
            }
            else
                await CoreMethods.PushPageModel<MapPageModel>();
        });


        public Command LogOutCommand => new Command(async() =>
        {
           var result = await CoreMethods.DisplayAlert("Déconnexion", "Êtes-vous sûr de vouloir vous déconnecter ?", "Oui", "Non");

            if (result)
            {
                ToastService.ShowLoading();

                var isloggedOut =  await StoreManager.LogoutAsync();

                ToastService.HideLoading();

                if (isloggedOut)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();

                        var container = new FreshNavigationContainer(page) { BarTextColor = Color.Black };

                        Application.Current.MainPage = container;
                    });
                }
            }
        });


      

    }
}
