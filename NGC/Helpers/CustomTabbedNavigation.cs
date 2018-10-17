using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Page = Xamarin.Forms.Page;

namespace NGC.Helpers
{

    public class CustomTabbedNavigation : Xamarin.Forms.TabbedPage, IFreshNavigationService
    {

        List<Page> _tabs = new List<Page>();
        public IEnumerable<Page> TabbedPages { get { return _tabs; } }

        public CustomTabbedNavigation() : this(FreshMvvm.Constants.DefaultNavigationServiceName)
        {
            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.RuntimePlatform == Device.Android)
            {
                this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
                this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor((Color)Application.Current.Resources["BottomTabText"]);
                this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor((Color)Application.Current.Resources["BottomTabTextTint"]);
                this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetElevation(10);               
                this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);             
            }
        }

        public CustomTabbedNavigation(string navigationServiceName)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationServiceName = navigationServiceName;
            RegisterNavigation();
        }

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public virtual Page AddTab<T>(string title, string icon, object data = null) where T : FreshBasePageModel
        {
            var page = FreshPageModelResolver.ResolvePageModel<T>(data);
            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            _tabs.Add(page);

            var navigationContainer = CreateContainerPageSafe(page);
            navigationContainer.Title = title;

            if (!string.IsNullOrWhiteSpace(icon))
                navigationContainer.Icon = icon;

            Children.Add(navigationContainer);
            return navigationContainer;
        }

        internal Page CreateContainerPageSafe(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is Xamarin.Forms.TabbedPage)
                return page;

            return CreateContainerPage(page);
        }

        protected virtual Page CreateContainerPage(Page page)
        {
            var curr = new NavigationPage(page){ BarTextColor = Color.White };

            curr.On<Xamarin.Forms.PlatformConfiguration.iOS>().HideNavigationBarSeparator();

            return curr;
        }

        public System.Threading.Tasks.Task PushPage(Xamarin.Forms.Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
                return this.Navigation.PushModalAsync(CreateContainerPageSafe(page), Device.RuntimePlatform == Device.iOS);

            return this.CurrentPage.Navigation.PushAsync(page,animate);
        }

        public System.Threading.Tasks.Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                return this.CurrentPage.Navigation.PopModalAsync(Device.RuntimePlatform == Device.iOS);

            return this.CurrentPage.Navigation.PopAsync(animate);
        }

        public Task PopToRoot(bool animate = true)
        {
            return this.CurrentPage.Navigation.PopToRootAsync(animate);
        }

        public string NavigationServiceName { get; private set; }

        public void NotifyChildrenPageWasPopped()
        {
            foreach (var page in this.Children)
            {
                if (page is NavigationPage)
                    ((NavigationPage)page).NotifyAllChildrenPopped();
            }
        }

        bool isWaitingForBack { get; set; }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            var page = _tabs.FindIndex(o => o.GetModel().GetType().FullName == typeof(T).FullName);

            if (page > -1)
            {
                CurrentPage = this.Children[page];
                var topOfStack = CurrentPage.Navigation.NavigationStack.LastOrDefault();
                if (topOfStack != null)
                    return Task.FromResult(topOfStack.GetModel());

            }
            return null;
        }

    }

}
