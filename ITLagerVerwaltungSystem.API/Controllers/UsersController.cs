using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ITLagerVerwaltungSystem.Core.Services;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager,Employee,WarehouseStaff")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registered = _userService.RegisterUser(dto);
            if (registered?.UserName == null)
                return BadRequest();
            var userIdHash = registered.UserName.GetHashCode();
            return CreatedAtAction(nameof(GetUser), new { id = userIdHash }, registered);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = _userService.UpdateUser(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
