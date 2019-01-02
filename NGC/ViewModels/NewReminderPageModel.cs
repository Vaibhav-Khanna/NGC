using System;
using NGC.Models.DataObjects;
using Xamarin.Forms;
using NGC.Resources;
using NGC.Helpers;
using NGC.DataModels;

namespace NGC.ViewModels
{
    public class NewReminderPageModel : BaseViewModel
    {
        public NewReminderPageModel()
        {

        }

        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.AddDays(1);

        public bool IsToggled { get; set; }

        public string ContactNumber { get; set; } = "Contact associé";

        private Contact contact;

        private RemindersModel Reminder { get; set; }


        public async override void Init(object initData)
        {
            base.Init(initData);

            if (initData is RemindersModel)
            {
                Reminder = ((RemindersModel)initData);

                Note = Reminder.Content;

                Date = Reminder.Reminder.ReminderAt.Value;

                ContactNumber = Reminder.Name;

                IsToggled = Reminder.Reminder.ContentHasRgpdData;

                contact = await StoreManager.ContactStore.GetItemAsync(Reminder.Reminder.ContactId);
            }
            else if (initData is ContactModel)
            {
                contact = ((ContactModel)initData).Contact;

                ContactNumber = contact.Name;
            }
        }


        public Command SaveCommand => new Command(async() =>
        {
            if (!string.IsNullOrWhiteSpace(Note) && Date != DateTime.Now.Date && contact != null)
            {
                ToastService.ShowLoading();

                if (Reminder == null)
                {
                    var note = new Note()
                    {
                        Subject = Note,
                        Content = Note,
                        ReminderAt = Date,
                        ContactId = contact.Id,
                        ContactFirstname = contact.Firstname,
                        ContactLastname = contact.Lastname,
                        UserId = Settings.UserId,
                        DoneAt = null,
                        ContentHasRgpdData = IsToggled,
                        Kind = "note"
                    };

                    await StoreManager.NoteStore.InsertAsync(note);
                }
                else
                {
                    Reminder.Reminder.Subject = Note;
                    Reminder.Reminder.Content = Note;
                    Reminder.Reminder.ReminderAt = Date;
                    Reminder.Reminder.ContactId = contact.Id;
                    Reminder.Reminder.ContactFirstname = contact.Firstname;
                    Reminder.Reminder.ContactLastname = contact.Lastname;
                    Reminder.Reminder.DoneAt = null;
                    Reminder.Reminder.ContentHasRgpdData = IsToggled;
                    Reminder.Reminder.Kind = "note";

                    await StoreManager.NoteStore.UpdateAsync(Reminder.Reminder);
                }

                ToastService.HideLoading();

                await CoreMethods.PopPageModel(contact,true, true);
            }
            else
            {
               await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.EnterAllDetails, AppResources.Ok);
            }
        });


        public Command BackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(true, true);
        });


        public Command ContactCommand => new Command(async() =>
        {
           var result = await ToastService.ShowSearchContact();

            if (result == null)
                ContactNumber = "Contact associé";
            else
            {
                ContactNumber = result.Name;
                contact = result;
            }

        });

    }
}
