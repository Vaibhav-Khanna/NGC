﻿using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace NGC.Helpers
{
    public partial class DialogLayout : PopupPage
    {
        public DialogLayout(string text)
        {
            InitializeComponent();

            if (String.IsNullOrWhiteSpace(text))
                textLb.IsVisible = false;
            else
                textLb.IsVisible = true;
            
            textLb.Text = text;

            IsAnimationEnabled = false;

            CloseWhenBackgroundIsClicked = false;
        }

		protected override bool OnBackButtonPressed()
		{
            return true;
		}
	}
}
