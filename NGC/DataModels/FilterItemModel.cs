using System;
using System.ComponentModel;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class FilterItemModel
    {

        /// <summary>
        /// Gets or sets a value indicating whether a shield should be displayed
        /// on this control to indicate that process elevation is required.
        /// </summary>       
        public string PropertyName { get; set; }

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
    }
}
