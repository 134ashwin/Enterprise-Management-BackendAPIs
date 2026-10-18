using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMello.Application.Users.DTOs;
using DMello.Domain.Interfaces;
using DMello.Domain.Models;

namespace DMello.Application.Users
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IManagedUserRepository _userRepository;

        public UserManagementService(IManagedUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToResponseDto).ToList();
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : MapToResponseDto(user);
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                throw new InvalidOperationException($"User with email '{dto.Email}' already exists.");
            }

            var entity = new ManagedUser
            {
                FullName = dto.Name.Trim(),
                MobileNumber = dto.MobileNumber.Trim(),
                Email = dto.Email.Trim().ToLowerInvariant(),
                Role = string.IsNullOrWhiteSpace(dto.Role) ? "Employee" : dto.Role.Trim(),
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "Active" : dto.Status.Trim(),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _userRepository.AddAsync(entity);
            return MapToResponseDto(created);
        }

        public async Task<UserResponseDto?> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            user.FullName = dto.Name.Trim();
            user.MobileNumber = dto.MobileNumber.Trim();
            user.Email = dto.Email.Trim().ToLowerInvariant();
            user.Role = string.IsNullOrWhiteSpace(dto.Role) ? user.Role : dto.Role.Trim();
            user.Status = string.IsNullOrWhiteSpace(dto.Status) ? user.Status : dto.Status.Trim();
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return MapToResponseDto(user);
        }

        public async Task<UserResponseDto?> UpdateUserRoleAsync(int id, string newRole)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            user.Role = newRole.Trim();
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return MapToResponseDto(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }

        private static UserResponseDto MapToResponseDto(ManagedUser user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.FullName,
                MobileNumber = user.MobileNumber,
                Email = user.Email,
                Role = user.Role,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
