using System;
using System.Collections.ObjectModel;
using NGC.Models;

namespace NGC.Helpers
{
    public class ObservableGroupCollection<T> : ObservableCollection<T>
    {
        public string Key { get; set; }
    }
}
