using System;
using NGC.Helpers;
using NGC.DataModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace NGC.ViewModels
{
    public class ContactListDetailPageModel : BaseViewModel
    {
       
        public ObservableCollection<ObservableGroupCollection<ContactDetailCellModel>> DetailSource { get; set; }

        public ContactModel Contact { get; set; }


        public Command ModifyCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<NewContactPageModel>(data:new Tuple<bool,bool,object>(false,true,false),modal:true);
        });

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
                case "Opportunité":
                    await CoreMethods.PushPageModel<NewOpportunityPageModel>(null, false);
                    break;
                default:
                    break;
            }
        });


        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is ContactModel)
            {
                Contact = ((ContactModel)initData);

                GetData();
            }
        }


        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);


        }

        async void GetData() 
        {
            IsLoading = true;


            var checkins = await StoreManager.CheckinStore.GetCheckinsByContactId(Contact.Contact.Id);

            var opportunities = await StoreManager.OpportunityStore.GetOpportunitiesByContactId(Contact.Contact.Id);

            var notes = await StoreManager.NoteStore.GetRemindersByContactId(Contact.Contact.Id);


            DetailSource = new ObservableCollection<ObservableGroupCollection<ContactDetailCellModel>>();

            var actions = new ObservableGroupCollection<ContactDetailCellModel>() { Key = "Actions à venir" };
         
             if (notes != null && notes.Any())
                foreach (var item in notes)
                {
                    actions.Add(new ContactDetailCellModel(ContactDetailCellModelType.Activity, item) { CellModelType = ContactDetailCellModelType.Activity });
                }
            else
            {
                actions.Add(new ContactDetailCellModel(ContactDetailCellModelType.Empty, null));
            }


            var opportunity = new ObservableGroupCollection<ContactDetailCellModel>() { Key = "Opportunités", Detail = "" };

            if (opportunities != null && opportunities.Any())
            {
                opportunity.Detail = $"{opportunities.Count()} opportunités";

                long Total = 0;

                foreach (var item in opportunities)
                {
                    opportunity.Add(new ContactDetailCellModel(ContactDetailCellModelType.Opportunity, item) { TagColor = "#ff6565" });
                    Total += item.Amount;
                }

                opportunity.Detail += $" - {Total} €";
            }
            else
            {
                opportunity.Add(new ContactDetailCellModel(ContactDetailCellModelType.Empty, null));
            }


            var activity = new ObservableGroupCollection<ContactDetailCellModel>() { Key = "Flux d’activités" };

            if (checkins != null && checkins.Any())
                foreach (var item in checkins)
                {
                    var user = await StoreManager.UserStore.GetItemAsync(item.UserId);
                    var checkinType = await StoreManager.CheckinTypeStore.GetItemAsync(item.CheckinTypeId);

                    if (user != null)
                        item.UserName = user.Name;
                    if (checkinType != null)
                        item.Subject = checkinType.Name;

                    activity.Add(new ContactDetailCellModel(ContactDetailCellModelType.Activity, item) { CellModelType = ContactDetailCellModelType.Activity });
                }
            else
            {
                activity.Add(new ContactDetailCellModel(ContactDetailCellModelType.Empty, null));
            }

            DetailSource.Add(actions);
            DetailSource.Add(opportunity);
            DetailSource.Add(activity);

            IsLoading = false;
        }


    }
}
