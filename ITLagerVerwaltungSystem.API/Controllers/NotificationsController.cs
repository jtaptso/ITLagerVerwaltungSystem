using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ITLagerVerwaltungSystem.Core.Services;

namespace ITLagerVerwaltungSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager,Employee,WarehouseStaff")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/notifications/pending
        [HttpGet("pending")]
        public IActionResult GetPendingNotifications()
        {
            var notifications = _notificationService.GetPendingNotifications();
            return Ok(notifications);
        }

        // POST: api/notifications/{id}/approve
        [HttpPost("{id}/approve")]
        public IActionResult ApproveNotification(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _notificationService.ApproveNotification(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
