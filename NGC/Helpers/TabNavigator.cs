using System;
using Xamarin.Forms;
using NGC.ViewModels;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Application = Xamarin.Forms.Application;
using Page = Xamarin.Forms.Page;
using NGC.Resources;

namespace NGC.Helpers
{
    public class TabNavigator
    {
        public static NavigationPage currentPage;

        public static Page GenerateTabPage()
        {
            var bottomBarPage = new CustomTabbedNavigation() { BarBackgroundColor = (Color)Application.Current.Resources["BottomTabBackground"] };
             

            bottomBarPage.AddTab<ContactListTabPageModel>(AppResources.Contacts, "1.png");
            bottomBarPage.AddTab<RemindersTabPageModel>(AppResources.Reminders, "2.png");
            bottomBarPage.AddTab<OpportunityTabPageModel>(AppResources.Opportunities, "3.png");
            bottomBarPage.AddTab<NotificationsTabPageModel>(AppResources.Notifications, "4.png");
            bottomBarPage.AddTab<PlusTabPageModel>(AppResources.More, "5.png");


            currentPage = new NavigationPage(bottomBarPage){ BarTextColor = Color.White };

            currentPage.On<Xamarin.Forms.PlatformConfiguration.iOS>().HideNavigationBarSeparator();

            return currentPage;
        }
    }
}
