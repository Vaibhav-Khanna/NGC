using System;
using System.Collections.ObjectModel;
using NGC.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace NGC.ViewModels 
{

    public class OpportunityTabPageModel : BaseViewModel
    {

        public ObservableCollection<OpportunityTabModel> ItemSource { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; }

        int _position;
        [PropertyChanged.DoNotNotify]
        public int Position 
        {
            get { return _position; }
            set
            {
                _position = value;

                if(ItemSource!=null && ItemSource.Any())
                {
                    Header = ItemSource[Position].Header;
                    Detail = ItemSource[Position].Detail;
                }

                RaisePropertyChanged();
            }
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            IsLoading = true;

            await GetData();

            IsLoading = false;

            Position = 0;
        }

        async Task GetData()
        {
            var opportunity_data = await StoreManager.OpportunityStore.GetItemsAsync(true, true);

            var items = new ObservableCollection<OpportunityTabModel>();

            if (opportunity_data != null && opportunity_data.Any())
            {
                var grouping_data = opportunity_data.GroupBy((arg) => arg.OpportunityStepName);

                foreach (var item in grouping_data)
                {
                    var group = new OpportunityTabModel() { Header = item.Key };

                    group.ItemSource = new ObservableCollection<OpportunityContacts>();

                    foreach (var x in item)
                    {
                        group.ItemSource.Add(new OpportunityContacts(x));
                    }

                    group.Detail = $"{group.ItemSource.Count} offre - {group.ItemSource.Select((arg) => arg.Amount).Sum()} €";

                    items.Add(group);
                }
            }

          
            ItemSource = items;
        }

        public Command NavigateCommand => new Command(async(obj) =>
        {
            await CoreMethods.PushPageModel<OpportunityDetailPageModel>();
        });


        public Command AddCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModel<NewOpportunityPageModel>();
        });

    }
}
