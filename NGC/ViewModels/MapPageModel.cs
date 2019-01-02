using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NGC.Helpers;
using Xamarin.Forms.GoogleMaps;
using NGC.Helpers.MapViews;
using System.Threading.Tasks;
using NGC.Models.DataObjects;
using System.Linq;

namespace NGC.ViewModels
{
    public class MapPageModel : BaseViewModel
    {

        public ObservableCollection<Contact> Pins { get; set; }

        private List<Contact> AllContacts { get; set; }

        public Contact SelectedContact { get; set; }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await GetPinsData();
        }


        async Task GetPinsData()
        {
            var contacts = await StoreManager.ContactStore.GetValidContactsWithLocation();

            AllContacts = contacts?.ToList();
        }


        Tuple<double,double,double,double> CalculateBoundingCoordinates(MapSpan region)
        {
            var center = region.Center;

            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;

            // Adjust for Internation Date Line (+/- 180 degrees longitude)
            if (left < -180) left = 180 + (180 + left);

            if (right > 180) right = (right - 180) - 180;

            return new Tuple<double, double, double, double>(left,top,right,bottom);
        }


        public void FilterVisibleRegion(MapSpan region)
        {
            if (AllContacts != null && AllContacts.Any())
            {
                var region_bounds = CalculateBoundingCoordinates(region);

                var left = region_bounds.Item1;
                var top = region_bounds.Item2;
                var right = region_bounds.Item3;
                var bottom = region_bounds.Item4;

                var points = AllContacts.Where((arg) => arg.Longitude <= right && arg.Longitude >= left && arg.Latitude >= bottom && arg.Latitude <= top).Take(250);

                Pins = new ObservableCollection<Contact>(points);
            }
        }

    }
}
