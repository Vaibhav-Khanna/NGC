using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGC.Models.DataObjects;
using NGC.Resources;
using NGC.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace NGC.Helpers.PopUps
{
    public class SearchContactPageModel : BaseViewModel
    {

        public ObservableCollection<Contact> ItemSource { get; set; }

        public bool IsResultAvailable { get; set; } = false;

        public bool IsHeaderVisible { get; set; }

        public string CountText { get; set; }


      
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
                ItemSource = new ObservableCollection<Contact>();
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
                ItemSource = new ObservableCollection<Contact>();
                return;
            }

            var results = await StoreManager.ContactStore.SearchCompany(SearchText);

            IsHeaderVisible = true;

            if (results != null && results.Any())
            {
                IsResultAvailable = true;

                CountText = $"{results.Count()} {AppResources.ResultsOpenData}";

                ItemSource = new ObservableCollection<Contact>();

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


        public Command ItemTapCommand => new Command(async(obj) =>
        {
            (this.CurrentPage as SearchContactPage).Result = obj as Contact;

            await PopupNavigation.Instance.PopAllAsync();
        });
    }
}
