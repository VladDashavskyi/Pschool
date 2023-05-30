using Pschool.API.Dto;
using Pschool.API.Enum;
using PSchool.API.DAL.Entities;
using System.Data;

namespace Pschool.API.Interfaces
{
    public interface IMainService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync(Role? role);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserUpdateDto userDto);
        Task<UserDto> UpdateUserAsync(int userId,UserUpdateDto userDto);
        Task DeleteUserAsync(int userId);
        
    }
}
