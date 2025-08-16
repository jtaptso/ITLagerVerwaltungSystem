
#nullable enable
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Services
{
    public interface IUserService
    {
        IEnumerable<UserUpdateDto> GetAllUsers();
        UserUpdateDto? GetUserById(int id);
        UserRegisterDto RegisterUser(UserRegisterDto dto);
        UserUpdateDto? UpdateUser(int id, UserUpdateDto dto);
    }
}
