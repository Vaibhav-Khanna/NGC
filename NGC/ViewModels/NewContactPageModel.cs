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

namespace NGC.ViewModels
{
    public class NewContactPageModel : BaseViewModel
    {

        #region Public Properties

        public ObservableCollection<string> ContactNumbers { get; set; } = new ObservableCollection<string>(){ "sdsd" };

        public ObservableCollection<Tag> Tags { get; set; }

        public ObservableCollection<string> Qualifications { get; set; }


        public bool IsSegmentVisible { get; set; }
     
        public bool IsContactTab { get; set; }

        private bool IsNewCreation;


        #endregion


        #region Properties For Contact


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string Function { get; set; }

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
                    CompanyName = CompanyLabel,
                    AllowCheckin = ActiveCheckIn,
                    CollectSourceName = Source,
                    JobTitle = Function,
                    Qualification = Qualification,
                    Tags = Tag?.Name,
                    Weight = Rating,
                };

                if (CompanyObject != null)
                {

                    if (CompanyObject is Record)
                    {
                        // Create a new company and insert it
                        var obj = CompanyObject as Record;

                        var company = new Company()
                        {
                            Name = obj.Name,
                            Siret = CompanyDetails[5].Text,
                            Email = CompanyDetails[1].Text,
                        };

                        ToastService.ShowLoading("Creating Company");

                        var isadded = await StoreManager.CompanyStore.InsertAsync(company);

                        ToastService.HideLoading();

                        if (isadded)
                            CompanyObject = company;
                        else
                        {
                            ShowErrorMessage();
                            return;
                        } 
                         
                        //
                    }

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
                ShowErrorMessage();
            }
            else if (!IsNewCreation && IsContactTab && IsSegmentVisible) // Modify a professional contact 
            {
                ShowErrorMessage();
            }
            else if(!IsNewCreation && !IsContactTab && !IsSegmentVisible) // Modifying a company details
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


                if (!IsNewCreation && IsContactTab && !IsSegmentVisible) // Modify exisiting normal contact
                {

                }
                else if (!IsNewCreation && IsContactTab && IsSegmentVisible) // Modify a professional contact 
                {

                }
                else if (!IsNewCreation && !IsContactTab && !IsSegmentVisible) // Modifying a company details
                {
                   
                }
                
                GetAdditionalData();

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

            }
        }

        async Task<bool> ValidateInput(bool isPro)
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || (IsProfessional && string.IsNullOrWhiteSpace(CompanyLabel)))
            {
                await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.Error, AppResources.Ok);
                return false;
            }

            return true;
        }

        async void ShowErrorMessage()
        {
            await CoreMethods.DisplayAlert(AppResources.Error, "We could not process this request at this time. Please try again.", AppResources.Ok);
        }

        // Get Tags and Qualifications for the pickers
        async void GetAdditionalData() 
        {
            Tags = new ObservableCollection<Tag>();

            Qualifications = new ObservableCollection<string>(Constants.StaticQualifications);

            var tags = await StoreManager.TagStore.GetItemsAsync();

            if (tags != null && tags.Any())
                Tags = new ObservableCollection<Tag>(tags);
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
