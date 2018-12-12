using System;
using System.Collections.Generic;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class NewContactPage : BasePage
    {
        public NewContactPage()
        {
            InitializeComponent();
        }

        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            (BindingContext as NewContactPageModel).TabSelectedChanged(e.NewValue);
        }

        void RatingClicked(object sender, System.EventArgs e)
        {
           var senderButton = sender as Button;

            bt1.BackgroundColor = Color.White;
            bt2.BackgroundColor = Color.White;
            bt3.BackgroundColor = Color.White;
            bt4.BackgroundColor = Color.White;
            bt5.BackgroundColor = Color.White;

            senderButton.BackgroundColor = Color.FromHex("#f8f8f8");
        }

    }
}
