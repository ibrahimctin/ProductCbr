using Microsoft.AspNetCore.Identity;
using ProductCbrAssignment.Domain.Abstractions.Users;
using ProductCbrAssignment.Domain.Entities.Identity;

namespace ProductCbrAssignment.Application.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;


        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateManagerAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new InvalidOperationException($"User with email '{email}' not found.");
            }

 

            if (!await _userManager.IsInRoleAsync(user, "Manager"))
            {
       
                var userResult = await _userManager.AddToRoleAsync(user, "Manager");
                return userResult;
            }

            return IdentityResult.Failed();
        }


        public async Task<IdentityResult> CreateCustomerAsync(string email, string password)
        {
            var user = new AppUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = " There is no User." });

            user.Email = newEmail;
            user.UserName = newEmail;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });

            return await _userManager.DeleteAsync(user);
        }

        public async Task<List<AppUser>> GetManagersAsync()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("Yönetici");
            return usersInRole.ToList();
        }

        public async Task<List<AppUser>> GetCustomersAsync()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("Müşteri");
            return usersInRole.ToList();
        }

    }

}
