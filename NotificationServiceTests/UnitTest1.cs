using Infrastructuur.Enums;
using Infrastructuur.Factories.NotificationFactory.Classes;

namespace NotificationServiceTests
{
    public class Tests
    {
     
        [Test]
        public void NotificationTestEmail()
        {
            var sender = NotificationSender.Notification(NotificationType.EMAIL);
            Assert.IsInstanceOf<EmailSender>(sender);
        }

        [Test]
        public void NotificationTestSms()
        {
            var sender = NotificationSender.Notification(NotificationType.SMS);
            Assert.IsInstanceOf<EmailSender>(sender);
        }
    }
}