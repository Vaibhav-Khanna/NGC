using System;
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
            await CoreMethods.DisplayAlert("Déconnexion", "Êtes-vous sûr de vouloir vous déconnecter ?", "Oui", "Non");
        });


      

    }
}
