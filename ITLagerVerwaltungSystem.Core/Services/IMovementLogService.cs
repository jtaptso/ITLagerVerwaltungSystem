
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface IMovementLogService
    {
        IEnumerable<MovementLogCreateDto> GetAllMovementLogs();
        IEnumerable<MovementLogCreateDto> GetMovementLogsForMaterial(int materialId);

        MovementLogCreateDto LogMovement(MovementLogCreateDto dto);
    }
}
