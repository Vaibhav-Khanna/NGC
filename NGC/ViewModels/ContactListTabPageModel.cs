﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGC.Helpers;
using NGC.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NGC.DataModels;
using System.Collections.Generic;
using NGC.Models.DataObjects;

namespace NGC.ViewModels
{
    public class ContactListTabPageModel : BaseViewModel
    {

        int TabIndex { get; set; } = 0;

        public bool IsFilterActive { get; set; }

        string _searchtext;
        [PropertyChanged.DoNotNotify]
        public string SearchText { get { return _searchtext; } set { _searchtext = value; Search(); RaisePropertyChanged(); } }

        ObservableCollection<ObservableGroupCollection<ContactModel>> AllContacts;
        public ObservableCollection<ObservableGroupCollection<ContactModel>> Contacts { get; set; }

        ObservableCollection<ObservableGroupCollection<CompanyModel>> AllCompanies;
        public ObservableCollection<ObservableGroupCollection<CompanyModel>> Companies { get; set; }

        List<FilterCategoryModel> Filters { get; set; }


        public async override void Init(object initData)
        {
            base.Init(initData);

            GetFilterData();

            IsLoading = true;

            await GetData();
           
            IsLoading = false;
        }


        public override void ReverseInit(object returnedData)
        {
            if (returnedData is IEnumerable<FilterCategoryModel>)
            {
                var activeFilters = (IEnumerable<FilterCategoryModel>)returnedData;

                if (activeFilters.Any())
                {
                    IsFilterActive = true;
                }
                else
                {
                    IsFilterActive = false;
                }
            }
            else if (returnedData is Contact)
            {
                IsRefreshing = true;

                RefreshCommand.Execute(null);
            }
            else if (returnedData is Company)
            {
                IsRefreshing = true;

                RefreshCommand.Execute(null);
            }

            base.ReverseInit(returnedData);
        }


        public Command RefreshCommand => new Command(async() =>
        {
            await GetData();

            IsRefreshing = false;
        });


        public Command SelectedCommand => new Command(async(obj) =>
        {
            await CoreMethods.PushPageModel<ContactListDetailPageModel>(obj);
        });

        public Command CompanySelectedCommand => new Command(async(obj) =>
        {
            await CoreMethods.PushPageModel<CompanyDetailPageModel>(obj);
        });

        public Command AddCommand => new Command(async() =>
        {
            var response = await ToastService.ShowActionSheet("Ajouter", new List<Tuple<string, string>>() { new Tuple<string, string>("iconparticulier.png", "Particulier"), new Tuple<string, string>("iconpro.png", "Professionnel") }, "Annuler");

            if (response == "Particulier")
                await CoreMethods.PushPageModel<NewContactPageModel>(new Tuple<bool, bool, object>(false, true, null), true,true);
            else if (response == "Professionnel")
                await CoreMethods.PushPageModel<NewContactPageModel>(new Tuple<bool, bool, object>(true, true, null), true,true);
        });


