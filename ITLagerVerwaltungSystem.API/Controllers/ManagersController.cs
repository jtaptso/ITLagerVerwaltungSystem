using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITLagerVerwaltungSystem.Core.Services;

namespace ITLagerVerwaltungSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        // POST: api/managers/{orderId}/approve
        [HttpPost("{orderId}/approve")]
        public IActionResult ApproveOrder(int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _managerService.ApproveOrder(orderId);
            if (!result) return NotFound();
            return Ok();
        }

        // POST: api/managers/{orderId}/reject
        [HttpPost("{orderId}/reject")]
        public IActionResult RejectOrder(int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _managerService.RejectOrder(orderId);
            if (!result) return NotFound();
            return Ok();
        }

        // GET: api/managers/reporting
        [HttpGet("reporting")]
        public IActionResult GetReporting()
        {
            var reports = _managerService.GetReporting();
            return Ok(reports);
        }
    }
}
