using System;
using NGC.Models.DataObjects;

namespace NGC.DataModels
{   
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class RemindersModel
    {
        public RemindersModel(Note reminder)
        {
            Reminder = reminder;

            Name = $"{reminder.ContactFirstname} {reminder.ContactLastname}";

            Content = reminder.Content;

            DateTime = reminder.ReminderAt.Value.ToString("dddd, dd MMMM");

            if (!reminder.DoneAt.HasValue)
                ColorDate = ConvertDateToColor(reminder.ReminderAt.Value);
            else
                ColorDate = "#7CFC00"; // green
        }


        public Note Reminder { get; private set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

        public string ColorDate { get; set; }


        private string ConvertDateToColor(DateTime date)
        {
            var differnce = date.Date.Subtract(System.DateTime.Now.Date).Days;

            if (differnce < 0)
                return "#ff6565"; // Red
            else if (differnce >= 0 && differnce <= 7)
                return "#f5a623"; // orange
            else if (differnce > 7)
                return "#c69f00"; // yellow
            else return "#d9d9d9";

        }

    }
}
