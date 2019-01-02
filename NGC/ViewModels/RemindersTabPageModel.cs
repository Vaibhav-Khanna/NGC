using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using NGC.DataModels;
using System.Linq;
using System.Threading.Tasks;
using NGC.Resources;

namespace NGC.ViewModels
{
    public class RemindersTabPageModel : BaseViewModel
    {

        private List<RemindersModel> AllReminders { get; set; }

        public ObservableCollection<RemindersModel> Reminders { get; set; }

        public bool IsFilterActive { get; set; }

        List<FilterCategoryModel> Filters { get; set; }

        private int TabIndex = 0;

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

        public Command ModifyItemCommand => new Command(async(obj) =>
        {
            if (obj is RemindersModel)
            {
                var reminder = obj as RemindersModel;

                if (!reminder.Reminder.DoneAt.HasValue)
                {
                    await CoreMethods.PushPageModel<NewReminderPageModel>(reminder, true);
                }

                await FreshData();
            }
        });

        public Command EndItemCommand => new Command(async(obj) =>
        {
            if (obj is RemindersModel)
            {
                var reminder = obj as RemindersModel;

                if (!reminder.Reminder.DoneAt.HasValue)
                {
                    reminder.Reminder.DoneAt = DateTime.Now;

                    ToastService.ShowLoading();

                    var updated = await StoreManager.NoteStore.UpdateAsync(reminder.Reminder);

                    if (updated)
                    {
                        await FreshData();
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.Error, AppResources.Ok);
                    }

                    ToastService.HideLoading();
                }
            }
        });

        void Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                TabSelectedChanged(TabIndex);

                var temp_Filtered_reminders = Reminders.ToList();

                Reminders = new ObservableCollection<RemindersModel>(temp_Filtered_reminders.Where((arg) => arg.Name.ToLower().Contains(SearchText.ToLower()) || arg.Content.ToLower().Contains(SearchText.ToLower())));
            }
            else
            {
                TabSelectedChanged(TabIndex);
            }
        }

        public void TabSelectedChanged(int index)
        {
            if (TabIndex != index)
            {
                _searchtext = null;
                RaisePropertyChanged("SearchText");
            }

            TabIndex = index;

            if(!IsLoading)
            if (AllReminders != null && AllReminders.Any())
            {
                if (index == 0) // show only active reminders
                {
                    Reminders = new ObservableCollection<RemindersModel>(AllReminders.Where((arg) => !arg.Reminder.DoneAt.HasValue));
                }
                else // Show terminated reminders
                {
                    Reminders = new ObservableCollection<RemindersModel>(AllReminders.Where((arg) => arg.Reminder.DoneAt.HasValue));
                }
            }
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

            await FreshData();

            IsLoading = false;

            TabSelectedChanged(TabIndex);
        }


        public async Task FreshData()
        {
            var reminders_data = await StoreManager.NoteStore.GetAllReminders();

            AllReminders = new List<RemindersModel>();

            Reminders = new ObservableCollection<RemindersModel>();

            if (reminders_data != null && reminders_data.Any())
            {
                foreach (var item in reminders_data)
                {
                    AllReminders.Add(new RemindersModel(item));
                }
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Filters != null)
                IsFilterActive = Filters.Any((arg) => arg.IsSelected);

            GetData();
        }

    }
}
