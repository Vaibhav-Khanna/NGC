using System;
using System.Collections.ObjectModel;
using System.Linq;

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
        public string Header { get; set; }

        public string Detail { get; set; }
    }
}
