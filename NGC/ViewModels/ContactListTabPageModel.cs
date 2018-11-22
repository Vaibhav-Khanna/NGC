using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGC.Helpers;
using NGC.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NGC.DataModels;
using System.Collections.Generic;


namespace NGC.ViewModels
{
    public class ContactListTabPageModel : BaseViewModel
    {

        public bool IsFilterActive { get; set; }

        string _searchtext;
        [PropertyChanged.DoNotNotify]
        public string SearchText { get { return _searchtext; } set { _searchtext = value; Search(); RaisePropertyChanged(); } }

        public ObservableCollection<ObservableGroupCollection<ContactModel>> Contacts { get; set; }

        private List<FilterCategoryModel> Filters { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            GetFilterData();

            Contacts = new ObservableCollection<ObservableGroupCollection<ContactModel>>();

            //Mock Data
            var a = new ObservableGroupCollection<ContactModel>() { Key = "A" };
            a.Add(new ContactModel());
            a.Add(new ContactModel());
            a.Add(new ContactModel());

            var b = new ObservableGroupCollection<ContactModel>() { Key = "B" };
            b.Add(new ContactModel());
            b.Add(new ContactModel());
            b.Add(new ContactModel());

            var c = new ObservableGroupCollection<ContactModel>() { Key = "C" };
            c.Add(new ContactModel());
            c.Add(new ContactModel());

            var d = new ObservableGroupCollection<ContactModel>() { Key = "D" };
            d.Add(new ContactModel());
            d.Add(new ContactModel());
            d.Add(new ContactModel());

            var e = new ObservableGroupCollection<ContactModel>() { Key = "E" };
            e.Add(new ContactModel());
            e.Add(new ContactModel());
            e.Add(new ContactModel());

            Contacts.Add(a);
            Contacts.Add(b);
            Contacts.Add(c);
            Contacts.Add(d);
            Contacts.Add(e);
            //Mock Data
        }

        public override void ReverseInit(object returnedData)
        {
            if(returnedData is IEnumerable<FilterCategoryModel>)
            {
                var activeFilters = (IEnumerable<FilterCategoryModel>)returnedData;

                if(activeFilters.Any())
                {
                    IsFilterActive = true;
                }
                else
                {
                    IsFilterActive = false;
                }
            }

            base.ReverseInit(returnedData);
        }


        public Command RefreshCommand => new Command(async() =>
        {
            await Task.Delay(3000);

            IsRefreshing = false;
        });


        public Command SelectedCommand => new Command(async(obj) =>
        {
            await CoreMethods.PushPageModel<ContactListDetailPageModel>();
        });


        public Command AddCommand => new Command(async() =>
        {
            var response = await ToastService.ShowActionSheet("Ajouter", new List<Tuple<string, string>>() { new Tuple<string, string>("iconparticulier.png", "Particulier"), new Tuple<string, string>("iconpro.png", "Professionnel") }, "Annuler");

            if (response == "Particulier")
                await CoreMethods.PushPageModel<NewContactPageModel>(false,true,true);
            else if (response == "Professionnel")
                await CoreMethods.PushPageModel<NewContactPageModel>(true, true,true);
        });


        public Command FilterCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModelWithNewNavigation<FilterListPageModel>(Filters);
        });


        public void TabSelectedChanged(int index)
        {
            
        }


        void Search()
        {

        }


        void GetFilterData()
        {
            Filters = new List<FilterCategoryModel>();

            var ft1 = new FilterCategoryModel() { CategoryName = "Statut", IsMultipleSelector = true };
            var ft2 = new FilterCategoryModel() { CategoryName = "Check In", IsMultipleSelector = true };
            var ft3 = new FilterCategoryModel() { CategoryName = "Poids", IsMultipleSelector = true };
            var ft4 = new FilterCategoryModel() { CategoryName = "Tags", IsMultipleSelector = true };
            var ft5 = new FilterCategoryModel() { CategoryName = "Code postal", FilterType = FilterType.SearchCode, IsMultipleSelector = true };

            Filters.Add(ft1);
            Filters.Add(ft2);
            Filters.Add(ft3);
            Filters.Add(ft4);
            Filters.Add(ft5);
        }



        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if(Filters!=null)
                IsFilterActive = Filters.Any((arg) => arg.IsSelected);
        }


    }
}
