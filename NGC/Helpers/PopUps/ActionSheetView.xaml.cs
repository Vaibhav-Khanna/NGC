using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using NGC.Effects;
using Rg.Plugins.Popup.Services;

namespace NGC.Helpers.PopUps
{
    public partial class ActionSheetView : PopupPage
    {
        public string Result { get; set; } = null;

        public ActionSheetView(string Title, IEnumerable<Tuple<string, string>> Buttons, string Cancel)
        {
            this.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
           
            InitializeComponent();

            lbHeader.Text = Title;
            lbCancel.Text = Cancel;

            foreach (var item in Buttons)
            {
                container.Children.Add(GenerateButton(item.Item1,item.Item2));
            }
        }

        Button GenerateButton(string image, string text)
        {
            var button = new Button()
            {
                Image = image,
                Text = text,
                Padding = new Thickness(10, 0, 0, 0),
                Margin = new Thickness(7, 0, 7, 0),
                HeightRequest = 60,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.FromHex("#4a4a4a"),
                FontFamily = "SFUIDisplay-Semibold",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = 0
            };

            button.Effects.Add(new ButtonTextAlignmentEffect());

            button.Clicked += Handle_Clicked;

            return button;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var bt = sender as Button;

            Result = bt.Text;

            await PopupNavigation.Instance.PopAllAsync();
        }

        protected override bool OnBackgroundClicked()
        {
            Result = lbCancel.Text;

            return base.OnBackgroundClicked();
        }
    }
}
