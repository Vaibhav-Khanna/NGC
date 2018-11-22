using System;
using NGC.Helpers;
using NGC.DataModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;

namespace NGC.ViewModels
{
    public class ContactListDetailPageModel : BaseViewModel
    {
       
        public ObservableCollection<ObservableGroupCollection<ContactDetailCellModel>> DetailSource { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            DetailSource = new ObservableCollection<ObservableGroupCollection<ContactDetailCellModel>>();

            var actions = new ObservableGroupCollection<ContactDetailCellModel>(){ Key = "Actions à venir" };
            actions.Add(new ContactDetailCellModel());
            actions.Add(new ContactDetailCellModel());

            var opportunity = new ObservableGroupCollection<ContactDetailCellModel>() { Key = "Opportunités", Detail = "2 opportunités - 30.000 €" };
            opportunity.Add(new ContactDetailCellModel(){ CellModelType = ContactDetailCellModelType.Opportunity });
            opportunity.Add(new ContactDetailCellModel(){ CellModelType = ContactDetailCellModelType.Opportunity, TagColor = "#ff6565" });

            var activity = new ObservableGroupCollection<ContactDetailCellModel>() { Key = "Flux d’activités" };
            activity.Add(new ContactDetailCellModel() { CellModelType = ContactDetailCellModelType.Activity });
            activity.Add(new ContactDetailCellModel() { CellModelType = ContactDetailCellModelType.Activity });

            DetailSource.Add(actions);
            DetailSource.Add(opportunity);
            DetailSource.Add(activity);
        }


        public Command AddCommand => new Command(async () =>
        {
            var handler = await ToastService.ShowActionSheet("Ajouter", new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("btCheckList.png", "Check In"), 
                new Tuple<string, string>("btpopnote.png", "Note"),
                new Tuple<string, string>("btpopbell.png", "Rappel"),
                new Tuple<string, string>("btpopcash.png", "Opportunité")
                }, "Annuler");

            switch (handler)
            {
                case "Rappel":
                    await CoreMethods.PushPageModel<NewReminderPageModel>(null,true);
                    break;
                case "Check In":
                    await CoreMethods.PushPageModel<CheckInPageModel>(null, true);
                    break;
                case "Note":
                    await CoreMethods.PushPageModel<NewNotePageModel>(null, true);
                    break;
                default:
                    break;
            }
        });


        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);


        }

    }
}
