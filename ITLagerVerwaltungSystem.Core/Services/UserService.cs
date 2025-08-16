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
            // For demo, just return the input
            return dto;
        }

        public UserUpdateDto UpdateUser(int id, UserUpdateDto dto)
        {
            var user = _users.FirstOrDefault(u => u.UserName.GetHashCode() == id);
            if (user == null) return null;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.Role = dto.Role;
            return user;
        }
    }
}
