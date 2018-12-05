using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NGC.Pages;
using FreshMvvm;
using NGC.ViewModels;
using NGC.DataStore.Implementation;
using NGC.DataStore.Abstraction;
using NGC.Helpers;
using NGC.Controls;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NGC
{
    public partial class App : Application
    {

        public static StoreManager storeManager;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDc1NjFAMzEzNjJlMzMyZTMwUGFGdTdZS09iYzB2MC9RWWdUYUp0QktYdG1KQlVjYy9UWGdrRGJ5eFNUUT0=");

            InitializeComponent();

            BaseViewModel.Initialize();

            storeManager = FreshIOC.Container.Resolve<IStoreManager>() as StoreManager;

            Init();

            MainPage = new ContentPage() {  BindingContext = new BaseViewModel() { IsLoading = true } };
        }

        async void Init()
        {
            if (storeManager == null)
                return;

            if (!storeManager.IsInitialized)
                await storeManager.InitializeAsync();

            //verify Token 
            await storeManager.VerifyTokenAsync();


            if (StoreManager.MobileService.CurrentUser == null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();

                    var container = new FreshNavigationContainer(page) { BarTextColor = Color.Black };

                    MainPage = container;
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread( () =>
                {
                    var tabbedPage = TabNavigator.GenerateTabPage();

                    MainPage = tabbedPage;
                });

               await storeManager.SyncAllAsync(true);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
