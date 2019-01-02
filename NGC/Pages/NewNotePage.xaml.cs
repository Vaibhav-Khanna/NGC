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

            datepicker.MinimumDate = DateTime.Now.Date;

            lbDate.Text = (string)new Converter.DateToStringConverter().Convert(DateTime.Now.Date, typeof(string), null, System.Globalization.CultureInfo.CurrentCulture);

            datepicker.DateSelected += Datepicker_DateSelected;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            datepicker.Focus();
        }

        void Datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            lbDate.Text = (string) new Converter.DateToStringConverter().Convert(e.NewDate, typeof(string), null, System.Globalization.CultureInfo.CurrentCulture);
        }

    }
}
