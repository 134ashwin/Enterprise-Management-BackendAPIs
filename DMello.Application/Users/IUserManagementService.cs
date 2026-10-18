using System.Collections.Generic;
using System.Threading.Tasks;
using DMello.Application.Users.DTOs;

namespace DMello.Application.Users
{
    public interface IUserManagementService
    {
        Task<List<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
        Task<UserResponseDto?> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<UserResponseDto?> UpdateUserRoleAsync(int id, string newRole);
        Task<bool> DeleteUserAsync(int id);
    }
}
