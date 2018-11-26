using System;
using System.Collections.ObjectModel;
using NGC.Models;
using Xamarin.Forms;

namespace NGC.ViewModels 
{
    public class OpportunityTabPageModel : BaseViewModel
    {
        public ObservableCollection<OpportunityTabModel> ItemSource { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; }

        int _position;
        [PropertyChanged.DoNotNotify]
        public int Position 
        {
            get { return _position; }
            set
            {
                _position = value;

                if(ItemSource!=null)
                {
                    Header = ItemSource[Position].Header;
                    Detail = ItemSource[Position].Detail;
                }

                RaisePropertyChanged();
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var leads = new OpportunityTabModel() { Header = "Lead", Detail = "2 offres - 30.000 €" };
            leads.ItemSource = new ObservableCollection<OpportunityContacts>();
            leads.ItemSource.Add(new OpportunityContacts() { Header = "Signature contrat", Detail = "Chris Grut - 10.000 €" });
            leads.ItemSource.Add(new OpportunityContacts() { Header = "Audit Softech", Detail = "Quentin Hulo - 20.000 €" });


            var contacts = new OpportunityTabModel() { Header = "Contact", Detail = "1 offre - 9.000 €" };
            contacts.ItemSource = new ObservableCollection<OpportunityContacts>();
            contacts.ItemSource.Add(new OpportunityContacts() { Header = "Facture relance", Detail = "Ben Cobra - 9.000 €" });

            var client = new OpportunityTabModel() { Header = "Client", Detail = "0 offre - 0 €" };
            client.ItemSource = new ObservableCollection<OpportunityContacts>();

            var unknown = new OpportunityTabModel() { Header = "Unknown", Detail = "0 offre - 0 €" };
            unknown.ItemSource = new ObservableCollection<OpportunityContacts>();

            ItemSource = new ObservableCollection<OpportunityTabModel>();
            ItemSource.Add(leads);
            ItemSource.Add(contacts);
            ItemSource.Add(client);
            ItemSource.Add(unknown);

            Position = 0;
        }


        public Command NavigateCommand => new Command((obj) =>
        {

        });
    }
}
