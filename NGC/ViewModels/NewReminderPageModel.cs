using System;
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

        public Command SaveCommand => new Command(() =>
        {

        });


        public Command BackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(true, true);
        });

    }
}
