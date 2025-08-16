using ITLagerVerwaltungSystem.Core.Domain;
using ITLagerVerwaltungSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITLagerVerwaltungSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
        // Implement custom methods for User if needed
    }
}
