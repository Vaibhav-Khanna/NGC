using System;
using System.Collections.Generic;
using System.Linq;
using NGC.ViewModels;
using Xamarin.Forms;

namespace NGC.Pages
{
    public partial class OpportunityDetailPage : BasePage
    {

        Color grey = Color.FromHex("#9b9b9b");
        Color red = Color.FromHex("ff6565");

        public OpportunityDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if(BindingContext != null)
            {
                InflateTabs();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            tab.Container.Padding = new Thickness((this.Width / 2) - 10, 15, (this.Width / 2) - 10, 15);

            InflateTabs();

            tab.SelectedIndex = 0;
        }

        void InflateTabs()
        {
            tabContainer.Children.Clear();

            for (int i = 0; i < (BindingContext as OpportunityDetailPageModel).Tabs.Count; i++)
            {
                var box = new BoxView() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 10, BackgroundColor = grey };

                if (i == 0)
                    box.BackgroundColor = red;

                tabContainer.Children.Add(box);
            }
        }


        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            foreach (var item in tabContainer.Children)
            {
                item.BackgroundColor = grey;
            }

            if (tabContainer.Children.Any())
                tabContainer.Children[e.NewValue].BackgroundColor = red;
        }

    }
}
