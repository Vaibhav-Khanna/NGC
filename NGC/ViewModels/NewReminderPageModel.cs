using System;
using NGC.Models;
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

        public Command SaveCommand => new Command(() =>
        {
            if (!string.IsNullOrWhiteSpace(Note) && Date!= DateTime.Now.Date && contact != null)
            {

            }
        });


        public Command BackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(true, true);
        });


        public Command ContactCommand => new Command(async() =>
        {
           var result = await ToastService.ShowSearchContact();
        });

    }
}
