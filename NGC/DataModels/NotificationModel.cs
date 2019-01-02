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

            Tag = ConvertKindToLanguage(notification.Kind);

            TagColor = ConvertKindToColor(notification.Kind);
        }

        public Notification Notification { get; private set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

        public string Tag { get; set; }

        public string TagColor { get; set; } = "#6bbebb";

        private string ConvertKindToLanguage(string kind)
        {
            switch (kind)
            {
                case "sequenceStepOpened":
                    return "Ouvert";
                case "mailOpened":
                    return "Ouvert";
                case "sequenceStopped":
                    return "Sequence arreté";
                case "sequenceStepReplied":
                    return "A répondu";
                case "checkinExpired":
                    return "Check-in expiré";
                case "mailReceived":
                    return "A écrit";
                case "mailReplied":
                    return "A répondu";
                default:
                    return "";
            }
        }

        private string ConvertKindToColor(string kind)
        {
            switch (kind)
            {
                case "sequenceStepOpened":
                    return "#6BB6B8";
                case "mailOpened":
                    return "#6BB6B8";
                case "sequenceStopped":
                    return "#f0ad4e";
                case "sequenceStepReplied":
                    return "#05AE0E";
                case "checkinExpired":
                    return "#d9534f";
                case "mailReceived":
                    return "#05AE0E";
                case "mailReplied":
                    return "#05AE0E";
                default:
                    return "#6bbebb";
            }
        }

    }
}
