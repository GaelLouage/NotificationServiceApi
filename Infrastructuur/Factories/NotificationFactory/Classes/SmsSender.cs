using Infrastructuur.Factories.NotificationFactory.Interfaces;
using Infrastructuur.Models;
using System.Diagnostics.CodeAnalysis;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Infrastructuur.Factories.NotificationFactory.Classes
{
    public class SmsSender : INotificationSender
    {
        public async Task<string> SendAsync(OptionsSettings data)
        {
            try
            {
                TwilioClient.Init(data.SmsData.AccountSid, data.SmsData.AuthToken);

                var messageResult = await MessageResource.CreateAsync(
                    body: data.EmailData.Body,
                    from: new PhoneNumber(data.SmsData.FromPhone),
                    to: new PhoneNumber(data.SmsReciever.ToPhone)
                );

                return $"Message SID: {messageResult.Sid}";
            }
            catch (Exception ex)
            {
                return $"Failed to send sms - Error: {ex.Message}";
            }
        }
    }
}
