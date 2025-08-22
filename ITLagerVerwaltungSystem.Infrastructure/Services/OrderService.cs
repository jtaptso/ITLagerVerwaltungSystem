#nullable enable
using System.Collections.Generic;
using System.Linq;
using ITLagerVerwaltungSystem.Core.DTOs;
using ITLagerVerwaltungSystem.Core.Services;

namespace ITLagerVerwaltungSystem.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all orders with optional filtering and pagination
        public IEnumerable<OrderResponseDto> GetAllOrders(string? status = null, string? userId = null, int? skip = null, int? take = null)
        {
            return GetAllOrdersAdvanced(null, status, userId, null, null, skip, take);
        }

        // Advanced version with sorting and date range filtering
        public IEnumerable<OrderResponseDto> GetAllOrdersAdvanced(
            string? sortBy = null,
            string? status = null,
            string? userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? skip = null,
            int? take = null)
        {
            var query = _dbContext.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);
            if (!string.IsNullOrEmpty(userId))
                query = query.Where(o => o.UserId == userId);
            if (fromDate.HasValue)
                query = query.Where(o => o.DateRequested >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(o => o.DateRequested <= toDate.Value);

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "date_asc":
                        query = query.OrderBy(o => o.DateRequested);
                        break;
                    case "date_desc":
                        query = query.OrderByDescending(o => o.DateRequested);
                        break;
                    case "status_asc":
                        query = query.OrderBy(o => o.Status);
                        break;
                    case "status_desc":
                        query = query.OrderByDescending(o => o.Status);
                        break;
                }
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return query
                .Select(o => ToDto(o))
                .ToList();
        }

        public OrderResponseDto? GetOrderById(int id)
        {
            var order = _dbContext.Orders.Find(id);
            return order == null ? null : ToDto(order);
        }

        public OrderResponseDto RequestOrder(OrderRequestDto dto)
        {
            var materials = dto.MaterialIds?.Select(mid => _dbContext.Materials.Find(mid)).Where(m => m != null).Cast<ITLagerVerwaltungSystem.Core.Domain.Material>().ToList() ?? new List<ITLagerVerwaltungSystem.Core.Domain.Material>();
            var order = new ITLagerVerwaltungSystem.Core.Domain.Order
            {
                UserId = dto.UserId.ToString(),
                DateRequested = DateTime.UtcNow,
                Status = "Requested",
                Materials = materials
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return ToDto(order);
        }

        public OrderResponseDto? ApproveOrder(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order == null) return null;
            order.Status = "Approved";
            _dbContext.SaveChanges();
            return ToDto(order);
        }

        public OrderResponseDto? RejectOrder(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order == null) return null;
            order.Status = "Rejected";
            _dbContext.SaveChanges();
            return ToDto(order);
        }

        private OrderResponseDto ToDto(ITLagerVerwaltungSystem.Core.Domain.Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.Id,
                UserId = int.TryParse(order.UserId, out var uid) ? uid : 0,
                MaterialIds = order.Materials?.Select(m => m.Id).ToList() ?? new List<int>(),
                Status = order.Status,
                Message = $"Order {order.Id} for user {order.UserId} is {order.Status}"
            };
        }
    }
}
