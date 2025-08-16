using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public class WarehouseStaffService : IWarehouseStaffService
    {
        private readonly List<StockUpdateDto> _stock = new();
        public IEnumerable<StockUpdateDto> GetStock() => _stock;
        public StockUpdateDto UpdateStock(StockUpdateDto dto)
        {
            _stock.Add(dto);
            return dto;
        }
    }
}
