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
                //ToolbarItems.Remove(btFilter);
                //imgFilter.IsVisible = true;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext is RemindersTabPageModel)
            {
                (BindingContext as RemindersTabPageModel).PropertyChanged -= Handle_PropertyChanged;
                (BindingContext as RemindersTabPageModel).PropertyChanged += Handle_PropertyChanged;
            }
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Reminders")
            {
                listview.ResetSwipe();
            }
        }


        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            (BindingContext as RemindersTabPageModel).TabSelectedChanged(e.NewValue);
        }

        void Terminate(object sender, System.EventArgs e)
        {
            
        }

        void Modify(object sender, System.EventArgs e)
        {

        }

    }
}
