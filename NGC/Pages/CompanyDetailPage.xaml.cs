using System;
using System.Collections.Generic;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class CompanyDetailPage : BasePage
    {
        public CompanyDetailPage()
        {
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (BindingContext as CompanyDetailPageModel).ContactDetailCommand.Execute(e.Item);

            listview.SelectedItem = null;
        }
    }
}
