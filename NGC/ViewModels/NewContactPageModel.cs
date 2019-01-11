using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using NGC.Resources;
using static NGC.Models.DataObjects.OpenDataResponse;
using NGC.Models.DataObjects;
using NGC.DataModels;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using NGC.Models;

namespace NGC.ViewModels
{
    public class NewContactPageModel : BaseViewModel
    {

        #region Public Properties

        public ObservableCollection<string> ContactNumbers { get; set; } = new ObservableCollection<string>(){ "sdsd" };

        public ObservableCollection<Tag> Tags { get; set; }

        public ObservableCollection<string> Qualifications { get; set; }

        public ObservableCollection<CountryCode> CountryCodes { get; set; }

        public bool IsSegmentVisible { get; set; }
     
        public bool IsContactTab { get; set; }

        private bool IsNewCreation;

        public CountryCode SelectedCountry { get; set; }

        public Contact ExistingContact { get; set; }

        #endregion


        #region Properties For Contact


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string Function { get; set; }

        public string Mobile { get; set; }

        public int Rating { get; set; }

        public string Qualification { get; set; }

        public Tag Tag { get; set; }

        public string Source { get; set; }

        public bool ActiveCheckIn { get; set; }

        #endregion

        #region CompanyRegion

        public object CompanyObject { get; set; }

        public bool IsProfessional { get; set; }

        public string CompanyLabel { get; set; }

        public ObservableCollection<CompanyDynamicEntry> CompanyDetails { get; set; } = new ObservableCollection<CompanyDynamicEntry>();

        #endregion

        #region Command


