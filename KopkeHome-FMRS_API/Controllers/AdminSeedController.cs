using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KopkeHome_ModelLayer.DataModel;

namespace KopkeHome_FMRS_API.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class AdminSeedController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AdminSeedController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> SeedAdmin()
        {
            string roleName = "Admin";
            string email = "admin@gmail.com";
            string password = "12345678";

            // 1. Role
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new Role { Name = roleName });
            }

            // 2. Already exists check
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                return Ok("Admin already exists");

            // 3. Create FULL user object (important fields)
            var admin = new User
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,

                // IMPORTANT custom fields (your schema)
                FirstName = "Kayla",
                LastName = "Kopke",
                PhoneNumber = "313-300-8122",
                BusinessName = "Kopke Remodeling & Design",
                City = "Sterling Heights",
                BusinessAddress = "38200 Van Dyke Ave",
                ZipCode = "48312",
                State = "Michigan",
                IsEmailVerified = true,
                IsDocumentsVerified = true,
                // RoleId = 5, // Constant.Admin
                CreatedOn = DateTime.UtcNow
            };

            // 4. Create user properly (Identity handles hashing/normalization)
            var result = await _userManager.CreateAsync(admin, password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // 5. Assign role
            await _userManager.AddToRoleAsync(admin, roleName);

            return Ok("Admin seeded successfully");
        }

        [HttpGet("delete-admin")]
        public async Task<IActionResult> DeleteAdmin()
        {
            string email = "admin@gmail.com";

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("Admin not found");

            await _userManager.DeleteAsync(user);

            return Ok("Admin deleted");
        }
    }
}