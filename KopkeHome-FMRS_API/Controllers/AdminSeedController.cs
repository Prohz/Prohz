using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KopkeHome_ModelLayer.DataModel;
using System;
using System.Threading.Tasks;

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

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("WORKING");
        }

        [HttpGet("admin")]
        public async Task<IActionResult> SeedAdmin()
        {
            string email = "saqib@gmail.com";
            string password = "12345678";
            string roleName = "Admin";

            try
            {
                // 1. Ensure role exists
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var roleCreate = await _roleManager.CreateAsync(new Role
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(),
                        IsActive = true
                    });

                    if (!roleCreate.Succeeded)
                        return BadRequest(roleCreate.Errors);
                }

                // 2. Remove existing user (clean seed)
                var existingUser = await _userManager.FindByEmailAsync(email);
                if (existingUser != null)
                {
                    var deleteResult = await _userManager.DeleteAsync(existingUser);
                    if (!deleteResult.Succeeded)
                        return BadRequest(deleteResult.Errors);
                }

                // 3. Create NEW Identity user (IMPORTANT)
                var user = new User
                {
                    UserName = email,
                    Email = email,

                    EmailConfirmed = true,   // IMPORTANT for login flows
                    PhoneNumberConfirmed = true,

                    FirstName = "Saqib",
                    LastName = "Asghar",

                    PhoneNumber = "3000000000",
                    PhoneNumberOffice = "3000000000",

                    BusinessName = "Seed Business",
                    BusinessAddress = "Seed Address",
                    City = "Lahore",
                    State = "Punjab",
                    ZipCode = "54000",

                    RoleId = 10, // Admin role
                    IsEmailVerified = true,
                    IsDocumentsVerified = true,

                    WorkStatus = 0,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                };

                // 4. CREATE USER (this hashes password correctly)
                var createResult = await _userManager.CreateAsync(user, password);

                if (!createResult.Succeeded)
                    return BadRequest(createResult.Errors);

                // 5. Assign role
                var roleResult = await _userManager.AddToRoleAsync(user, roleName);

                if (!roleResult.Succeeded)
                    return BadRequest(roleResult.Errors);

                return Ok(new
                {
                    message = "Admin seeded successfully",
                    email,
                    password
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("delete-admin")]
        public async Task<IActionResult> DeleteAdmin()
        {
            string email = "saqib@gmail.com";

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("Admin not found");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

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







