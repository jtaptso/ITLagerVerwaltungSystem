using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public class MovementLogService : IMovementLogService
    {
        public IEnumerable<string> GetAllMovementLogs() => new List<string> { "Log1", "Log2" };
        public IEnumerable<string> GetMovementLogsForMaterial(int materialId) => new List<string> { $"Log for material {materialId}" };

        public MovementLogCreateDto LogMovement(MovementLogCreateDto dto)
        {
            // In a real implementation, this would persist to a database
            // For now, just return the dto as confirmation
            return dto;
        }
    }
}
