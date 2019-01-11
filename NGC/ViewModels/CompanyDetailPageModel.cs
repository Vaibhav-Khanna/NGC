using System;
using System.Collections.ObjectModel;
using NGC.DataModels;
using NGC.Helpers;
using Xamarin.Forms;
using Xamarin.Essentials;
using NGC.Models.DataObjects;
using NGC.Resources;
using Map = Xamarin.Essentials.Map;

namespace NGC.ViewModels
{
    public class CompanyDetailPageModel : BaseViewModel
    {

        public ObservableCollection<ObservableGroupCollection<ContactModel>> Contacts { get; set; }

        public CompanyModel Company { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is CompanyModel)
            {
                Company = ((CompanyModel)initData);

                Contacts = new ObservableCollection<ObservableGroupCollection<ContactModel>>();

                var a = new ObservableGroupCollection<ContactModel>() { Key = "Contacts" };

                foreach (var item in ((CompanyModel)initData).Contacts)
                {
                    a.Add(item);
                }

                Contacts.Add(a);
            }
        }


        public Command ContactDetailCommand => new Command(async(obj) =>
        {
            await CoreMethods.PushPageModel<ContactListDetailPageModel>(obj);
        });


        public Command MapCommand => new Command(async () =>
        {
            if (Company.Company.Latitude == 0 || Company.Company.Longitude == 0)
            {
                await CoreMethods.DisplayAlert(AppResources.Error, AppResources.LocationNotFound, AppResources.Ok);
            }
            else
                await Map.OpenAsync(Company.Company.Latitude, Company.Company.Longitude);
        });

        public Command ModifyCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<NewContactPageModel>(data:new Tuple<bool, bool, object>(false, false, Company.Company ), modal:true);
        });

    }
}
