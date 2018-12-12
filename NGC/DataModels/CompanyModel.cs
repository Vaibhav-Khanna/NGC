using System;
using System.Collections.ObjectModel;
using NGC.Models.DataObjects;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CompanyModel
    {
        public CompanyModel(Company company)
        {
            Company = company;
            CompanyName = company.Name;
            Address = company.Address;
        }

        public Company Company { get; private set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public ObservableCollection<ContactModel> Contacts { get; set; }
    }
}
