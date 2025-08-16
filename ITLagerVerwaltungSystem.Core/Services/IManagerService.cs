
#nullable enable
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface IManagerService
    {
        bool ApproveOrder(int orderId);
        bool RejectOrder(int orderId);
        IEnumerable<string> GetReporting();
    }
}
