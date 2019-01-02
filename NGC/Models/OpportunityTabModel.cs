using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGC.Models.DataObjects;

namespace NGC.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class OpportunityTabModel
    {
        public string Header { get; set; }

        public string Detail { get; set; }

        public bool ShowFooter { get { return ItemSource != null && !ItemSource.Any(); } }

        public ObservableCollection<OpportunityContacts> ItemSource { get; set; }
    }

    public class OpportunityContacts
    {
        public OpportunityContacts(Opportunity opportunity)
        {
            Opportunity = opportunity;

            Header = opportunity.Subject;

            Detail = $"{opportunity.ContactFirstname} {opportunity.ContactLastname} - {opportunity.Amount} €";

            Amount = opportunity.Amount;

            ContactName = $"{opportunity.ContactFirstname} {opportunity.ContactLastname}";
        }

        public Opportunity Opportunity { get; set; }

        public long Amount { get; set; }

        public string ContactName { get; set; }

        public string Header { get; set; }

        public string Detail { get; set; }
    }
}
