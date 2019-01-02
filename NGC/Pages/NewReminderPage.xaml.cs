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

            datepicker.MinimumDate = DateTime.Now;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            datepicker.Focus();
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            lbDate.Text = e.NewDate.ToString("dddd, dd MMMM");
        }

    }
}
