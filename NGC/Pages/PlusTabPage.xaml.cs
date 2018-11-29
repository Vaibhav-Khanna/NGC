using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class PlusTabPage : BasePage
    {
        public PlusTabPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lblogout.HorizontalOptions = LayoutOptions.Center;
            lblogout.HorizontalTextAlignment = TextAlignment.Center;
        }
    }
}
