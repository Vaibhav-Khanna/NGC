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

namespace NGC.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class BaseViewModel : FreshBasePageModel
    {

        public BaseViewModel()
        {
            ToastService = new ToastService();
        }

        public IDataStore<BaseDataObject> DataStore => DependencyService.Get<IDataStore<BaseDataObject>>() ?? new MockDataStore();

        protected IToastService ToastService { get; set; }

        public bool IsRefreshing { get; set; }

        public bool IsBusy
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

    }
}
