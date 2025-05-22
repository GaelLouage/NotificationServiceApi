using Infrastructuur.Dtos;
using Infrastructuur.Factories.NotificationFactory.Classes;
using Infrastructuur.Factories.NotificationFactory.Interfaces;
using Infrastructuur.Models;
using Infrastructuur.Repositories.Classes;
using Infrastructuur.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Services.Classes
{
    public class NotificationService<T> : INotificationService<T> where T : class
    {
        private readonly OptionsSettings _optionSettings;
        private readonly IMongoRepository<Logger> _logger;
        public NotificationService(OptionsSettings optionSettings, IMongoRepository<Logger> logger)
        {
            _optionSettings = optionSettings;
            _logger = logger;
        }

        public async Task<(string Result, NotificationEntity<T>? Notification)> SendAsync(NotificationEntity<T> notification)
        {
            try
            {
                if(notification.Message is EmailReciever reciever)
                {
                    _optionSettings.EmailReciever = reciever;
                }
                if(notification.Message is SmsReciever smsReciever)
                {
                    _optionSettings.SmsReciever = smsReciever;
                }
                INotificationSender notif = NotificationSender.Notification(notification.Type);
                // get message
                var notificationMessage = await notif.SendAsync(_optionSettings);
                var log = await _logger.InsertAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Message = $"{nameof(notification)}: {notificationMessage}",
                    Type = Enums.LoggerType.INFO
                });
                return ($"{nameof(notification)}: {notificationMessage}", notification);
            }
            catch (Exception ex)
            {
                var log = await _logger.InsertAsync(new Logger()
                {
                    Date = DateTime.Now,
                    Message = ex.Message,
                    Type = Enums.LoggerType.ERROR
                });
                return (ex.Message, null);
            }
        }
    }
}