        public Command FilterCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModelWithNewNavigation<FilterListPageModel>(Filters);
        });


        public Command LoadMoreCommand => new Command(async(obj) =>
        {
            if (TabIndex == 0)
            {
                var contacts_data = await StoreManager.ContactStore.GetNextItemsAsync(Contacts.Count);

                if (contacts_data != null && contacts_data.Any())
                {

                }
            }
        });

        public void TabSelectedChanged(int index)
        {     
            TabIndex = index;

            Search();

            IsEmpty = !(index == 0 ? Contacts != null && Contacts.Any() : Companies != null && Companies.Any());
        }

        void Search()
        {
            if (TabIndex == 0)
            {
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    List<ContactModel> search_results = new List<ContactModel>();

                    foreach (var item in AllContacts)
                    {
                        var res = item.Where((arg) => arg.Name.ToLower().Contains(SearchText.ToLower()));
                        search_results.AddRange(res);
                    }

                    Contacts = new ObservableCollection<ObservableGroupCollection<ContactModel>>();

                    foreach (var item in search_results.GroupBy((arg) => arg.LastName?.First()).OrderBy((arg) => arg.Key))
                    {
                        var group = new ObservableGroupCollection<ContactModel>() { Key = item.Key?.ToString()?.ToUpper() };

                        foreach (var x in item)
                        {
                            group.Add(x);
                        }

                        Contacts.Add(group);
                    }
                }
                else
                    Contacts = AllContacts;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    List<CompanyModel> search_results = new List<CompanyModel>();

                    foreach (var item in AllCompanies)
                    {
                        var res = item.Where((arg) => arg.CompanyName.ToLower().Contains(SearchText.ToLower()));
                        search_results.AddRange(res);
                    }

                    Companies = new ObservableCollection<ObservableGroupCollection<CompanyModel>>();

                    foreach (var item in search_results.GroupBy((arg) => arg.CompanyName?.First()).OrderBy((arg) => arg.Key))
                    {
                        var group = new ObservableGroupCollection<CompanyModel>() { Key = item.Key?.ToString()?.ToUpper() };

                        foreach (var x in item)
                        {
                            group.Add(x);
                        }

                        Companies.Add(group);
                    }
                }
                else
                    Companies = AllCompanies;
            }

            FilterData();
        }


        void GetFilterData()
        {
            Filters = new List<FilterCategoryModel>();

            var ft1 = new FilterCategoryModel() { CategoryName = "Statut", IsMultipleSelector = true };
            ft1.AddFilterCategory(new string[] { "Tous", "Lead", "Contact", "Client" });

            var ft2 = new FilterCategoryModel() { CategoryName = "Check In", IsMultipleSelector = true };
            ft2.AddFilterCategory(new string[] { "Tous","Rouge", "Orange", "Vert", "Noir" });

            var ft3 = new FilterCategoryModel() { CategoryName = "Poids", IsMultipleSelector = true };
            ft3.AddFilterCategory(new string[] { "0", "1", "2", "3", "4", "5" });

            var ft4 = new FilterCategoryModel() { CategoryName = "Tags", IsMultipleSelector = true };
            ft4.AddFilterCategory(new string[] { "Tous","Admin", "Assistant","Patron","Consultant" });


            var ft5 = new FilterCategoryModel() { CategoryName = "Code postal", FilterType = FilterType.SearchCode, IsMultipleSelector = true };

            Filters.Add(ft1);
            Filters.Add(ft2);
            Filters.Add(ft3);
            Filters.Add(ft4);
            Filters.Add(ft5);
        }

        async Task GetData()
        {
            var contacts_data = await StoreManager.ContactStore.GetItemsAsync(true,true);
           
            var company_data = await StoreManager.CompanyStore.GetItemsAsync(true, true);

            #region Contacts

            Contacts = new ObservableCollection<ObservableGroupCollection<ContactModel>>();

            foreach (var item in contacts_data.GroupBy((arg) => arg.Lastname?.First()).OrderBy((arg) => arg.Key))
            {
                var group = new ObservableGroupCollection<ContactModel>() { Key = item.Key?.ToString()?.ToUpper() };

                foreach (var x in item)
                {
                    ContactModel t;
                    group.Add(t = new ContactModel(x));
                }

                Contacts.Add(group);
            }

            #endregion

            #region Comapnies


            Companies = new ObservableCollection<ObservableGroupCollection<CompanyModel>>();

            foreach (var item in company_data?.Where((arg) => arg.Name != null).OrderBy((arg) => arg.Name))
            {
                var group = new ObservableGroupCollection<CompanyModel>() { Key = item.Name?.First().ToString()?.ToUpper() };

                List<ContactModel> contacts = new List<ContactModel>();

                foreach (var x in Contacts)
                {
                   contacts.AddRange(x.Where((arg) => arg.Contact.CompanyId == item.Id));
                }

                group.Add(new CompanyModel(item){ Contacts = new ObservableCollection<ContactModel>(contacts) });

                Companies.Add(group);
            }

            #endregion

            AllContacts = Contacts;

            AllCompanies = Companies;

            TabSelectedChanged(TabIndex);

        }



        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if(Filters!=null)
                IsFilterActive = Filters.Any((arg) => arg.IsSelected);

            FilterData();
        }


        void GroupContacts(IEnumerable<ContactModel> list)
        {
            Contacts = new ObservableCollection<ObservableGroupCollection<ContactModel>>();

            foreach (var item in list.GroupBy((arg) => arg.LastName?.First()).OrderBy((arg) => arg.Key))
            {
                var group = new ObservableGroupCollection<ContactModel>() { Key = item.Key?.ToString()?.ToUpper() };

                foreach (var x in item)
                {
                    group.Add(x);
                }

                Contacts.Add(group);
            }
        }


        void FilterData()
        {
            List<ContactModel> dataToFilter = new List<ContactModel>();

            if(AllContacts!=null)
            foreach (var item in AllContacts)
            {
                foreach (var x in item)
                {
                    dataToFilter.Add(x);
                }
            }

            if (!IsFilterActive)
            {
                if(AllContacts != null && AllContacts.Any() )
                GroupContacts(dataToFilter);

                return;
            }

            var temp_list = new List<ContactModel>();

            foreach (var category in Filters)
            {           
                if (category.ActiveFilters > 0)
                {
                    temp_list.Clear();

                    switch (category.CategoryName)
                    {
                        case "Statut":
                            {
                                foreach (var selected_filter in category.SelectedFilters)
                                {
                                    if (selected_filter.PropertyName != "Tous")
                                    {
                                        temp_list.AddRange(dataToFilter.Where((arg) => arg.Contact.State == selected_filter.PropertyName));
                                    }
                                    else
                                    {
                                        temp_list.AddRange(dataToFilter);
                                    }
                                }

                                break;
                            }
                        case "Check In":
                            {
                                foreach (var selected_filter in category.SelectedFilters)
                                {
                                    if (selected_filter.PropertyName != "Tous")
                                    {
                                        temp_list.AddRange(dataToFilter.Where((arg) => arg.RatingColorFrenchText == selected_filter.PropertyName));
                                    }
                                    else
                                    {
                                        temp_list.AddRange(dataToFilter);
                                    }
                                }
                            }
                            break;
                        case "Poids":
                            {
                                foreach (var selected_filter in category.SelectedFilters)
                                {
                                    temp_list.AddRange(dataToFilter.Where((arg) => arg.Rating.ToString() == selected_filter.PropertyName ));
                                }
                            }
                            break;
                        case "Tags":
                            {
                                foreach (var selected_filter in category.SelectedFilters)
                                {
                                    if (selected_filter.PropertyName != "Tous")
                                    {
                                        temp_list.AddRange(dataToFilter.Where((arg) => arg.JobTitle == selected_filter.PropertyName));
                                    }
                                    else
                                    {
                                        temp_list.AddRange(dataToFilter);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    dataToFilter = temp_list.ToList();
                }
            }

            GroupContacts(temp_list.Distinct());

        }


    }
}
