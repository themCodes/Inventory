using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services
{
    public class UserManagerService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManagerService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Return a list of all available roles in the database.
        public List<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<bool> UpdateUserRoleByUsernameAsync(string username, string newRole)
        {
            // Find the user by their username
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return false; // User not found
            }

            // Retrieve the current roles for the user
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove the existing roles
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                return false; // Failed to remove roles
            }

            // Add the new role
            var addResult = await _userManager.AddToRoleAsync(user, newRole);

            if (!addResult.Succeeded)
            {
                return false; // Failed to add the new role
            }

            // Save the changes to the database
            var updateResult = await _userManager.UpdateAsync(user);

            return updateResult.Succeeded;
        }
    }
}
