using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace NGC.ViewModels
{
    public class CheckInPageModel : BaseViewModel
    {

        public ObservableCollection<string> Items { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            Items = new ObservableCollection<string>() { "SAV", "Commande", "Relance", "Signature contrat" };
        }

        public Command BackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(true);
        });


        public Command ItemSelected => new Command(async(obj) =>
        {
            await CoreMethods.PopPageModel(obj, true);
        });

    }
}
