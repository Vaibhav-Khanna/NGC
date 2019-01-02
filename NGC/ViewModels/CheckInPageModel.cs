using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using NGC.Helpers;
using NGC.Models.DataObjects;
using NGC.DataModels;

namespace NGC.ViewModels
{
    public class CheckInPageModel : BaseViewModel
    {

        public ObservableCollection<CheckinType> Items { get; set; }

        public ContactModel Contact { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is ContactModel)
            {
                Contact = ((ContactModel)initData);

                GetData();
            }
        }

        async void GetData()
        {
            var data = await StoreManager.CheckinTypeStore.GetItemsAsync();

            Items = new ObservableCollection<CheckinType>();

            if (data != null && data.Any())
            {
               var types = data.DistinctBy((arg) => arg.Name);

                foreach (var item in types)
                {
                    Items.Add(item);
                }
            }
        }

        public Command BackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(true);
        });


        public Command ItemSelected => new Command(async(obj) =>
        {
            if (obj != null)
            {
                ToastService.ShowLoading();

                var checkin = new Checkin() { CheckinTypeId = (obj as CheckinType).Id, UserId = Settings.UserId, ContactId = Contact.Contact.Id, DoneAt = DateTime.Now };

                var isAdded = await StoreManager.CheckinStore.InsertAsync(checkin);

                ToastService.HideLoading();

                if(isAdded)
                await CoreMethods.PopPageModel(obj, true);
            }
        });

    }
}
