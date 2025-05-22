using Infrastructuur.Dtos;

namespace Infrastructuur.Services.Classes
{
    public interface INotificationService<T> where T : class
    {
        Task<(string Result, NotificationEntity<T>? Notification)> SendAsync(NotificationEntity<T> notification);
    }
}