using System.Collections.Generic;
using System.Linq;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserUpdateDto> _users = new();

        public IEnumerable<UserUpdateDto> GetAllUsers() => _users;

        public UserUpdateDto GetUserById(int id) => _users.FirstOrDefault(u => u.UserName.GetHashCode() == id);

        public UserRegisterDto RegisterUser(UserRegisterDto dto)
        {
            // Always assign 'Employee' as the default role on registration
            var registered = new UserRegisterDto
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password
            };

            // Add to user list as Employee (simulate persistence)
            _users.Add(new UserUpdateDto
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Role = "Employee"
            });

            return registered;
        }

        public UserUpdateDto? UpdateUser(int id, UserUpdateDto dto)
        {
            var user = _users.FirstOrDefault(u => u.UserName?.GetHashCode() == id);
            if (user == null) return null;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.Role = dto.Role;
            return user;
        }
    }
}
