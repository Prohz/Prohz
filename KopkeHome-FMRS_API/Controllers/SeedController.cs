using KopkeHome_DataAccessLayer;
using KopkeHome_ModelLayer.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace KopkeHome_FMRS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // In-memory cache for dependency handling
        private List<State> _statesCache = new();
        private List<Role> _rolesCache = new();

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===================== SEED ALL =====================
        [HttpGet("all")]
        public IActionResult SeedAll()
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                SeedRoles();
                SeedStates();
                SeedCities();
                SeedCategories();
                SeedMembershipPlans();
                SeedUniqueMemberIds();

                _context.SaveChanges();
                transaction.Commit();

                return Ok("Database seeded successfully from scratch.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCode(500, "Seeding failed: " + ex.Message);
            }
        }

        // ===================== RESET ONLY SEEDED TABLES =====================
        [HttpDelete("reset-seed")]
        public IActionResult ResetSeed()
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.UniqueMemberId.RemoveRange(_context.UniqueMemberId);
                _context.MembershipBenefitsPlan.RemoveRange(_context.MembershipBenefitsPlan);
                _context.City.RemoveRange(_context.City);
                _context.State.RemoveRange(_context.State);
                _context.Categories.RemoveRange(_context.Categories);
                _context.Set<Role>().RemoveRange(_context.Set<Role>());

                _context.SaveChanges();
                transaction.Commit();

                return Ok("Seed data deleted successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCode(500, ex.Message);
            }
        }

        // ===================== ROLES =====================
        private void SeedRoles()
        {
            if (_context.Set<Role>().Any())
                return;

            _rolesCache = new List<Role>
            {
                new Role { IsActive = true, Name = "Contractor", NormalizedName = "CONTRACTOR" },
                new Role { IsActive = true, Name = "Sub-Contractor", NormalizedName = "SUB-CONTRACTOR" },
                new Role { IsActive = true, Name = "Independent Contractor", NormalizedName = "INDEPENDENT CONTRACTOR" },
                new Role { IsActive = true, Name = "Home Owner", NormalizedName = "HOME OWNER" },
                new Role { IsActive = true, Name = "Admin", NormalizedName = "ADMIN" }
            };

            _context.Set<Role>().AddRange(_rolesCache);
        }

        // ===================== STATES =====================
        private void SeedStates()
        {
            if (_context.State.Any())
                return;

            _statesCache = new List<State>
            {
                new State { StateName = "Alaska", CountryId = 2, USAStateCode = "49" },
                new State { StateName = "California", CountryId = 2, USAStateCode = "31" },
                new State { StateName = "Florida", CountryId = 2, USAStateCode = "27" },
                new State { StateName = "Kentucky", CountryId = 2, USAStateCode = "15" },
                new State { StateName = "Alabama", CountryId = 2, USAStateCode = "22" },
                new State { StateName = "Arizona", CountryId = 2, USAStateCode = "48" },
                new State { StateName = "Arkansas", CountryId = 2, USAStateCode = "25" },
                new State { StateName = "Colorado", CountryId = 2, USAStateCode = "38" },
                new State { StateName = "Connecticut", CountryId = 2, USAStateCode = "05" },
                new State { StateName = "Delaware", CountryId = 2, USAStateCode = "01" },
                new State { StateName = "Georgia", CountryId = 2, USAStateCode = "04" },
                new State { StateName = "Hawaii", CountryId = 2, USAStateCode = null }
            };

            _context.State.AddRange(_statesCache);
        }

        // ===================== CITIES =====================
        private void SeedCities()
        {
            if (_context.City.Any())
                return;

            var alaska = _statesCache.First(x => x.StateName == "Alaska");
            var arizona = _statesCache.First(x => x.StateName == "Arizona");

            _context.City.AddRange(
                new City { CityName = "Anchorage", State = alaska },
                new City { CityName = "Jber", State = alaska },
                new City { CityName = "Ajo", State = arizona },
                new City { CityName = "Alpine", State = arizona },
                new City { CityName = "Amado", State = arizona }
            );
        }

        // ===================== CATEGORIES =====================
        private void SeedCategories()
        {
            if (_context.Categories.Any())
                return;

            _context.Categories.AddRange(
                new Categories { Name = "Air Duct Cleaning" },
                new Categories { Name = "Alarm/Security Companies" },
                new Categories { Name = "Animal Control" },
                new Categories { Name = "Appliance Installations" },
                new Categories { Name = "Appliance Repairs" }
            );
        }

        // ===================== MEMBERSHIP PLANS =====================
        private void SeedMembershipPlans()
        {
            if (_context.MembershipBenefitsPlan.Any())
                return;

            var contractorRole = _rolesCache.FirstOrDefault(x => x.Name == "Contractor")
                ?? _context.Set<Role>().FirstOrDefault(x => x.Name == "Contractor");

            if (contractorRole == null)
                throw new Exception("Roles must be seeded before plans.");

            _context.MembershipBenefitsPlan.AddRange(
                new MembershipBenifitsPlan
                {
                    RoleId = contractorRole.Id,
                    Title = "Bronze",
                    Categories = "1",
                    ZipCodes = "10",
                    PricePerMonth = 0,
                    PricePerYear = 0,
                    PhoneApp = true,
                    Website = true
                },
                new MembershipBenifitsPlan
                {
                    RoleId = contractorRole.Id,
                    Title = "Silver",
                    Categories = "3",
                    ZipCodes = "30",
                    PricePerMonth = 150,
                    PricePerYear = 300,
                    PhoneApp = true,
                    Website = true
                },
                new MembershipBenifitsPlan
                {
                    RoleId = contractorRole.Id,
                    Title = "Gold",
                    Categories = "10",
                    ZipCodes = "100",
                    PricePerMonth = 500,
                    PricePerYear = 1000,
                    PhoneApp = true,
                    Website = true
                }
            );
        }

        // ===================== UNIQUE MEMBER IDS =====================
        private void SeedUniqueMemberIds()
        {
            if (_context.UniqueMemberId.Any())
                return;

            _context.UniqueMemberId.AddRange(
                new UniqueMemberId { MemberId = 1 },
                new UniqueMemberId { MemberId = 2 }
            );
        }
    }
}