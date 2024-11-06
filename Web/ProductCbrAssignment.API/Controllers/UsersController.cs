using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCbrAssignment.Domain.Abstractions.Users;
using ProductCbrAssignment.Domain.DTOs.Users;
using ProductCbrAssignment.Domain.Entities.Identity;

namespace ProductCbrAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("create-manager")]
        public async Task<IActionResult> CreateManager([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateManagerAsync(request.Email);
            if (result.Succeeded)
                return Ok("Manager Created.");

            return BadRequest(result.Errors);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateCustomerAsync(request.Email, request.Password);
            if (result.Succeeded)
                return Ok("Customer Created.");

            return BadRequest(result.Errors);
        }

        [Authorize(Roles = "Customer,Manager")]
        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateUserAsync(userId, request.NewEmail);
            if (result.Succeeded)
                return Ok("User Updated.");

            return BadRequest(result.Errors);
        }

        [Authorize(Roles = "Customer,Manager")]
        [HttpPost("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (result.Succeeded)
                return Ok("User deleted.");

            return BadRequest(result.Errors);
        }

        [Authorize(Roles = "Customer,Manager")]
        [HttpGet("list-managers")]
        public async Task<ActionResult<List<AppUser>>> ListManagers()
        {
            var admins = await _userService.GetManagersAsync();
            return Ok(admins);
        }

        [Authorize]
        [HttpGet("list-customers")]
        public async Task<ActionResult<List<AppUser>>> ListCustomers()
        {
            var customers = await _userService.GetCustomersAsync();
            return Ok(customers);
        }
    }
}
