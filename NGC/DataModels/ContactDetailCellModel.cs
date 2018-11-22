using System;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactDetailCellModel
    {

        public ContactDetailCellModel()
        {

        }

        public ContactDetailCellModelType CellModelType { get; set; }

        public string TagColor { get; set; } = "#F5A623";

    }

    public enum ContactDetailCellModelType
    {
        Action, Opportunity, Activity
    }
}
