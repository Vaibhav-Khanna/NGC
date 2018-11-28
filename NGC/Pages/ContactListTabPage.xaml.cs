using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using NGC.ViewModels;

namespace NGC.Pages
{
    public partial class ContactListTabPage : BasePage
    {
        public ContactListTabPage()
        {
            InitializeComponent();

            if(Device.RuntimePlatform == Device.Android)
            {
                ToolbarItems.Remove(btFilter);
                imgFilter.IsVisible = true;
            }
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (BindingContext as ContactListTabPageModel).SelectedCommand.Execute(e.Item);

            listview.SelectedItem = null;
        }

        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            (BindingContext as ContactListTabPageModel).TabSelectedChanged(e.NewValue);
        }
    }
}
