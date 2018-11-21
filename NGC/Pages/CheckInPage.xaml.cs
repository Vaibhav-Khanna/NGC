using System;
using System.Collections.Generic;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class CheckInPage : BasePage
    {
        public CheckInPage()
        {
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (BindingContext as CheckInPageModel).ItemSelected.Execute(e.Item);

            list.SelectedItem = null;
        }
    }
}
