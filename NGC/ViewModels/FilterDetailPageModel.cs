using System;
using System.Collections.ObjectModel;
using NGC.DataModels;
using Xamarin.Forms;
using NGC.Resources;
using System.Collections.Generic;
using System.Linq;

namespace NGC.ViewModels
{
    public class FilterDetailPageModel : BaseViewModel
    {

        public ObservableCollection<FilterItemModel> Filters { get; set; }

        private FilterCategoryModel CategoryModel { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData is FilterCategoryModel)
            {
                CategoryModel = (FilterCategoryModel)initData;

                Title = $"{AppResources.Filter} par {CategoryModel.CategoryName.ToLower()}";

                Filters = new ObservableCollection<FilterItemModel>(CategoryModel.CategoryItemSource);
            }
        }


        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);

            foreach (var item in CategoryModel.CategoryItemSource)
            {
                item.IsSelected = item.IsActive;
            }
        }


        public Command ItemTappedCommand => new Command((obj) =>
        {
            var item = obj as FilterItemModel;

            item.IsSelected = !item.IsSelected;

            if (item.PropertyName == "Tous" && item.IsSelected)
            {
               var tobeChanged = Filters.Where((arg) => arg.PropertyName != "Tous");

                foreach (var i in tobeChanged)
                {
                    i.IsSelected = false;
                }
            }

            if (item.PropertyName != "Tous" && item.IsSelected)
            {
                var toChange = Filters.Where((arg) => arg.PropertyName == "Tous");

                if (toChange != null && toChange.Any())
                {
                    toChange.First().IsSelected = false;
                }
            }
        });


        public Command ValidateCommand => new Command(async() =>
        {                  
            CategoryModel.SelectedFilters = new List<FilterItemModel>(Filters.Where((arg) => arg.IsSelected));

            foreach (var item in CategoryModel.CategoryItemSource)
            {
                item.IsActive = item.IsSelected;
            }

            await CoreMethods.PopPageModel(CategoryModel);
        });

    }
}
