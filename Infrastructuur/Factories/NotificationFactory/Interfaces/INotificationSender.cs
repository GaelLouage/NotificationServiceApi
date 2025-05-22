using Infrastructuur.Dtos;
using Infrastructuur.Models;

namespace Infrastructuur.Factories.NotificationFactory.Interfaces
{
    public interface INotificationSender
    {
        Task<string> SendAsync(OptionsSettings message);
    }
}