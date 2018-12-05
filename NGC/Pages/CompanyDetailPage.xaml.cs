using System;
using System.Collections.Generic;

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
            listview.SelectedItem = null;
        }
    }
}
