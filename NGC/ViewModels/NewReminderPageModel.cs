using System;
using NGC.Models.DataObjects;
using Xamarin.Forms;


namespace NGC.ViewModels
{
    public class NewReminderPageModel : BaseViewModel
    {
        public NewReminderPageModel()
        {
        }

        public string Note { get; set; }

        public DateTime Date { get; set; }

        public bool IsToggled { get; set; }

        public string ContactNumber { get; set; } = "Contact associé";

        private Contact contact;

        public Command SaveCommand => new Command(async() =>
        {
            if (!string.IsNullOrWhiteSpace(Note) && Date != DateTime.Now.Date && contact != null)
            {
                ToastService.ShowLoading();

                var note = new Note()
                {
                    Subject = Note,
                    Content = Note,
                    ReminderAt = Date,
                    ContactId = contact.Id,
                    ContentHasRgpdData = IsToggled,
                    Kind = "note"
                };

                await StoreManager.NoteStore.InsertAsync(note);

                ToastService.HideLoading();

                await CoreMethods.PopPageModel(true, true);
            }
            else
            {
            // warning 
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
