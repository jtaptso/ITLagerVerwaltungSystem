using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITLagerVerwaltungSystem.Core.Services;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager,WarehouseStaff")]
    public class MovementLogsController : ControllerBase
    {
        private readonly IMovementLogService _movementLogService;
        public MovementLogsController(IMovementLogService movementLogService)
        {
            _movementLogService = movementLogService;
        }

        // GET: api/movementlogs
        [HttpGet]
        public IActionResult GetAllMovementLogs()
        {
            var logs = _movementLogService.GetAllMovementLogs();
            return Ok(logs);
        }

        // GET: api/movementlogs/{materialId}
        [HttpGet("{materialId}")]
        public IActionResult GetMovementLogsForMaterial(int materialId)
        {
            var logs = _movementLogService.GetMovementLogsForMaterial(materialId);
            return Ok(logs);
        }
        // POST: api/movementlogs
        [HttpPost]
        public IActionResult LogMovement([FromBody] MovementLogCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = _movementLogService.LogMovement(dto);
            return CreatedAtAction(nameof(GetMovementLogsForMaterial), new { materialId = created.MaterialId }, created);
        }
    }
}
