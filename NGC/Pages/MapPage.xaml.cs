using System;
using System.Collections.Generic;
using NGC.Helpers;
using NGC.Helpers.MapViews;
using NGC.ViewModels;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace NGC.Pages
{
    public partial class MapPage : BasePage
    {

        Position currentLocation;
        Pin currentPin;
        MapPageModel context;

        public MapPage()
        {
            InitializeComponent();

            map.PinClicked += Map_PinClicked;
            map.MapClicked += Map_MapClicked;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is MapPageModel)
            {
                context = ((MapPageModel)BindingContext);

                (BindingContext as MapPageModel).PropertyChanged -= Handle_PropertyChanged;
                (BindingContext as MapPageModel).PropertyChanged += Handle_PropertyChanged;
            }
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Pins")
            {
                PlotPins();
            }
        }

        void Handle_MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            MoveToCurrentLocation();
        }

        void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            HidePopUp();
        }

        void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            ShowPopup();
        }

        async void ShowPopup()
        {
            if(popover.TranslationY!=0)
            await popover.TranslateTo(0, 0, 250, Easing.SpringIn);
        }

        async void HidePopUp()
        {
            if (popover.TranslationY == 0)
                await popover.TranslateTo(0, 270, 250, Easing.Linear);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            HidePopUp();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MoveToCurrentLocation();
        }

        async void MoveToCurrentLocation()
        {
            currentLocation = await Location.GetCurrentLocation(true);

            if(currentLocation!=null)
            {
                if (currentPin != null && map.Pins.Contains(currentPin))
                    map.Pins.Remove(currentPin);

                map.Pins.Add(currentPin = new Pin(){ Label = "Me", Position = new Xamarin.Forms.GoogleMaps.Position(currentLocation.Latitude,currentLocation.Longitude), Icon = BitmapDescriptorFactory.FromView(new BindingPinView("2", Color.Green)) });

                await map.MoveCamera(CameraUpdateFactory.NewPositionZoom(new Xamarin.Forms.GoogleMaps.Position(currentLocation.Latitude, currentLocation.Longitude), 12d));
            }
        }


        void PlotPins()
        {
            map.Pins.Clear();

            if (currentPin != null)
                map.Pins.Add(currentPin);

            foreach (var item in context.Pins)
            {
                map.Pins.Add(item);
            }
        }

    }
}
