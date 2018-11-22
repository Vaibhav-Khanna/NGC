using System;
using NGC.Models;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactModel
    {
        public ContactModel(Contact contact=null)
        {
            Contact = contact;

            //mock data
            FirstName = "Milton";
            LastName = "Aaron";
            Company = "Fresh Food";
            Rating = "3";
            RatingColor = "#f5a623";
            FilterColor = "#ec1414";
            //
        }

        public Contact Contact { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        public string Company { get; set; }

        public string Rating { get; set; } 

        public string RatingColor { get; set; }

        public string FilterColor { get; set; }
    }
}
