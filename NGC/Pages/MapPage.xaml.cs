using System;
using System.Collections.Generic;
using NGC.Helpers;
using NGC.Helpers.MapViews;
using NGC.Models.DataObjects;
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
            if (e.PropertyName == "Pins")
            {
                PlotPins();
            }
            else if (e.PropertyName == "AllContacts")
            {
                context.FilterVisibleRegion(map.VisibleRegion);
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
            if (e.Pin.Type == PinType.Generic)
            {
                ShowPopup();

                context.SelectedContact = e?.Pin?.BindingContext as Contact;

                if (context.SelectedContact != null)
                    bt.BackgroundColor = GetCheckinColor(context.SelectedContact);
            }
            else 
            {
                HidePopUp(); 
            }
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

            if (currentLocation != null)
            {
                if (currentPin != null && map.Pins.Contains(currentPin))
                    map.Pins.Remove(currentPin);

                map.Pins.Add(currentPin = new Pin() { Label = "Me", Position = new Xamarin.Forms.GoogleMaps.Position(currentLocation.Latitude, currentLocation.Longitude), Type = PinType.Place });

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
                var pin = new Pin()
                { 
                    Label = item.Name,
                    Address = item.Address,
                    Position = new Xamarin.Forms.GoogleMaps.Position(item.Latitude, item.Longitude),
                    Icon = BitmapDescriptorFactory.FromView(new BindingPinView(item.Weight.ToString(), GetCheckinColor(item) )),
                    BindingContext = item,
                    Type = PinType.Generic
                };

                map.Pins.Add(pin);
            }
        }

        void Handle_CameraIdled(object sender, Xamarin.Forms.GoogleMaps.CameraIdledEventArgs e)
        {
            context.FilterVisibleRegion(map.VisibleRegion);
        }

        Color GetCheckinColor(Contact contact)
        {
            if (!contact.AllowCheckin)
                return Color.FromHex("656565");
            else
            {
                var diff = DateTime.Now.Subtract(contact.LastCheckinAt.DateTime).Days;

                if (diff >= 0 && diff <= contact.FirstCheckinDuration)
                    return Color.FromHex("7ed321");  //green
                else if (diff > contact.FirstCheckinDuration && diff <= contact.SecondCheckinDuration)
                    return Color.FromHex("f5a623"); //orange
                else if (diff > contact.SecondCheckinDuration)
                    return Color.FromHex("ec1414"); //red   
                    else return Color.FromHex("ec1414"); //red        
            }
        }
    }
}
