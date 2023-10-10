using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Inventory.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public string showMessage = "";
        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

            showMessage = DoesTheAdmingroupExist();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            if(ModelState.IsValid)
            {
                // Add the user.
                var identityUser = new IdentityUser { UserName = Input.Username};
                var addUserResult = await _userManager.CreateAsync(identityUser, Input.Password);

                // Create roles if they don't exist and add the user to the correct role depending on the output.
                string addUserToThisRole = "Unassigned";
                if (!_roleManager.RoleExistsAsync("Administrator").Result)
                {
                    IdentityRole identityRole;

                    identityRole = new IdentityRole("Administrator");
                    await _roleManager.CreateAsync(identityRole);

                    identityRole = new IdentityRole("User");
                    await _roleManager.CreateAsync(identityRole);

                    identityRole = new IdentityRole("Unassigned");
                    await _roleManager.CreateAsync(identityRole);

                    addUserToThisRole = "Administrator";
                }
                var addUserToRoleResult = await _userManager.AddToRoleAsync(identityUser, addUserToThisRole);

                // If the user was added successfully: log in the user.
                if (addUserResult.Succeeded && addUserToRoleResult.Succeeded)
                {
                    await _signInManager.SignInAsync(identityUser,isPersistent: false);
                    return LocalRedirect(ReturnUrl);
                }
            }

            return Page();
        }

        private string DoesTheAdmingroupExist()
        {
            if (!_roleManager.RoleExistsAsync("Administrator").Result)
            {
                return "Det finns inget administratörskonto, så kontot du skapar nu kommer att få den rollen.";
            }

            return "";
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
