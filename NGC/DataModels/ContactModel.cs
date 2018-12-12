using System;
using System.Collections.Generic;
using NGC.Models.DataObjects;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactModel
    {
      
        public ContactModel(Contact contact)
        {
            Contact = contact;

            if (contact != null)
            {
                FirstName = contact.Firstname;
                LastName = contact.Lastname;
                Company = contact.CompanyName;
                Rating = Convert.ToInt32(contact.Weight);
                Email = contact.Email;
                Mobile = contact.Mobile;
                JobTitle = contact.JobTitle;
                Qualification = contact.Qualification;
                Tag = contact.Tags;
                Source = contact.CollectSourceName;
                ActiveCheckIn = contact.AllowCheckin;

                GetCheckinColor();
            }

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

        public int Rating { get; set; } 

        public string Mobile { get; set; }

        public string RatingColor { get; set; } = "#656565";

        public string FilterColor { get; set; }


        public string Qualification { get; set; }

        public string Tag { get; set; }

        public string Source { get; set; }

        public bool ActiveCheckIn { get; set; }


        public void SaveToDataObject(string userId, string CompanyId,string salesTeamId)
        {
            if (string.IsNullOrEmpty(userId))
                return;

            if (Contact == null)
                Contact = new Contact();

            Contact.Firstname = FirstName;
            Contact.Lastname = LastName;
            Contact.Email = Email;
            Contact.Weight = Rating;
            Contact.CompanyId = CompanyId;
            Contact.CompanyName = Company;
            Contact.JobTitle = JobTitle;
            Contact.Mobile = Mobile;
            Contact.Qualification = Qualification;
            Contact.Tags = Tag;
            Contact.CollectSourceName = Source;
            Contact.AllowCheckin = ActiveCheckIn;
            Contact.UserId = userId;
            Contact.SalesTeamId = salesTeamId;
        }

    }
}
