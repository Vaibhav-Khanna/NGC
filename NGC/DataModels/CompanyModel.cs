using System;
using System.Collections.ObjectModel;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CompanyModel
    {
        public CompanyModel()
        {

        }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public ObservableCollection<ContactModel> Contacts { get; set; }

    }
}
