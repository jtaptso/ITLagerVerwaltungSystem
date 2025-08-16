
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface IMaterialService
    {
        IEnumerable<MaterialDto> GetAllMaterials();
        MaterialDto? GetMaterialById(int id);
        MaterialDto CreateMaterial(MaterialDto dto);
        MaterialDto? UpdateMaterial(int id, MaterialDto dto);
        bool DeleteMaterial(int id);
        MaterialDto? UpdateMaterialStatus(int id, MaterialStatusDto dto);
    }
}
