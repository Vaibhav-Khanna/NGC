using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using NGC.ViewModels;
using NGC.Resources;
using System.Linq;
using static NGC.Models.DataObjects.OpenDataResponse;

namespace NGC.Helpers.PopUps
{
    public class SearchCompanyPageModel : BaseViewModel
    {
        public ObservableCollection<Record> ItemSource { get; set; }

        public bool IsResultAvailable { get; set; } = false;

        public bool IsHeaderVisible { get; set; }

        public string CountText { get; set; }


        public SearchCompanyPageModel()
        {
           
        }

        string _search;
        [PropertyChanged.DoNotNotify]
        public string SearchText
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                ItemSource = new ObservableCollection<Record>();
                Search();
                RaisePropertyChanged();
            }
        }


        async void Search()
        {
            IsHeaderVisible = false;
          
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                IsHeaderVisible = false;
                ItemSource = new ObservableCollection<Record>();
                return;
            }

            var results = await StoreManager.CompanyStore.SearchCompanyFromOpenData(SearchText);

            IsHeaderVisible = true;

            if (results != null && results.Any())
            {
                IsResultAvailable = true;

                CountText = $"{results.Count()} {AppResources.ResultsOpenData}";

                ItemSource = new ObservableCollection<Record>();

                foreach (var item in results)
                {
                    ItemSource.Add(item);
                }
            }
            else
            {
                IsResultAvailable = false;
            }

        }


        public Command ItemTapCommand => new Command((obj) =>
        {

        });

    }
}
