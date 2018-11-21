using System;


namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class FilterItemModel
    {
        public string PropertyName { get; set; }

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
    }
}
