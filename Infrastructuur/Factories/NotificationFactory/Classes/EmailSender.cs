using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructuur.Factories.NotificationFactory.Interfaces;
using Infrastructuur.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructuur.Factories.NotificationFactory.Classes
{
    public class EmailSender : INotificationSender
    {
        public async Task<string> SendAsync(OptionsSettings emailOptions)
        {
            // email sender logic
            try
            {
                    var fromAddress = new MailAddress(emailOptions.EmailData.FromEmail, emailOptions.EmailData.FromName);

                    if(string.IsNullOrWhiteSpace(emailOptions.EmailData.FromEmail) || string.IsNullOrWhiteSpace(emailOptions.EmailReciever.ToName))
                    {
                        throw new ArgumentException();
                    }
                    var toAddress = new MailAddress(emailOptions.EmailReciever.ToEmail, emailOptions.EmailReciever.ToName);
                    string fromPassword = emailOptions.EmailData.Password;
                    string subject = emailOptions.EmailData.Subject;
                    string body =  emailOptions.EmailData.Body;

                    var smtp = new SmtpClient
                    {
                        Host = emailOptions.EmailData.SmtpHost, // e.g., smtp.gmail.com
                        Port = emailOptions.EmailData.SmtpPort, // or 465 for SSL  ||587
                        EnableSsl = emailOptions.EmailData.EnableSsl,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var messageEmail = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        await smtp.SendMailAsync(messageEmail);
                    }
                return "Email sent!";
            }
            catch (Exception ex)
            {
                return $"{ex.Message}: \n Failed to send email!";
            }
        }
    }
}
