using System;
using System.Collections.ObjectModel;
using NGC.DataModels;

namespace NGC.ViewModels
{
    public class SearchPostalPageModel : BaseViewModel
    {

        public ObservableCollection<string> ItemSource { get; set; }

        string searchtext;
        [PropertyChanged.DoNotNotify]
        public string SearchText { get { return searchtext; } set { searchtext = value; Search(); RaisePropertyChanged(); } }

        public string Selected { get; set; }

        public SearchPostalPageModel()
        {
            Title = "Filtrer par code postal";
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var data = initData as FilterCategoryModel;           
        }

        void Search()
        {

        }

    }
}
