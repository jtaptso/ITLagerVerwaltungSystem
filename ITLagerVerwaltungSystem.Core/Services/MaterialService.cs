
#nullable enable
using System.Collections.Generic;
using System.Linq;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public class MaterialService : IMaterialService
    {
        // In-memory store for demonstration
        private readonly List<MaterialDto> _materials = new List<MaterialDto>();

        public IEnumerable<MaterialDto> GetAllMaterials()
        {
            return _materials;
        }


        public MaterialDto? GetMaterialById(int id)
        {
            return _materials.FirstOrDefault(m => m.Id == id);
        }

        public MaterialDto CreateMaterial(MaterialDto dto)
        {
            dto.Id = _materials.Count > 0 ? _materials.Max(m => m.Id) + 1 : 1;
            // Ensure Quantity is set (default to 1 if not provided)
            if (dto.Quantity <= 0) dto.Quantity = 1;
            _materials.Add(dto);
            return dto;
        }


        public MaterialDto? UpdateMaterial(int id, MaterialDto dto)
        {
            var material = _materials.FirstOrDefault(m => m.Id == id);
            if (material == null) return null;
            material.Name = dto.Name;
            material.Type = dto.Type;
            material.Status = dto.Status;
            material.Quantity = dto.Quantity;
            return material;
        }

        public bool DeleteMaterial(int id)
        {
            var material = _materials.FirstOrDefault(m => m.Id == id);
            if (material == null) return false;
            _materials.Remove(material);
            return true;
        }


        public MaterialDto? UpdateMaterialStatus(int id, MaterialStatusDto dto)
        {
            var material = _materials.FirstOrDefault(m => m.Id == id);
            if (material == null) return null;
            material.Status = dto.Status;
            return material;
        }
    }
}
