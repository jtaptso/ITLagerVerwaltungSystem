
#nullable enable
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderResponseDto> GetAllOrders(string? status = null, string? userId = null, int? skip = null, int? take = null);
        OrderResponseDto? GetOrderById(int id);
        OrderResponseDto RequestOrder(OrderRequestDto dto);
        OrderResponseDto? ApproveOrder(int id);
        OrderResponseDto? RejectOrder(int id);
    }
}
