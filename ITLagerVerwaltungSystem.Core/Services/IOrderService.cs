
#nullable enable
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderResponseDto> GetAllOrders();
        OrderResponseDto? GetOrderById(int id);
        OrderResponseDto RequestOrder(OrderRequestDto dto);
        OrderResponseDto? ApproveOrder(int id);
        OrderResponseDto? RejectOrder(int id);
    }
}