        public Command BackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(null, true);
        });


        public Command SaveCommand => new Command(async () =>
        {
            if (IsNewCreation && IsContactTab && !IsSegmentVisible) // Create a private contact
            {
                if (!await ValidateInput(false))
                    return;

                var contact = new Contact()
                {
                    Firstname = FirstName,
                    Lastname = LastName,
                    Email = Email,
                    AllowCheckin = ActiveCheckIn,
                    Mobile = string.IsNullOrWhiteSpace(Mobile) ? "" : SelectedCountry.DialCode + "-" + Mobile,
                    CollectSourceName = Source,
                    Qualification = Qualification,
                    Tags = Tag?.Name,
                    Weight = Rating,
                };

                ToastService.ShowLoading(AppResources.Save);

                var isCreated = await StoreManager.ContactStore.InsertAsync(contact);

                ToastService.HideLoading();

                if (isCreated)
                    await CoreMethods.PopPageModel(contact, true);
                else
                    ShowErrorMessage();
            }
            else if (IsNewCreation && IsContactTab && IsSegmentVisible) // Create a professional Contact
            {
                if (!await ValidateInput(true))
                    return;

                var contact = new Contact()
                {
                    Firstname = FirstName,
                    Lastname = LastName,
                    Email = Email,
                    CompanyName = Company,
                    Mobile = string.IsNullOrWhiteSpace(Mobile) ? "" : SelectedCountry.DialCode + "-" + Mobile,
                    AllowCheckin = ActiveCheckIn,
                    CollectSourceName = Source,
                    JobTitle = Function,
                    Qualification = Qualification,
                    Tags = Tag?.Name,
                    Weight = Rating,
                };

                var iscreated = await CreateCompany();

                if (!iscreated)
                    return;

                if (CompanyObject is Company)
                {
                    var obj = CompanyObject as Company;

                    contact.CompanyId = obj.Id;
                    contact.CompanyName = Company;
                    contact.CompanyCity = obj.City;
                    contact.CompanyState = obj.State;
                    contact.CompanyCountry = obj.Country;
                    contact.CompanyStreet1 = obj.Street1;
                    contact.CompanyStreet2 = obj.Street2;
                    contact.CompanyZipCode = obj.ZipCode;
                    contact.CompanyLatitude = obj.Latitude;
                    contact.CompanyLongitude = obj.Longitude;
                }


                ToastService.ShowLoading(AppResources.Save);

                var isCreated = await StoreManager.ContactStore.InsertAsync(contact);

                ToastService.HideLoading();

                if (isCreated)
                    await CoreMethods.PopPageModel(contact, true);
                else
                    ShowErrorMessage();
            }
            else if (!IsNewCreation && IsContactTab && !IsSegmentVisible) // Modify exisiting normal contact
            {
                var contact = ExistingContact;

                contact.Firstname = FirstName;
                contact.Lastname = LastName;
                contact.Email = Email;
                contact.CompanyName = Company;
                contact.Mobile = string.IsNullOrWhiteSpace(Mobile) ? "" : SelectedCountry.DialCode + "-" + Mobile;
                contact.AllowCheckin = ActiveCheckIn;
                contact.CollectSourceName = Source;
                contact.JobTitle = Function;
                contact.Qualification = Qualification;
                contact.Tags = Tag?.Name;
                contact.Weight = Rating;

                ToastService.ShowLoading(AppResources.Save);

                var isCreated = await StoreManager.ContactStore.UpdateAsync(contact);

                ToastService.HideLoading();

                if (isCreated)
                    await CoreMethods.PopPageModel(contact, true);
                else
                    ShowErrorMessage();

            }
            else if (!IsNewCreation && IsContactTab && IsSegmentVisible) // Modify a professional contact 
            {
                var contact = ExistingContact;

                contact.Firstname = FirstName;
                contact.Lastname = LastName;
                contact.Email = Email;
                contact.CompanyName = Company;
                contact.Mobile = string.IsNullOrWhiteSpace(Mobile) ? "" : SelectedCountry.DialCode + "-" + Mobile;
                contact.AllowCheckin = ActiveCheckIn;
                contact.CollectSourceName = Source;
                contact.JobTitle = Function;
                contact.Qualification = Qualification;
                contact.Tags = Tag?.Name;
                contact.Weight = Rating;

                var iscreated = await CreateCompany();

                if (!iscreated)
                    return;

                if (CompanyObject is Company)
                {
                    var obj = CompanyObject as Company;

                    contact.CompanyId = obj.Id;
                    contact.CompanyName = Company;
                    contact.CompanyCity = obj.City;
                    contact.CompanyState = obj.State;
                    contact.CompanyCountry = obj.Country;
                    contact.CompanyStreet1 = obj.Street1;
                    contact.CompanyStreet2 = obj.Street2;
                    contact.CompanyZipCode = obj.ZipCode;
                    contact.CompanyLatitude = obj.Latitude;
                    contact.CompanyLongitude = obj.Longitude;
                }

                ToastService.ShowLoading(AppResources.Save);

                var isCreated = await StoreManager.ContactStore.UpdateAsync(contact);

                ToastService.HideLoading();

                if (isCreated)
                    await CoreMethods.PopPageModel(contact, true);
                else
                    ShowErrorMessage();
            }
            else if (!IsNewCreation && !IsContactTab && !IsSegmentVisible) // Modifying a company details
            {
                ShowErrorMessage();
            }

        });


        public Command AddCommand => new Command(() =>
        {
            ContactNumbers.Add("sdsd");
        });

        public Command WeightCommand => new Command((obj) =>
        {
            Rating = Convert.ToInt32(obj as string);
        });


        public Command SearchCompanyCommand => new Command(async () =>
        {
            var result = await ToastService.ShowSearchCompany();

            if (result is Record)
            {
                CompanyLabel = ((Record)result).fields.nomen_long;
                Company = CompanyLabel;
                CompanyObject = result;
            }
            else if (result is Company)
            {
                CompanyLabel = ((Company)result).Name;
                Company = CompanyLabel;
                CompanyObject = result;
            }
            else
            {
                CompanyLabel = null;
                Company = CompanyLabel;
                CompanyObject = null;
            }

            if (CompanyObject is Company)
            {
                var obj = CompanyObject as Company;

                CompanyDetails[0].Text = obj.Address; // Address
                CompanyDetails[1].Text = obj.Email;
                CompanyDetails[2].Text = obj.Phone;
                CompanyDetails[3].Text = obj.Mobile;
                CompanyDetails[4].Text = obj.Web;
                CompanyDetails[5].Text = obj.Siret;
                CompanyDetails[6].Text = obj.StatutJuridique;
                CompanyDetails[7].Text = obj.Ape;
                CompanyDetails[8].Text = obj.ApeLibelle;
                CompanyDetails[9].Text = obj.ApeSousClasse;
                CompanyDetails[10].Text = obj.Effectif;
            }
            else if (CompanyObject is Record)
            {
                var obj = CompanyObject as Record;

                //CompanyDetails[0].Text = obj.fields.; // Address
                //CompanyDetails[1].Text = obj.Email; // email
                //CompanyDetails[2].Text = obj.Phone; // phone
                //CompanyDetails[3].Text = obj.Mobile; // mobile
                //CompanyDetails[4].Text = obj.Web; // web
                //CompanyDetails[5].Text = obj.fields.siret; // siret
                //CompanyDetails[6].Text = obj.; //statut juridique
                //CompanyDetails[7].Text = obj.fields.apet700; // ape
                //CompanyDetails[8].Text = obj.fields.lib; // apeLibelle
                //CompanyDetails[9].Text = obj.ApeSousClasse; // ApeSousClasse
                //CompanyDetails[10].Text = obj.Effectif; // Effectif
            }

        });


        #endregion

        #region Methods

        public override void Init(object initData)
        {

            base.Init(initData);


            if (initData is Tuple<bool, bool, object>)
            {
              
                if (((Tuple<bool, bool, object>)initData).Item1)
                    IsSegmentVisible = ((Tuple<bool, bool, object>)initData).Item1;

                if (((Tuple<bool, bool, object>)initData).Item2)
                    IsContactTab = ((Tuple<bool, bool, object>)initData).Item2;


                if (((Tuple<bool, bool, object>)initData).Item3 == null)
                {
                    Title = $"{AppResources.New} {AppResources.Contact}";
                    IsNewCreation = true;
                }
                else
                {
                    Title = $"{AppResources.Contact}";
                    IsNewCreation = false;
                }


                IsProfessional = IsSegmentVisible;

              
                if (!IsNewCreation)
                {
                    ExistingContact = (((Tuple<bool, bool, object>)initData).Item3 as ContactModel).Contact;

                    FirstName = ExistingContact.Firstname;
                    LastName = ExistingContact.Lastname;
                    Email = ExistingContact.Email;
                    Company = ExistingContact.CompanyName;
                    Function = ExistingContact.JobTitle;
                    Mobile = ExistingContact.Mobile?.Split('-')?.Last();
                    Rating = Convert.ToInt32(ExistingContact.Weight);
                    Qualification = ExistingContact.Qualification;
                    Source = ExistingContact.CollectSourceName;
                    ActiveCheckIn = ExistingContact.AllowCheckin;

                    if (IsContactTab && !IsSegmentVisible) // Modify exisiting normal contact
                    {

                    }
                    else if (IsContactTab && IsSegmentVisible) // Modify a professional contact 
                    {

                    }
                    else if (!IsContactTab && !IsSegmentVisible) // Modifying a company details
                    {

                    }
                }

               
                //mockdata
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Adresse postale" });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Email", Keyboard = Keyboard.Email });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Tel. Bureau", Keyboard = Keyboard.Telephone });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Tel. Mobile", Keyboard = Keyboard.Telephone });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Web", Keyboard = Keyboard.Url });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Siret" });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Statut juridique" });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "APE" });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "APE libéllé" });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "APE sous classe" });
                CompanyDetails.Add(new CompanyDynamicEntry() { Placeholder = "Effectifs" });
                //

                GetAdditionalData();
            }
        }

        async Task<bool> ValidateInput(bool isPro)
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) )
            {
                await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.EnterAllDetails, AppResources.Ok);
                return false;
            }

            return true;
        }

        async void ShowErrorMessage()
        {
            await CoreMethods.DisplayAlert(AppResources.Error, AppResources.NotEntertainRequest, AppResources.Ok);
        }

        async Task<bool> CreateCompany()
        {
            if (CompanyObject != null && CompanyObject is Record)
            {
                // Create a new company and insert it
                var obj = CompanyObject as Record;

                var company = new Company()
                {
                    Name = obj.Name,
                    Latitude = obj.geometry.coordinates[0],
                    Longitude = obj.geometry.coordinates[1],
                    Email = CompanyDetails[1].Text,
                    Phone = CompanyDetails[2].Text,
                    Mobile = CompanyDetails[3].Text,
                    Web = CompanyDetails[4].Text,
                    Siret = CompanyDetails[5].Text,
                    StatutJuridique = CompanyDetails[6].Text,
                    Ape = CompanyDetails[7].Text,
                    ApeLibelle = CompanyDetails[8].Text,
                    ApeSousClasse = CompanyDetails[9].Text,
                    Effectif = CompanyDetails[10].Text
                };

                ToastService.ShowLoading(AppResources.CreatingCompany);

                var isadded = await StoreManager.CompanyStore.InsertAsync(company);

                ToastService.HideLoading();

                if (isadded)
                {
                    CompanyObject = company;
                    return true;
                }
                else
                {
                    ShowErrorMessage();
                }
                //

                return false;
            }

            return true;
        }

        // Get Tags and Qualifications for the pickers
        async void GetAdditionalData() 
        {
            Tags = new ObservableCollection<Tag>();

            Qualifications = new ObservableCollection<string>(Constants.StaticQualifications);

            var tags = await StoreManager.TagStore.GetItemsAsync();

            if (tags != null && tags.Any())
                Tags = new ObservableCollection<Tag>(tags);

            var list_codes =  ReadJsonFile();

            if (list_codes != null)
            {
                CountryCodes = new ObservableCollection<CountryCode>(list_codes);

                SelectedCountry = CountryCodes.First((arg) => arg.Name == "France");
            }

            if (ExistingContact != null)
            {
                Qualification = ExistingContact.Qualification;

                if (!string.IsNullOrEmpty(ExistingContact.Mobile))
                {
                    var code = CountryCodes?.Where((arg) => arg.DialCode == ExistingContact?.Mobile?.Split('-')?.First())?.First();

                    if (code != null)
                        SelectedCountry = code;
                }

                if (!string.IsNullOrEmpty(ExistingContact.Tags))
                    Tag = Tags?.Where((arg) => arg.Name == ExistingContact?.Tags)?.First();
            }

        }

        List<CountryCode> ReadJsonFile()
        {
            var assembly = typeof(Pages.NewContactPage).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.CountryCodes.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();

                var data = JsonConvert.DeserializeObject<List<CountryCode>>(json);

                return data;
            }
        }


        public void TabSelectedChanged(int index)
        {
            if (!IsSegmentVisible)
                return;

            IsContactTab = index == 0;
        }

        #endregion
    }
}
