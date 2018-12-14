using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using NGC.DataModels;
using System.Linq;

namespace NGC.ViewModels
{
    public class RemindersTabPageModel : BaseViewModel
    {
        public ObservableCollection<RemindersModel> Reminders { get; set; }

        public bool IsFilterActive { get; set; }

        List<FilterCategoryModel> Filters { get; set; }

        string _searchtext;
        [PropertyChanged.DoNotNotify]
        public string SearchText { get { return _searchtext; } set { _searchtext = value; Search(); RaisePropertyChanged(); } }


        public override void Init(object initData)
        {
            base.Init(initData);

            GetFilterData();
        }


        public Command AddCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModel<NewReminderPageModel>(null,true);
        });

        public Command FilterCommand => new Command(async() =>
        {

            await CoreMethods.PushPageModelWithNewNavigation<FilterListPageModel>(Filters);

        });

        public Command ItemSelectedCommand => new Command((obj) =>
        {

        });

        public Command ModifyItemCommand => new Command((obj) =>
        {

        });

        public Command EndItemCommand => new Command((obj) =>
        {

        });

        void Search()
        {

        }

        public void TabSelectedChanged(int index)
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

        async void GetData()
        {
            IsLoading = true;

            var reminders_data = await StoreManager.NoteStore.GetAllReminders();

            Reminders = new ObservableCollection<RemindersModel>();

            if (reminders_data != null && reminders_data.Any())
            {
                foreach (var item in reminders_data)
                {
                    Reminders.Add(new RemindersModel(item));
                }
            }

            IsLoading = false;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Filters != null)
                IsFilterActive = Filters.Any((arg) => arg.IsSelected);

            if (Reminders == null)
                GetData();
        }

    }
}
