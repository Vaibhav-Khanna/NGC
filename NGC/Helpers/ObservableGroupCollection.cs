using System;
using System.Collections.ObjectModel;
using NGC.Models;

namespace NGC.Helpers
{
    public class ObservableGroupCollection<T> : ObservableCollection<T>
    {
        string key;
        public string Key { get { return key?.ToUpper(); } set { key = value; } }

        string detail;
        public string Detail { get { return detail; } set { detail = value; } }

    }
}
