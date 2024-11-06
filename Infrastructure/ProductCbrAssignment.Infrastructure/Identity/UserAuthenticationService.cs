using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.DTOs.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;
using ProductCbrAssignment.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;

namespace ProductCbrAssignment.Infrastructure.Identity.Impl
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwtAuthentication;
        private readonly JwtOptions _jwtOptions;
        private readonly IMapper _mapper;
        

        public UserAuthenticationService(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IJwtTokenService jwtAuthentication,
            IOptions<JwtOptions> jwtOptions,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthentication = jwtAuthentication;
            _jwtOptions = jwtOptions.Value;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
        {
            var user = _mapper.Map<AppUser>(request);

            if (user is null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = "REGISTRATION ERROR" });
            }
            var userCreation = await _userManager.CreateAsync(user, request.Password);

            if (!userCreation.Succeeded)
            {
                return IdentityResult.Failed(new IdentityError() { Description = "REGISTRATION ERROR" });
            }

            return IdentityResult.Success;
        }

        public async Task<VerifiedUserResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                throw new Exception($"User with {loginRequest.Email} not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{loginRequest.Email} aren't valid'.");
            }

            var jwtSecurityToken = await _jwtAuthentication.GenerateJsonWebToken(user);

            VerifiedUserResponse response = new VerifiedUserResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,
                IsSuccess = true
            };

            return response;
        }
    }
}

