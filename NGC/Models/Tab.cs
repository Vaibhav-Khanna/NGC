using System;

namespace NGC.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Tab
    { 
        public string Title { get; set; }
    }
}
