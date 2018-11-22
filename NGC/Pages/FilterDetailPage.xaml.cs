using System;
using System.Collections.Generic;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class FilterDetailPage : BasePage
    {

        public FilterDetailPage()
        {
            InitializeComponent();
        }


        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var context = e.Item;

            (BindingContext as FilterDetailPageModel).ItemTappedCommand.Execute(context);

            list.SelectedItem = null;
        }
    }
}
