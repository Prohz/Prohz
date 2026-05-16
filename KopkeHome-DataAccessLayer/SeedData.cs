using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KopkeHome_DataAccessLayer
{
#nullable disable
    public static class SeedData
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            context.Database.EnsureCreated();

            // 1) Roles
            var roles = new[] { "Admin", "Contractor", "Homeowner", "Sales", "Support" };
            foreach (var r in roles)
            {
                if (!roleManager.Roles.Any(x => x.Name == r))
                {
                    var role = new Role { Name = r, NormalizedName = r.ToUpper(), IsActive = true };
                    await roleManager.CreateAsync(role);
                }
            }

            // 2) Users (create with UserManager so passwords are hashed correctly)
            var sampleUsers = new[] {
                new User { UserName = "admin@prohz.example", Email = "admin@prohz.example", FirstName = "System", LastName = "Admin", RoleId = 1, EmailConfirmed = true, CreatedOn = DateTime.UtcNow },
                new User { UserName = "alice.contractor@example.com", Email = "alice.contractor@example.com", FirstName = "Alice", LastName = "Smith", RoleId = 2, EmailConfirmed = true, CreatedOn = DateTime.UtcNow },
                new User { UserName = "bob.homeowner@example.com", Email = "bob.homeowner@example.com", FirstName = "Bob", LastName = "Jones", RoleId = 3, EmailConfirmed = true, CreatedOn = DateTime.UtcNow },
                new User { UserName = "carol.sales@example.com", Email = "carol.sales@example.com", FirstName = "Carol", LastName = "Davis", RoleId = 4, EmailConfirmed = true, CreatedOn = DateTime.UtcNow }
            };

            foreach (var u in sampleUsers)
            {
                if (await userManager.FindByEmailAsync(u.Email) == null)
                {
                    var result = await userManager.CreateAsync(u, "P@ssw0rd1");
                    if (result.Succeeded)
                    {
                        var roleName = roles[u.RoleId - 1];
                        await userManager.AddToRoleAsync(u, roleName);
                    }
                }
            }

            // 3) Master data: Roles already in identity, now other masters
            if (!context.State.Any())
            {
                var states = new[] {
                    new State { StateId = 1, StateName = "California", CountryId = 1, USAStateCode = "CA" },
                    new State { StateId = 2, StateName = "Texas", CountryId = 1, USAStateCode = "TX" },
                    new State { StateId = 3, StateName = "New York", CountryId = 1, USAStateCode = "NY" },
                    new State { StateId = 4, StateName = "Florida", CountryId = 1, USAStateCode = "FL" },
                    new State { StateId = 5, StateName = "Illinois", CountryId = 1, USAStateCode = "IL" },
                    new State { StateId = 6, StateName = "Pennsylvania", CountryId = 1, USAStateCode = "PA" },
                    new State { StateId = 7, StateName = "Ohio", CountryId = 1, USAStateCode = "OH" },
                    new State { StateId = 8, StateName = "Georgia", CountryId = 1, USAStateCode = "GA" },
                    new State { StateId = 9, StateName = "North Carolina", CountryId = 1, USAStateCode = "NC" },
                    new State { StateId = 10, StateName = "Michigan", CountryId = 1, USAStateCode = "MI" }
                };
                context.State.AddRange(states);
            }

            if (!context.Categories.Any())
            {
                var categories = new[] {
                    new Categories { Id = 1, Name = "Plumbing" },
                    new Categories { Id = 2, Name = "Electrical" },
                    new Categories { Id = 3, Name = "Roofing" },
                    new Categories { Id = 4, Name = "Painting" },
                    new Categories { Id = 5, Name = "HVAC" },
                    new Categories { Id = 6, Name = "Landscaping" },
                    new Categories { Id = 7, Name = "Flooring" },
                    new Categories { Id = 8, Name = "Carpentry" },
                    new Categories { Id = 9, Name = "Masonry" },
                    new Categories { Id = 10, Name = "Cleaning" }
                };
                context.Categories.AddRange(categories);
            }

            // Cities + ZipCodes minimal set
            if (!context.City.Any())
            {
                var cities = new[] {
                    new City { Id = 1, CityName = "Los Angeles", StateId = 1 },
                    new City { Id = 2, CityName = "San Francisco", StateId = 1 },
                    new City { Id = 3, CityName = "Houston", StateId = 2 },
                    new City { Id = 4, CityName = "Dallas", StateId = 2 },
                    new City { Id = 5, CityName = "New York", StateId = 3 },
                    new City { Id = 6, CityName = "Miami", StateId = 4 },
                    new City { Id = 7, CityName = "Chicago", StateId = 5 },
                    new City { Id = 8, CityName = "Philadelphia", StateId = 6 },
                    new City { Id = 9, CityName = "Columbus", StateId = 7 },
                    new City { Id = 10, CityName = "Atlanta", StateId = 8 }
                };
                context.City.AddRange(cities);
            }

            if (!context.ZipCode.Any())
            {
                var zips = new[] {
                    new ZipCode { Id = 1, CityId = 1, Zipcode = "90001" },
                    new ZipCode { Id = 2, CityId = 2, Zipcode = "94102" },
                    new ZipCode { Id = 3, CityId = 3, Zipcode = "77001" },
                    new ZipCode { Id = 4, CityId = 4, Zipcode = "75201" },
                    new ZipCode { Id = 5, CityId = 5, Zipcode = "10001" },
                    new ZipCode { Id = 6, CityId = 6, Zipcode = "33101" },
                    new ZipCode { Id = 7, CityId = 7, Zipcode = "60601" },
                    new ZipCode { Id = 8, CityId = 8, Zipcode = "19101" },
                    new ZipCode { Id = 9, CityId = 9, Zipcode = "43085" },
                    new ZipCode { Id = 10, CityId = 10, Zipcode = "30301" }
                };
                context.ZipCode.AddRange(zips);
            }

            // Membership plans
            if (!context.MembershipBenefitsPlan.Any())
            {
                var plans = new[] {
                    new MembershipBenifitsPlan { Id = 1, Title = "Basic Contractor", RoleId = 2, Categories = "1,2", ZipCodes = "1,3", PricePerMonth = 29.99, PricePerYear = 299.99, Website = true, PhoneApp = false },
                    new MembershipBenifitsPlan { Id = 2, Title = "Pro Contractor", RoleId = 2, Categories = "1,2,3", ZipCodes = "1,2,3", PricePerMonth = 59.99, PricePerYear = 599.99, Website = true, PhoneApp = true }
                };
                context.MembershipBenefitsPlan.AddRange(plans);
            }

            // Documents verification statuses
            if (!context.DocumentsVerificationStatus.Any())
            {
                context.DocumentsVerificationStatus.AddRange(new[] {
                    new DocumentsVerificationStatus { Id = 1, Status = "Pending" },
                    new DocumentsVerificationStatus { Id = 2, Status = "Verified" },
                    new DocumentsVerificationStatus { Id = 3, Status = "Rejected" }
                });
            }

            // FAQ
            if (!context.FAQ.Any())
            {
                context.FAQ.AddRange(new[] {
                    new FAQ { Id = 1, Question = "How to register?", Answer = "Click Register and fill details.", IsActive = true },
                    new FAQ { Id = 2, Question = "How to contact support?", Answer = "Use the Contact form or email support@prohz.example.", IsActive = true }
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
