using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NewContactModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Qualification { get; set; }

        public List<string> Tags { get; set; }

        public int Weight { get; set; }

        public string Email { get; set; }

        public List<TelephoneNumbers> Numbers { get; set; }

        public string Source { get; set; }

        public bool ActiveCheckIn { get; set; }
    }

    public class TelephoneNumbers
    {
        public string Type { get; set; }

        public string Mobile { get; set; }
    }
}
