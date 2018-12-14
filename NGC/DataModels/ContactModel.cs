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
            RefreshData(contact,null);
        }


        public ContactModel(Contact contact, Company company)
        {
            RefreshData(contact,company);
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


        #region ContactProperties

        public Company CompanyObject { get; set; }

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

        #endregion

        #region CompanyProperties

        public string CompanyAddress { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyTelephone { get; set; }

        public string CompanyMobile { get; set; }

        public string CompanyWebsite { get; set; }

        public string CompanySiret { get; set; }

        public string CompanyStatusJuridique { get; set; }

        public string CompanyAPE { get; set; }

        public string CompanyAPELabel { get; set; }

        public string CompanyAPEClass { get; set; }

        public string CompanyEffectifs { get; set; }

        #endregion

        public void SaveToDataObject(string userId, string CompanyId,string salesTeamId)
        { 
            if (Contact == null)
                Contact = new Contact();

            if (CompanyObject == null)
                CompanyObject = new Company();

            // contact properties set
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
            //

            //Company Properties Set

            CompanyObject.Address = CompanyAddress;

            //
        }

        public void RefreshData(Contact contact,Company company)
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

            CompanyObject = company;

            if (CompanyObject != null)
            {
                CompanyAddress = company.Address;
                CompanyEmail = company.Email;
                CompanyWebsite = company.Web;
                CompanyAPE = company.Ape;
                CompanyAPELabel = company.ApeLibelle;
                CompanyAPEClass = company.ApeSousClasse;
                CompanySiret = company.Siret;
                CompanyMobile = company.Mobile;
                CompanyTelephone = company.Phone;
                CompanyStatusJuridique = company.StatutJuridique;
                CompanyEffectifs = company.Effectif;
            }

        }

    }
}
