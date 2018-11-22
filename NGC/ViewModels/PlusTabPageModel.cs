using System;
using Xamarin.Forms;

namespace NGC.ViewModels
{
    public class PlusTabPageModel : BaseViewModel
    {
        public PlusTabPageModel()
        {
        }


        public Command MapCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModel<MapPageModel>();
        });

        public Command LogOutCommand => new Command(async() =>
        {
            await CoreMethods.DisplayAlert("Déconnexion", "Êtes-vous sûr de vouloir vous déconnecter ?", "Oui", "Non");
        });

    }
}
