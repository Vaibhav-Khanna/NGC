using System;
using System.Collections.Generic;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class FilterListPage : BasePage
    {
        public FilterListPage()
        {
            InitializeComponent();
        }


        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (BindingContext as FilterListPageModel).ItemSelectedCommand.Execute(e.Item);

            list.SelectedItem = null;
        }

    }
}
