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
    }
}
