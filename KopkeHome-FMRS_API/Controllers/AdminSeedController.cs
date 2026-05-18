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
            string email = "saqib@gmail.com";
            string password = "12345678";

            // 1. Ensure Role exists
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(new Role
                {
                    Name = roleName
                });

                if (!roleResult.Succeeded)
                    return BadRequest(roleResult.Errors);
            }

            // 2. Check if user exists
            var existingUser = await _userManager.FindByEmailAsync(email);

            // 3. Toggle delete behavior
            if (existingUser != null)
            {
                await _userManager.DeleteAsync(existingUser);
                return Ok("Admin deleted. Call again to re-create.");
            }

            // 4. IMPORTANT: RoleId MUST match your DB (Admin = 5)
            var admin = new User
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,

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

                RoleId = 5, // 🔥 THIS IS CRITICAL (Admin in your DB)

                CreatedOn = DateTime.UtcNow
            };

            // 5. Create user
            var result = await _userManager.CreateAsync(admin, password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // 6. Assign Identity role ALSO
            await _userManager.AddToRoleAsync(admin, roleName);

            return Ok("Admin seeded successfully");
        }

        [HttpGet("delete-admin")]
        public async Task<IActionResult> DeleteAdmin()
        {
            string email = "saqib@gmail.com";

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("Admin not found");

            await _userManager.DeleteAsync(user);

            return Ok("Admin deleted");
        }
    }
}










































// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Identity;
// using KopkeHome_ModelLayer.DataModel;

// namespace KopkeHome_FMRS_API.Controllers
// {
//     [Route("api/seed")]
//     [ApiController]
//     public class AdminSeedController : ControllerBase
//     {
//         private readonly UserManager<User> _userManager;
//         private readonly RoleManager<Role> _roleManager;

//         public AdminSeedController(
//             UserManager<User> userManager,
//             RoleManager<Role> roleManager)
//         {
//             _userManager = userManager;
//             _roleManager = roleManager;
//         }

//         [HttpGet("admin")]
//         public async Task<IActionResult> SeedAdmin()
//         {
//             string roleName = "Admin";
//             string email = "saqib@gmail.com";
//             string password = "12345678";

//             // 1. Ensure Role exists
//             if (!await _roleManager.RoleExistsAsync(roleName))
//             {
//                 var roleResult = await _roleManager.CreateAsync(new Role
//                 {
//                     Name = roleName
//                 });

//                 if (!roleResult.Succeeded)
//                     return BadRequest(roleResult.Errors);
//             }

//             // 2. Check if user exists
//             var existingUser = await _userManager.FindByEmailAsync(email);

//             // 3. If exists → DELETE (toggle behavior)
//             if (existingUser != null)
//             {
//                 var deleteResult = await _userManager.DeleteAsync(existingUser);

//                 if (!deleteResult.Succeeded)
//                     return BadRequest(deleteResult.Errors);

//                 return Ok("Existing admin deleted. Call again to re-seed.");
//             }

//             // 4. Create admin user
//             var admin = new User
//             {
//                 UserName = email,
//                 Email = email,
//                 EmailConfirmed = true,

//                 FirstName = "Kayla",
//                 LastName = "Kopke",
//                 PhoneNumber = "313-300-8122",
//                 BusinessName = "Kopke Remodeling & Design",
//                 City = "Sterling Heights",
//                 BusinessAddress = "38200 Van Dyke Ave",
//                 ZipCode = "48312",
//                 State = "Michigan",

//                 IsEmailVerified = true,
//                 IsDocumentsVerified = true,

//                 CreatedOn = DateTime.UtcNow
//             };

//             // 5. Create user
//             var result = await _userManager.CreateAsync(admin, password);

//             if (!result.Succeeded)
//                 return BadRequest(result.Errors);

//             // 6. Assign role
//             var roleAssignResult = await _userManager.AddToRoleAsync(admin, roleName);

//             if (!roleAssignResult.Succeeded)
//                 return BadRequest(roleAssignResult.Errors);

//             return Ok("Admin seeded successfully");
//         }

//         [HttpGet("delete-admin")]
//         public async Task<IActionResult> DeleteAdmin()
//         {
//             string email = "saqib@gmail.com";

//             var user = await _userManager.FindByEmailAsync(email);

//             if (user == null)
//                 return NotFound("Admin not found");

//             var result = await _userManager.DeleteAsync(user);

//             if (!result.Succeeded)
//                 return BadRequest(result.Errors);

//             return Ok("Admin deleted");
//         }
//     }
// }







