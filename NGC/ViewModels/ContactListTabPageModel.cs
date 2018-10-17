using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGC.Helpers;
using NGC.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace NGC.ViewModels
{
    public class ContactListTabPageModel : BaseViewModel
    {
     
        public ObservableCollection<ObservableGroupCollection<Item>> Contacts { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            Contacts = new ObservableCollection<ObservableGroupCollection<Item>>();

            //Mock Data
            var a = new ObservableGroupCollection<Item>() { Key = "A" };
            a.Add(new Item());
            a.Add(new Item());
            a.Add(new Item());

            var b = new ObservableGroupCollection<Item>() { Key = "B" };
            b.Add(new Item());
            b.Add(new Item());
            b.Add(new Item());

            var c = new ObservableGroupCollection<Item>() { Key = "C" };
            c.Add(new Item());
            c.Add(new Item());

            var d = new ObservableGroupCollection<Item>() { Key = "D" };
            d.Add(new Item());
            d.Add(new Item());
            d.Add(new Item());

            var e = new ObservableGroupCollection<Item>() { Key = "E" };
            e.Add(new Item());
            e.Add(new Item());
            e.Add(new Item());

            Contacts.Add(a);
            Contacts.Add(b);
            Contacts.Add(c);
            Contacts.Add(d);
            Contacts.Add(e);
            //Mock Data
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


        public Command AddCommand => new Command(() =>
        {

        });

        public Command FilterCommand => new Command(() =>
        {

        });

        public void TabSelectedChanged(int index)
        {
            
        }

    }
}
