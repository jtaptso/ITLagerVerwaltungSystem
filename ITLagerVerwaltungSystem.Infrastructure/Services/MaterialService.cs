#nullable enable
using System.Collections.Generic;
using System.Linq;
using ITLagerVerwaltungSystem.Core.DTOs;
using ITLagerVerwaltungSystem.Core.Services;
using ITLagerVerwaltungSystem.Infrastructure;

namespace ITLagerVerwaltungSystem.Infrastructure.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly AppDbContext _dbContext;

        public MaterialService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MaterialDto> GetAllMaterials()
        {
            return _dbContext.Materials
                .Select(m => ToDto(m))
                .ToList();
        }

        public MaterialDto? GetMaterialById(int id)
        {
            var material = _dbContext.Materials.Find(id);
            return material == null ? null : ToDto(material);
        }

        public MaterialDto CreateMaterial(MaterialDto dto)
        {
            var material = new ITLagerVerwaltungSystem.Core.Domain.Material
            {
                MaterialType = dto.Type != null ? new ITLagerVerwaltungSystem.Core.Domain.MaterialType(dto.Type) : null,
                Quantity = dto.Quantity > 0 ? dto.Quantity : 1,
                Status = Enum.TryParse<ITLagerVerwaltungSystem.Core.Domain.MaterialStatus>(dto.Status, out var status) ? status : ITLagerVerwaltungSystem.Core.Domain.MaterialStatus.New,
                PicturePaths = dto.PicturePaths,
                // Add other mappings as needed
            };
            _dbContext.Materials.Add(material);
            _dbContext.SaveChanges();
            return ToDto(material);
        }

        public MaterialDto? UpdateMaterial(int id, MaterialDto dto)
        {
            var material = _dbContext.Materials.Find(id);
            if (material == null) return null;
            material.MaterialType = dto.Type != null ? new ITLagerVerwaltungSystem.Core.Domain.MaterialType(dto.Type) : null;
            material.Quantity = dto.Quantity;
            material.Status = Enum.TryParse<ITLagerVerwaltungSystem.Core.Domain.MaterialStatus>(dto.Status, out var status) ? status : material.Status;
            material.PicturePaths = dto.PicturePaths;
            // Add other mappings as needed
            _dbContext.SaveChanges();
            return ToDto(material);
        }

        public bool DeleteMaterial(int id)
        {
            var material = _dbContext.Materials.Find(id);
            if (material == null) return false;
            _dbContext.Materials.Remove(material);
            _dbContext.SaveChanges();
            return true;
        }

        public MaterialDto? UpdateMaterialStatus(int id, MaterialStatusDto dto)
        {
            var material = _dbContext.Materials.Find(id);
            if (material == null) return null;
            material.Status = Enum.TryParse<ITLagerVerwaltungSystem.Core.Domain.MaterialStatus>(dto.Status, out var status) ? status : material.Status;
            _dbContext.SaveChanges();
            return ToDto(material);
        }

        private MaterialDto ToDto(ITLagerVerwaltungSystem.Core.Domain.Material material)
        {
            return new MaterialDto
            {
                Id = material.Id,
                Type = material.MaterialType?.Value,
                Status = material.Status.ToString(),
                Quantity = material.Quantity,
                PicturePaths = material.PicturePaths,
                // Add other mappings as needed
            };
        }
    }
}
