using System;
using NGC.Models;

namespace NGC.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactDetailCellModel
    {

        public ContactDetailCellModel(ContactDetailCellModelType type, BaseDataObject model)
        {
            CellModelType = type;

            Model = model;
        }

        public object Model { get; set; }

        public ContactDetailCellModelType CellModelType { get; set; }

        public string TagColor { get; set; } = "#F5A623";

    }

    public enum ContactDetailCellModelType
    {
        Action, Opportunity, Activity, Empty
    }
}
