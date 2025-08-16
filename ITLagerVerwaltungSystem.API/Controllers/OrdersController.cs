using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ITLagerVerwaltungSystem.Core.Services;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager,Employee")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // POST: api/orders/request
        [HttpPost("request")]
        public IActionResult RequestOrder([FromBody] OrderRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = _orderService.RequestOrder(dto);
            return CreatedAtAction(nameof(GetOrder), new { id = created.OrderId }, created);
        }

        // POST: api/orders/{id}/approve
        [HttpPost("{id}/approve")]
        public IActionResult ApproveOrder(int id)
        {
            var approved = _orderService.ApproveOrder(id);
            if (approved == null) return NotFound();
            return Ok(approved);
        }

        // POST: api/orders/{id}/reject
        [HttpPost("{id}/reject")]
        public IActionResult RejectOrder(int id)
        {
            var rejected = _orderService.RejectOrder(id);
            if (rejected == null) return NotFound();
            return Ok(rejected);
        }
    }
}
