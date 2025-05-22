using Infrastructuur.Enums;
using Infrastructuur.Factories.NotificationFactory.Interfaces;
using Infrastructuur.Factories.NotificationFactory.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructuur.Models;
using Microsoft.Extensions.Options;


namespace Infrastructuur.Factories.NotificationFactory.Classes
{
    public static class NotificationSender
    {
        public static INotificationSender Notification(NotificationType notificationType)
        {
            return notificationType switch
            {
                NotificationType.SMS => new SmsSender(),
                NotificationType.EMAIL => new EmailSender(),
                _ => throw new NotImplementedException("No such notification type.")
            };
        }
    }
}
