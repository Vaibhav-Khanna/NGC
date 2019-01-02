using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class FilterCategoryModel
    {

        public FilterType FilterType { get; set; }

        public string CategoryName { get; set; }

        public ObservableCollection<FilterItemModel> CategoryItemSource { get; set; }

        public List<FilterItemModel> SelectedFilters { get; set; }

        public bool IsSelected { get { return SelectedFilters != null && SelectedFilters.Any() ? true : false; } }

        public bool IsMultipleSelector { get; set; }

        public int ActiveFilters { get { return IsSelected ? SelectedFilters.Count : 0; } }

        public string FilterText { get{ return ActiveFilters > 1 ? $"{ActiveFilters} actifs" : $"{ActiveFilters} actif"; } }


        public FilterCategoryModel()
        {
            CategoryItemSource = new ObservableCollection<FilterItemModel>();
        }


        public void AddFilterCategory(string Category)
        {
            CategoryItemSource.Add(new FilterItemModel() { PropertyName = Category, });
        }


        public void AddFilterCategory(IEnumerable<string> Categories)
        {
            if (Categories != null)
                foreach (var item in Categories)
                {
                    CategoryItemSource.Add(new FilterItemModel() { PropertyName = item });
                }
        }

    }

    public enum FilterType
    {
        ListSelector, SearchCode
    }
}
