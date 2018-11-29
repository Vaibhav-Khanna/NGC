using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NGC.Helpers;
using Xamarin.Forms.GoogleMaps;
using NGC.Helpers.MapViews;
using System.Threading.Tasks;

namespace NGC.ViewModels
{
    public class MapPageModel : BaseViewModel
    {

        public ObservableCollection<Pin> Pins { get; set; }


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            var pins = new ObservableCollection<Pin>
            {
                new Pin()
                {
                    Label = "Manuel Llop",
                    Address = "4 rue de messanges, Biarritz",
                    Position = new Position(43.459774, -1.535855),
                    Icon = BitmapDescriptorFactory.FromView(new BindingPinView("5", Color.Red))
                },
                new Pin()
                {
                    Label = "Remy marty",
                    Address = "4 rue de messanges, Biarritz",
                    Position = new Position(43.468894, -1.562825),
                    Icon = BitmapDescriptorFactory.FromView(new BindingPinView("10", Color.Yellow))
                },
                new Pin()
                {
                    Label = "Alexis reverte",
                    Address = "4 rue de messanges, Biarritz",
                    Position = new Position(43.459246, -1.542925),
                    Icon = BitmapDescriptorFactory.FromView(new BindingPinView("2", Color.Green))
                }
            };

            Pins = pins;
        }
       
    }
}
