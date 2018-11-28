using System;
using System.Collections.ObjectModel;
using NGC.Models;

namespace NGC.ViewModels
{
    public class OpportunityDetailPageModel : BaseViewModel
    {
       
        public ObservableCollection<Tab> Tabs { get; set; }
        public int SelectedIndex { get; set; }

        public string Header { get; set; }
        public string Detail { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            Tabs = new ObservableCollection<Tab>() { new Tab(){ Title = "Lead"}, new Tab(){ Title="Contact"}, new Tab(){ Title = "Client" } };

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            SelectedIndex = 0;
        }

    }
}
