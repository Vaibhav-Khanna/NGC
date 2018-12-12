using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGC.Helpers;
using NGC.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace NGC.ViewModels
{
    public class NotificationsTabPageModel : BaseViewModel
    {
    
        public ObservableCollection<ObservableGroupCollection<BaseDataObject>> Notifications { get; set; }


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

            await Task.Delay(3000);

            Notifications = new ObservableCollection<ObservableGroupCollection<BaseDataObject>>();

            //Mock Data
            var a = new ObservableGroupCollection<BaseDataObject>() { Key = "Aujourd’hui" };
            a.Add(new BaseDataObject());
            a.Add(new BaseDataObject());
            a.Add(new BaseDataObject());

            var b = new ObservableGroupCollection<BaseDataObject>() { Key = "Hier" };
            b.Add(new BaseDataObject());
            b.Add(new BaseDataObject());
            b.Add(new BaseDataObject());

            var c = new ObservableGroupCollection<BaseDataObject>() { Key = "Avril" };
            c.Add(new BaseDataObject());
            c.Add(new BaseDataObject());

            var d = new ObservableGroupCollection<BaseDataObject>() { Key = "Mars" };
            d.Add(new BaseDataObject());
            d.Add(new BaseDataObject());
            d.Add(new BaseDataObject());

            Notifications.Add(a);
            Notifications.Add(b);
            Notifications.Add(c);
            Notifications.Add(d);

            IsLoading = false;
        }

    }
}
