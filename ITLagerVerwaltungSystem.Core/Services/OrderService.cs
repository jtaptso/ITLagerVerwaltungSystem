
#nullable enable
using System.Collections.Generic;
using System.Linq;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public class OrderService : IOrderService
    {
        // In-memory store for demonstration
        private readonly List<OrderResponseDto> _orders = new List<OrderResponseDto>();

        public IEnumerable<OrderResponseDto> GetAllOrders()
        {
            return _orders;
        }

        public OrderResponseDto? GetOrderById(int id)
        {
            return _orders.FirstOrDefault(o => o.OrderId == id);
        }

        public OrderResponseDto RequestOrder(OrderRequestDto dto)
        {
            var order = new OrderResponseDto
            {
                OrderId = _orders.Count > 0 ? _orders.Max(o => o.OrderId) + 1 : 1,
                Status = "Requested",
                // Map other properties as needed
            };
            _orders.Add(order);
            return order;
        }

        public OrderResponseDto? ApproveOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return null;
            order.Status = "Approved";
            return order;
        }

        public OrderResponseDto? RejectOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return null;
            order.Status = "Rejected";
            return order;
        }
    }
}
