using System;
using NGC.Models.DataObjects;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactModel
    {
      
        public ContactModel(Contact contact)
        {
            Contact = contact;
             
            FirstName = contact.Firstname;
            LastName = contact.Lastname;
            Company = contact.CompanyName;
            Rating = contact.Weight.ToString();
            Email = contact.Email;
            Mobile = contact.Mobile;
            JobTitle = contact.JobTitle;

            GetCheckinColor();

            FilterColor = "#ec1414"; 
        }


        void GetCheckinColor()
        {
            if (!Contact.AllowCheckin)
                RatingColor = "#656565";
            else
            {
                var diff = DateTime.Now.Subtract(Contact.LastCheckinAt.DateTime).Days;

                if (diff >= 0 && diff <= Contact.FirstCheckinDuration)
                    RatingColor = "#7ed321";  //green
                else if (diff > Contact.FirstCheckinDuration && diff <= Contact.SecondCheckinDuration)
                    RatingColor = "#f5a623"; //orange
                else if (diff > Contact.SecondCheckinDuration)
                    RatingColor = "#ec1414"; //red
            }
        }


        public Contact Contact { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        public string Company { get; set; }

        public string Email { get; set; }

        public string JobTitle { get; set; }

        public string Rating { get; set; } 

        public string Mobile { get; set; }

        public string RatingColor { get; set; }

        public string FilterColor { get; set; }

    }
}
