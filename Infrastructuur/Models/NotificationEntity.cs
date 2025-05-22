using Infrastructuur.Enums;
using Infrastructuur.Factories.NotificationFactory.Classes;
using Infrastructuur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Dtos
{
    public class NotificationEntity<T>
    {
        public NotificationType Type { get; set; }
        public T Message { get; set; }
    }
}
