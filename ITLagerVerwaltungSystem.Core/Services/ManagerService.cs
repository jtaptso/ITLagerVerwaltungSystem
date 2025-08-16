using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public class ManagerService : IManagerService
    {
        public bool ApproveOrder(int orderId) => true;
        public bool RejectOrder(int orderId) => true;
        public IEnumerable<string> GetReporting() => new List<string> { "Report1", "Report2" };
    }
}
