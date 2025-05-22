using Infrastructuur.Models;
using Infrastructuur.Repositories.Classes;
using Infrastructuur.Repositories.Interfaces;
using Infrastructuur.Services.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Bootstrapper
{
    public static class ServiceExtensions
    {
        public static void AddService(this IServiceCollection services)
        {

            services.AddScoped((Func<IServiceProvider, INotificationService<SmsReciever>>)(sp =>
            {
                var connectionString = GetConnectionString(sp);
                var options = sp.GetRequiredService<IOptions<OptionsSettings>>().Value;
                return new NotificationService<SmsReciever>(options, GetMongoInstance(connectionString));
            }));

           services.AddScoped<INotificationService<EmailReciever>>(sp =>
            {
                var connectionString = GetConnectionString(sp);
                var options = sp.GetRequiredService<IOptions<OptionsSettings>>().Value;
                return new NotificationService<EmailReciever>(options, GetMongoInstance(connectionString));
            });
        }

        private static string? GetConnectionString(IServiceProvider sp)
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("MongoDatabase");
            return connectionString;
        }

        private static MongoRepository<Logger> GetMongoInstance(string connectionString) =>
            new MongoRepository<Logger>(connectionString, "Notification", "NotificationCollection");
    }
}
