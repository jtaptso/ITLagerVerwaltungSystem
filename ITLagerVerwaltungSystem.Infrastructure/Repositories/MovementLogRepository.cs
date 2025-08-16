using ITLagerVerwaltungSystem.Core.Domain;
using ITLagerVerwaltungSystem.Core.Interfaces;

namespace ITLagerVerwaltungSystem.Infrastructure.Repositories
{
    public class MovementLogRepository : GenericRepository<MovementLog>, IMovementLogRepository
    {
        public MovementLogRepository(AppDbContext context) : base(context) { }
        // Implement custom methods for MovementLog if needed
    }
}
