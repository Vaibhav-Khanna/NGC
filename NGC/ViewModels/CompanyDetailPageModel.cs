using System;
using System.Collections.ObjectModel;
using NGC.DataModels;
using NGC.Helpers;

namespace NGC.ViewModels
{
    public class CompanyDetailPageModel : BaseViewModel
    {

        public ObservableCollection<ObservableGroupCollection<ContactModel>> Contacts { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);


            if (initData is CompanyModel)
            {
                Contacts = new ObservableCollection<ObservableGroupCollection<ContactModel>>();

                var a = new ObservableGroupCollection<ContactModel>() { Key = "Contacts" };

                foreach (var item in ((CompanyModel)initData).Contacts)
                {
                    a.Add(item);
                }

                Contacts.Add(a);
            }

        }
    }
}
