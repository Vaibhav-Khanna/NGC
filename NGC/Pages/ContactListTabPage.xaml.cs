using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using NGC.ViewModels;
using NGC.DataModels;
using System.Linq;

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

        void Handle_ItemTapped_1(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (BindingContext as ContactListTabPageModel).CompanySelectedCommand.Execute(e.Item);

            listview_Company.SelectedItem = null;
        }

        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            if (e.NewValue == 0)
            {
                lt.IsVisible = true;
                ltCompany.IsVisible = false;
            }
            else
            {
                lt.IsVisible = false;
                ltCompany.IsVisible = true;
            }

             (BindingContext as ContactListTabPageModel).TabSelectedChanged(e.NewValue);
        }


        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            if ((e.Item as ContactModel)?.Contact == (this.BindingContext as ContactListTabPageModel).Contacts?.Last()?.LastOrDefault()?.Contact)
            {
                (BindingContext as ContactListTabPageModel).LoadMoreCommand.Execute(null);
            }
        }

    }
}
