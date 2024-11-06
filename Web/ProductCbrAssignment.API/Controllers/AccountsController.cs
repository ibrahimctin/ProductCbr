using Microsoft.AspNetCore.Mvc;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.DTOs.Identity;

namespace ProductCbrAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        IUserAuthenticationService _userAuthenticationService;

        public AccountsController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<VerifiedUserResponse>> Login([FromBody] LoginRequest user)
        {
            var loginProcess = await _userAuthenticationService.LoginUserAsync(user);
            return Ok(loginProcess);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userAuthenticationService.RegisterAsync(request);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(Register), new { request.Username }, request);
            }

            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }
    }
}