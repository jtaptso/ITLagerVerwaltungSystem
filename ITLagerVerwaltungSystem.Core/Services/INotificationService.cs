
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface INotificationService
    {
        IEnumerable<NotificationDto> GetPendingNotifications();
        bool ApproveNotification(int id);
        bool MarkAsUnread(int id);
        NotificationDto CreateNotification(string userId, string message);
        IEnumerable<NotificationDto> GetNotificationsForUser(string userId);
    }
}
