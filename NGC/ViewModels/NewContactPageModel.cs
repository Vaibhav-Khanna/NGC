using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using NGC.Resources;
using static NGC.Models.DataObjects.OpenDataResponse;
using NGC.Models.DataObjects;
using NGC.DataModels;

namespace NGC.ViewModels
{
    public class NewContactPageModel : BaseViewModel
    {

        #region Public Properties

        public ObservableCollection<string> CompanyDetails { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> ContactNumbers { get; set; } = new ObservableCollection<string>(){ "sdsd" };

        public ObservableCollection<Tag> Tags { get; set; }

        public ObservableCollection<string> Qualifications { get; set; }

        public string SelectedQualification { get; set; }
                   
        public Tag SelectedTag { get; set; }

        public bool IsSegmentVisible { get; set; }
     
        public bool IsContactTab { get; set; }

        public string CompanyLabel { get; set; }

        private bool IsNewCreation;

        private bool IsCompany;

        public object Model { get; set; }


        #endregion




        #region Command


        public Command BackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(null, true);
        });


        public Command SaveCommand => new Command(async () =>
        {          
            if (Model != null)
            {
                var newContactModel = Model as ContactModel;

                newContactModel.SaveToDataObject(null, null, null); 
            }

            await CoreMethods.PopPageModel(null, true);
        });


        public Command AddCommand => new Command(() =>
        {
            ContactNumbers.Add("sdsd");
        });

        public Command WeightCommand => new Command((obj) =>
        {
            (Model as ContactModel).Rating = Convert.ToInt32(obj as string);
        });

        public Command SearchCompanyCommand => new Command(async () =>
        {
            var result = await ToastService.ShowSearchCompany();

            if (result is Record)
            {
                CompanyLabel = ((Record)result).fields.nomen_long;
            }
            else
            {
                CompanyLabel = null;
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

                if (IsNewCreation)
                {
                    if (IsContactTab)
                    {
                        if (IsSegmentVisible)
                        {
                            // Creating a new Professional Contact
                            Model = new ContactModel(null, null);
                        }
                        else
                        {
                            // Creating a regular personal contact

                            Model = new ContactModel(null,null);
                        }
                    }
                    else
                    {
                        // Creating a new professional Company
                        Model = new ContactModel(null, null);
                    }
                }
                else
                {
                    if (IsContactTab)
                    {
                        if (IsSegmentVisible)
                        {
                            // modifying a Professional Contact
                        }
                        else
                        {
                            // modifying a regular personal contact

                            Model = ((Tuple<bool, bool, object>)initData).Item3;

                        }
                    }
                    else
                    {
                        // modifying a professional Company
                    }
                }

                GetAdditionalData();

                //mockdata
                CompanyDetails.Add("Adresse postale");
                CompanyDetails.Add("Email");
                CompanyDetails.Add("Tel. Bureau");
                CompanyDetails.Add("Tel. Mobile");
                CompanyDetails.Add("Web");
                CompanyDetails.Add("Siret");
                CompanyDetails.Add("Statut juridique");
                CompanyDetails.Add("APE");
                CompanyDetails.Add("APE libéllé");
                CompanyDetails.Add("APE sous classe");
                CompanyDetails.Add("Effectifs");
                //
            }
        }

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
