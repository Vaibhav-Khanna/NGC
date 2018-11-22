using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class NewNotePage : BasePage
    {
        public NewNotePage()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            datepicker.Focus();
        }

    }
}
