using System;
using System.Collections.Generic;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            (BindingContext as LoginPageModel).PropertyChanged -= Handle_PropertyChanged;
            (BindingContext as  LoginPageModel).PropertyChanged += Handle_PropertyChanged;
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FocusNext")
            {
                tbemail.Focus();

                tbpassword.Focus();
            }
        }

    }
}
