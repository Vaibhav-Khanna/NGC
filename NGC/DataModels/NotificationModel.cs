using System;
using NGC.Models.DataObjects;
using NGC.Helpers;
using NGC.Resources;

namespace NGC.DataModels
{

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NotificationModel
    {
        public NotificationModel(Notification notification)
        {
            Notification = notification;

            Content = Extensions.StripTagsRegexCompiled(notification.Content);

            DateTime = $"{notification.DatabaseInsertAt.ToString("dddd, dd MMMM")} {AppResources.At} {notification.DatabaseInsertAt.ToString("HH:mm")}";

            Tag = notification.Kind;
        }

        public Notification Notification { get; private set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

        public string Tag { get; set; }

        public string TagColor { get; set; } = "#6bbebb";
    }
}
