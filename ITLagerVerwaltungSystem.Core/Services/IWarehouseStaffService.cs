
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface IWarehouseStaffService
    {
        IEnumerable<StockUpdateDto> GetStock();
        StockUpdateDto UpdateStock(StockUpdateDto dto);
    }
}
