using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public class NotificationService : INotificationService
    {
        public IEnumerable<string> GetPendingNotifications() => new List<string> { "Notification1", "Notification2" };
        public bool ApproveNotification(int id) => true;
    }
}
