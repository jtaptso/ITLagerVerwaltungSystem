using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;
using ITLagerVerwaltungSystem.Core.Services;

namespace ITLagerVerwaltungSystem.Infrastructure.Services
{
    public class ManagerService : IManagerService
    {
        private readonly AppDbContext _dbContext;

        public ManagerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ApproveOrder(int orderId)
        {
            var order = _dbContext.Orders.Find(orderId);
            if (order == null) return false;
            order.Status = "Approved";
            _dbContext.SaveChanges();
            return true;
        }

        public bool RejectOrder(int orderId)
        {
            var order = _dbContext.Orders.Find(orderId);
            if (order == null) return false;
            order.Status = "Rejected";
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<string> GetReporting()
        {
            var totalOrders = _dbContext.Orders.Count();
            var approvedOrders = _dbContext.Orders.Count(o => o.Status == "Approved");
            var rejectedOrders = _dbContext.Orders.Count(o => o.Status == "Rejected");
            return new List<string>
            {
                $"Total Orders: {totalOrders}",
                $"Approved Orders: {approvedOrders}",
                $"Rejected Orders: {rejectedOrders}"
            };
        }
    }
}
