using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if(BindingContext is OpportunityTabPageModel)
            {
                ((OpportunityTabPageModel)BindingContext).PropertyChanged += Handle_PropertyChanged;
            }
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "ItemSource")
            {
                InflateTabs((BindingContext as OpportunityTabPageModel).ItemSource?.Count);
            }
        }

        void InflateTabs(int? tabCount)
        {
            if (tabCount == null)
                return;

            container.Children.Clear();

            for (int i = 0; i < tabCount; i++)
            {
                var box = new BoxView() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 10, BackgroundColor = grey };

                if (i == 0)
                    box.BackgroundColor = red;

                container.Children.Add(box);
            }
        }

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            foreach (var item in container.Children)
            {
                item.BackgroundColor = grey;
            }

            if (container.Children.Any())
                container.Children[e.NewValue].BackgroundColor = red;
        }


        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (this.BindingContext as OpportunityTabPageModel).NavigateCommand.Execute(new Tuple<OpportunityTabModel, OpportunityContacts>((sender as ListView).BindingContext as OpportunityTabModel,e.Item as OpportunityContacts));
         
            (sender as ListView).SelectedItem = null;
        }

    }
}
