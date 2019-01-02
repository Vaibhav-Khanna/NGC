using System;
using NGC.DataModels;
using NGC.Models.DataObjects;
using Xamarin.Forms;

namespace NGC.ViewModels
{
    public class NewNotePageModel : BaseViewModel
    {
        public NewNotePageModel()
        {
        }

        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.Date;

        public bool IsToggled { get; set; }

        private ContactModel Contact { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is ContactModel)
            {
                Contact = ((ContactModel)initData);
            }
        }


        public Command SaveCommand => new Command(async() =>
        {
            if (!string.IsNullOrWhiteSpace(Note) && Contact != null)
            {
                ToastService.ShowLoading();

                var note = new Note()
                {
                    ContactId = Contact.Contact.Id,
                    ContactFirstname = Contact.Contact.Firstname,
                    ContactLastname = Contact.Contact.Lastname,
                    ContactCompanyName = Contact.Contact.CompanyName,
                    UserId = NGC.Helpers.Settings.UserId,
                    Subject = Note,
                    Content = Note,
                    ReminderAt = Date,
                    DoneAt = Date,
                    ContentHasRgpdData = IsToggled,
                    Kind = "note"
                };

               var isAdded = await  StoreManager.NoteStore.InsertAsync(note);

                ToastService.HideLoading();
               
                if (isAdded) 
                {
                    await CoreMethods.PopPageModel(note,true, true);
                }
            }
        });


        public Command BackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true, true);
        });
    }
}
