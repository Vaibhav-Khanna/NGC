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

            ColorDate = "#f5a623";
        }

        public Note Reminder { get; private set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

        public string ColorDate { get; set; }
    }
}
