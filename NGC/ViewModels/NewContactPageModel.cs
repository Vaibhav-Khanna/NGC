using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace NGC.ViewModels
{
    public class NewContactPageModel : BaseViewModel
    {
        public NewContactPageModel()
        {

        }

        public ObservableCollection<string> CompanyDetails { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> ContactNumbers { get; set; } = new ObservableCollection<string>(){ "sdsd" };

        public bool IsProfessional { get; set; }

        public bool IsContactTab { get; set; } = true;


        public Command BackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(null, true);
        });

        public Command SaveCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(null, true);
        });

        public Command AddCommand => new Command(() =>
        {
            ContactNumbers.Add("sdsd");
        });

        public override void Init(object initData)
        {
            base.Init(initData);

            IsProfessional = (bool)initData;

            //mockdata

            CompanyDetails.Add("Adresse postale");
            CompanyDetails.Add("Email");
            CompanyDetails.Add("Tel. Bureau");
            CompanyDetails.Add("Tel. Mobile");
            CompanyDetails.Add("Web");
            CompanyDetails.Add("Siret");
            CompanyDetails.Add("Statut juridique");
            CompanyDetails.Add("APE");
            CompanyDetails.Add("APE libéllé");
            CompanyDetails.Add("APE sous classe");
            CompanyDetails.Add("Effectifs");
           

            //

        }

        public void TabSelectedChanged(int index)
        {
            IsContactTab = index == 0;
        }

    }
}
