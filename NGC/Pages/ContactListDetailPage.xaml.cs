﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class ContactListDetailPage : BasePage
    {
        public ContactListDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            btAdd.IsVisible = true;
        }
    }
}
