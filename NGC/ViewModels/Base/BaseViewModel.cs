using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using FreshMvvm;
using NGC.Models;
using NGC.Services;

namespace NGC.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class BaseViewModel : FreshBasePageModel
    {

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

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
