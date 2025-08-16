using ITLagerVerwaltungSystem.Core.Domain;
using ITLagerVerwaltungSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITLagerVerwaltungSystem.Infrastructure.Repositories
{
    public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(AppDbContext context) : base(context) { }
        // Implement custom methods for Material if needed
    }
}
