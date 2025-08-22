using System.Collections.Generic;
using System.Linq;
using ITLagerVerwaltungSystem.Core.DTOs;
using ITLagerVerwaltungSystem.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace ITLagerVerwaltungSystem.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<UserUpdateDto> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<UserUpdateDto>();
            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                userDtos.Add(new UserUpdateDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Employee"
                });
            }
            return userDtos;
        }

        public UserUpdateDto? GetUserById(int id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id.GetHashCode() == id);
            if (user == null) return null;
            var roles = _userManager.GetRolesAsync(user).Result;
            return new UserUpdateDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "Employee"
            };
        }

        public UserRegisterDto RegisterUser(UserRegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName))
                throw new System.ArgumentException("UserName is required.");
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new System.ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new System.ArgumentException("Password is required.");

            var user = new IdentityUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };
            var result = _userManager.CreateAsync(user, dto.Password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Employee").Wait();
                return new UserRegisterDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = dto.Password
                };
            }
            throw new System.Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        public UserUpdateDto? UpdateUser(int id, UserUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName))
                throw new System.ArgumentException("UserName is required.");
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new System.ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(dto.Role))
                throw new System.ArgumentException("Role is required.");

            var user = _userManager.Users.FirstOrDefault(u => u.Id.GetHashCode() == id);
            if (user == null) return null;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            var updateResult = _userManager.UpdateAsync(user).Result;
            if (!updateResult.Succeeded)
                throw new System.Exception(string.Join("; ", updateResult.Errors.Select(e => e.Description)));
            // Update roles
            var currentRoles = _userManager.GetRolesAsync(user).Result;
            if (!currentRoles.Contains(dto.Role))
            {
                foreach (var role in currentRoles)
                    _userManager.RemoveFromRoleAsync(user, role).Wait();
                _userManager.AddToRoleAsync(user, dto.Role).Wait();
            }
            return new UserUpdateDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = dto.Role
            };
        }
    }
}
