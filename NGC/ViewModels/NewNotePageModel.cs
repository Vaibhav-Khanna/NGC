using System;
using Xamarin.Forms;

namespace NGC.ViewModels
{
    public class NewNotePageModel : BaseViewModel
    {
        public NewNotePageModel()
        {
        }

        public string Note { get; set; }
        public DateTime Date { get; set; }
        public bool IsToggled { get; set; }

        public Command SaveCommand => new Command(() =>
        {

        });


        public Command BackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true, true);
        });
    }
}
