using System;
using System.Collections.ObjectModel;
using NGC.DataModels;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace NGC.ViewModels
{
    public class FilterListPageModel : BaseViewModel
    {

        public ObservableCollection<FilterCategoryModel> Filters { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            Filters = new ObservableCollection<FilterCategoryModel>((List<FilterCategoryModel>)initData);
        }


        public Command BackCommand => new Command(async() =>
        {
            ResetFilters();

            await CoreMethods.PopPageModel(true);
        });


        public Command ApplyCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(Filters.Where((arg) => arg.IsSelected),true);
        });


        public Command ItemSelectedCommand => new Command(async(obj) =>
        {
            if ((obj as FilterCategoryModel).FilterType == FilterType.ListSelector)
                await CoreMethods.PushPageModel<FilterDetailPageModel>(obj);
            else
                await CoreMethods.PushPageModel<SearchPostalPageModel>(obj);
        });


        void ResetFilters()
        {
            foreach (var item in Filters)
            {
                item.SelectedFilters = null;

                foreach (var fitem in item.CategoryItemSource)
                {
                    fitem.IsActive = false;
                    fitem.IsSelected = false;
                }
            }
        }


    }
}
