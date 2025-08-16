
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface INotificationService
    {
        IEnumerable<string> GetPendingNotifications();
        bool ApproveNotification(int id);
    }
}
