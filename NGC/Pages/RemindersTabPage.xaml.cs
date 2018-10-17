using System;
using System.Collections.Generic;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class RemindersTabPage : BasePage
    {
        public RemindersTabPage()
        {
            InitializeComponent();
        }

        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            (BindingContext as RemindersTabPageModel).TabSelectedChanged(e.NewValue);
        }
    }
}
