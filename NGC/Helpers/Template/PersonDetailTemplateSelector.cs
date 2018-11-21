using System;
using NGC.DataModels;
using Xamarin.Forms;

namespace NGC.Helpers.Template
{
    public class PersonDetailTemplateSelector : DataTemplateSelector
    {       
        public DataTemplate ActionTemplate { get; set; }
        public DataTemplate OpportunitiesTemplate { get; set; }
        public DataTemplate ActivityTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch ((item as ContactDetailCellModel).CellModelType)
            {
                case ContactDetailCellModelType.Action:
                    return ActionTemplate;
                case ContactDetailCellModelType.Activity:
                    return ActivityTemplate;
                case ContactDetailCellModelType.Opportunity:
                    return OpportunitiesTemplate;
                default: return ActionTemplate;
            }
        }
    }
}
