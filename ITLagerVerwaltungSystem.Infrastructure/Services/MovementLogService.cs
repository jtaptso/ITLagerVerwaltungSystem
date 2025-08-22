using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;
using ITLagerVerwaltungSystem.Core.Services;

namespace ITLagerVerwaltungSystem.Infrastructure.Services
{
    public class MovementLogService : IMovementLogService
    {
        private readonly AppDbContext _dbContext;

        public MovementLogService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MovementLogCreateDto> GetAllMovementLogs()
        {
            return _dbContext.MovementLogs
                .ToList()
                .Select(m => new MovementLogCreateDto
                {
                    MaterialId = m.MaterialId,
                    UserId = int.TryParse(m.UserId, out var uid) ? uid : 0,
                    MovementType = m.MovementType.ToString(),
                    Date = m.Date
                });
        }

        public IEnumerable<MovementLogCreateDto> GetMovementLogsForMaterial(int materialId)
        {
            return _dbContext.MovementLogs
                .Where(m => m.MaterialId == materialId)
                .ToList()
                .Select(m => new MovementLogCreateDto
                {
                    MaterialId = m.MaterialId,
                    UserId = int.TryParse(m.UserId, out var uid) ? uid : 0,
                    MovementType = m.MovementType.ToString(),
                    Date = m.Date
                });
        }

        public MovementLogCreateDto LogMovement(MovementLogCreateDto dto)
        {
            // Map DTO to entity
            var movementTypeParsed = Enum.TryParse<ITLagerVerwaltungSystem.Core.Domain.MovementType>(dto.MovementType, out var movementType)
                ? movementType
                : ITLagerVerwaltungSystem.Core.Domain.MovementType.Procurement;

            var movementLog = new ITLagerVerwaltungSystem.Core.Domain.MovementLog
            {
                MaterialId = dto.MaterialId,
                UserId = dto.UserId.ToString(),
                MovementType = movementTypeParsed,
                Date = dto.Date
            };
            _dbContext.MovementLogs.Add(movementLog);
            _dbContext.SaveChanges();
            // Return confirmation DTO with new ID and details
            return new MovementLogCreateDto
            {
                MaterialId = movementLog.MaterialId,
                UserId = int.TryParse(movementLog.UserId, out var uid) ? uid : 0,
                MovementType = movementLog.MovementType.ToString(),
                Date = movementLog.Date
            };
        }
    }
}
