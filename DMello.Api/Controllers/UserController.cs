using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMello.Application.Users;
using DMello.Application.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DMello.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        public UserController(IUserManagementService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// GET /api/users - Fetch list of all active managed users
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// GET /api/users/{id} - Fetch single managed user by id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            return Ok(user);
        }

        /// <summary>
        /// POST /api/users - Create new user with assigned role and status
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// PUT /api/users/{id} - Update user details (Name, Mobile, Email, Role, Status)
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> Update(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.UpdateUserAsync(id, dto);
            if (updatedUser == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(updatedUser);
        }

        /// <summary>
        /// PATCH /api/users/{id}/role - Dynamically update role from table dropdown
        /// </summary>
        [HttpPatch("{id:int}/role")]
        public async Task<ActionResult<UserResponseDto>> UpdateRole(int id, [FromBody] UpdateUserRoleDto dto)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(dto.Role))
            {
                return BadRequest(new { message = "Valid Role assignment is required." });
            }

            var updatedUser = await _userService.UpdateUserRoleAsync(id, dto.Role);
            if (updatedUser == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(updatedUser);
        }

        /// <summary>
        /// DELETE /api/users/{id} - Trigger user deletion sequence (soft-delete workflow)
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(new { message = $"User {id} deleted successfully." });
        }

        /// <summary>
        /// Legacy profile endpoint
        /// </summary>
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok(new
            {
                email = User.Identity?.Name ?? "Admin",
                role = "Admin",
                status = "Active"
            });
        }
    }
}
