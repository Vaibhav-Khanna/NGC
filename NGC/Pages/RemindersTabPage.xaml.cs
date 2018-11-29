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

            if (Device.RuntimePlatform == Device.Android)
            {
                ToolbarItems.Remove(btFilter);
                imgFilter.IsVisible = true;
            }
        }

        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            (BindingContext as RemindersTabPageModel).TabSelectedChanged(e.NewValue);
        }
    }
}
