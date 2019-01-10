using System;
using Xamarin.Forms;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CompanyDynamicEntry
    {
        public string Placeholder { get; set; }

        public string Text { get; set; }

        public Keyboard Keyboard { get; set; }
    }
}
