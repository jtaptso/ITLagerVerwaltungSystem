using ITLagerVerwaltungSystem.Core.Domain;
using ITLagerVerwaltungSystem.Core.Interfaces;

namespace ITLagerVerwaltungSystem.Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context) { }
        // Implement custom methods for Notification if needed
    }
}
