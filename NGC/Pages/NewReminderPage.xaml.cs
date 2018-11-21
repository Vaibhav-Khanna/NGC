using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class NewReminderPage : BasePage
    {
        public NewReminderPage()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            datepicker.Focus();
        }
    }
}
