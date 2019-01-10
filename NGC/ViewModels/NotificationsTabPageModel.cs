using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGC.Helpers;
using NGC.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NGC.DataModels;

namespace NGC.ViewModels
{
    public class NotificationsTabPageModel : BaseViewModel
    {
    
        public ObservableCollection<ObservableGroupCollection<NotificationModel>> Notifications { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

           
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            GetData();
        }


        async void GetData()
        {
            IsLoading = true;

            var noti_data = await StoreManager.NotificationStore.GetNotificationsByUserId(Settings.UserId);

            Notifications = new ObservableCollection<ObservableGroupCollection<NotificationModel>>();

           
            var a = new ObservableGroupCollection<NotificationModel>() { Key = "Aujourd’hui" };

            foreach (var item in noti_data.Where((arg) => arg.DatabaseInsertAt.Date == DateTime.Now.Date))
            {
                a.Add(new NotificationModel(item));
            }


            var b = new ObservableGroupCollection<NotificationModel>() { Key = "Hier" };

            foreach (var item in noti_data.Where((arg) => arg.DatabaseInsertAt.Date == DateTime.Now.Date.AddDays(-1).Date) )
            {
                b.Add(new NotificationModel(item));
            }

            if(a.Any())
            Notifications.Add(a);

            if(b.Any())
            Notifications.Add(b);

           
            var leftOverNotifications = noti_data.Where((arg) => arg.DatabaseInsertAt.Date != DateTime.Now.Date.AddDays(-1).Date && arg.DatabaseInsertAt.Date != DateTime.Now.Date);


            var grouped = leftOverNotifications.OrderByDescending((arg) => arg.DatabaseInsertAt.Month).GroupBy((arg) => arg.DatabaseInsertAtMonth);

            foreach (var item in grouped)
            {
                var c = new ObservableGroupCollection<NotificationModel>() { Key = item.Key };

                foreach (var x in item)
                {
                    c.Add(new NotificationModel(x));
                }

                Notifications.Add(c);
            }


            IsEmpty = !Notifications.Any();

            IsLoading = false;
        }

    }
}
