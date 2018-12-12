using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using FreshMvvm;
using NGC.Models;
using NGC.Services;
using NGC.Helpers.PopUps;
using NGC.Helpers;
using NGC.DataStore.Abstraction;
using NGC.DataStore.Implementation;
using NGC.DataStore.Abstraction.Stores;
using NGC.Resources;
using NGC.DataStore.Implementation.Stores;

namespace NGC.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class BaseViewModel : FreshBasePageModel
    {

        public BaseViewModel()
        {
            ToastService = new ToastService();
        }

        protected IStoreManager StoreManager = FreshIOC.Container.Resolve<IStoreManager>();

        protected IToastService ToastService { get; set; }

        public bool IsRefreshing { get; set; }

        public bool IsLoading { get; set; }

        public string LoadingText { get; set; }

        public bool IsBusy
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public static void Initialize()
        {
            FreshIOC.Container.Register<IStoreManager, StoreManager>();

            FreshIOC.Container.Register<IUserStore, UserStore>();
            FreshIOC.Container.Register<IOpportunityStore, OpportunityStore>();
            FreshIOC.Container.Register<ICompanyStore, CompanyStore>();
            FreshIOC.Container.Register<IContactStore, ContactStore>();
            FreshIOC.Container.Register<INoteStore, NoteStore>();
            FreshIOC.Container.Register<ICheckinTypeStore, CheckinTypeStore>();
            FreshIOC.Container.Register<ICheckinStore, CheckinStore>();
            FreshIOC.Container.Register<ITagStore, TagStore>();
            FreshIOC.Container.Register<IContactCustomFieldSourceEntryStore, ContactCustomFieldSourceEntryStore>();
            FreshIOC.Container.Register<IContactCustomFieldSourceStore, ContactCustomFieldSourceStore>();
            FreshIOC.Container.Register<IContactCustomFieldStore, ContactCustomFieldStore>();
            FreshIOC.Container.Register<IContactSequenceStore, ContactSequenceStore>();
            FreshIOC.Container.Register<IUserStore, UserStore>();
        }

    }
}
