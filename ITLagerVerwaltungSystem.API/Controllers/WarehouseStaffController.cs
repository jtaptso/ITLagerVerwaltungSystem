
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ITLagerVerwaltungSystem.Core.Services;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "WarehouseStaff")]
    public class WarehouseStaffController : ControllerBase
    {
        private readonly IWarehouseStaffService _warehouseStaffService;
        public WarehouseStaffController(IWarehouseStaffService warehouseStaffService)
        {
            _warehouseStaffService = warehouseStaffService;
        }

        // GET: api/warehousestaff/stock
        [HttpGet("stock")]
        public IActionResult GetStock()
        {
            var stock = _warehouseStaffService.GetStock();
            return Ok(stock);
        }

        // POST: api/warehousestaff/stock/update
        [HttpPost("stock/update")]
        public IActionResult UpdateStock([FromBody] StockUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = _warehouseStaffService.UpdateStock(dto);
            return Ok(updated);
        }
    }
}
