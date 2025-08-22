using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;
using ITLagerVerwaltungSystem.Core.Services;

namespace ITLagerVerwaltungSystem.Infrastructure.Services
{
    public class WarehouseStaffService : IWarehouseStaffService
    {
        private readonly AppDbContext _dbContext;

        public WarehouseStaffService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<StockUpdateDto> GetStock()
        {
            return _dbContext.Materials
                .Select(m => new StockUpdateDto
                {
                    MaterialId = m.Id,
                    Quantity = m.Quantity
                })
                .ToList();
        }

        public StockUpdateDto UpdateStock(StockUpdateDto dto)
        {
            if (dto.MaterialId <= 0)
                throw new System.ArgumentException("MaterialId must be positive.");
            if (dto.Quantity < 0)
                throw new System.ArgumentException("Quantity cannot be negative.");

            var material = _dbContext.Materials.FirstOrDefault(m => m.Id == dto.MaterialId);
            if (material == null)
                throw new System.Exception($"Material with ID {dto.MaterialId} not found.");

            material.Quantity = dto.Quantity;
            _dbContext.SaveChanges();
            return new StockUpdateDto
            {
                MaterialId = material.Id,
                Quantity = material.Quantity
            };
        }
    }
}
