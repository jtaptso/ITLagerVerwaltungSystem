using ITLagerVerwaltungSystem.Core.Domain;
using ITLagerVerwaltungSystem.Core.Interfaces;

namespace ITLagerVerwaltungSystem.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }
        // Implement custom methods for Order if needed
    }
}
