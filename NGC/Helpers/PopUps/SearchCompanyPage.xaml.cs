using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace NGC.Helpers.PopUps
{
    public partial class SearchCompanyPage : PopupPage
    {
        public object Result;

        public SearchCompanyPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

           var context = this.BindingContext;
        }

        protected override bool OnBackgroundClicked()
        {
            Result = "";

            return base.OnBackgroundClicked();
        }

        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();

            entry.Focus();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            Result = "";

            await PopupNavigation.Instance.PopAllAsync();
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Result = e.Item;

            list.SelectedItem = null;

            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
