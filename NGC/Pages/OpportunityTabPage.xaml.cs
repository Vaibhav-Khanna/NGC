using System;
using System.Collections.Generic;
using NGC.Models;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class OpportunityTabPage : BasePage
    {
        Color grey = Color.FromHex("#9b9b9b");
        Color red = Color.FromHex("ff6565");

        public OpportunityTabPage()
        {
            InitializeComponent();
        }

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            bx1.BackgroundColor = grey;
            bx2.BackgroundColor = grey;
            bx3.BackgroundColor = grey;
            bx4.BackgroundColor = grey;

            if (e.NewValue == 0)
                bx1.BackgroundColor = red;
            if (e.NewValue == 1)
                bx2.BackgroundColor = red;
            if (e.NewValue == 2)
                bx3.BackgroundColor = red;
            if (e.NewValue == 3)
                bx4.BackgroundColor = red;

        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (this.BindingContext as OpportunityTabPageModel).NavigateCommand.Execute(new Tuple<OpportunityTabModel, OpportunityContacts>((sender as ListView).BindingContext as OpportunityTabModel,e.Item as OpportunityContacts));
         
            (sender as ListView).SelectedItem = null;
        }
    }
}
