using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KopkeHome_DataAccessLayer
{
#nullable disable
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<BusinessProfileDataModel> BusinessProfile { get; set; }
        public DbSet<BusinessProfileSubContractor> BusinessProfileSubContractors { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<VerifyOTP> VerifyOTP { get; set; }
        public DbSet<ZipCode> ZipCode { get; set; }
        public DbSet<MembershipBenefits> MembershipBenefits { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<WorkGallery> WorkGallery { get; set; }
        public DbSet<MembershipBenifitsPlan> MembershipBenefitsPlan { get; set; }
        public DbSet<UserMembershipSubscriptions> UserMembershipSubscriptions { get; set; }
        public DbSet<UserMembershipCategories> UserMembershipCategories { get; set; }
        public DbSet<UserMembershipZipcodes> UserMembershipZipcodes { get; set; }
        public DbSet<ContractorsReview> ContractorsReview { get; set; }

        public DbSet<CustomZipcodesRequest> CustomZipcodesRequest { get; set; }
        public DbSet<UniqueMemberId> UniqueMemberId { get; set; }
        public DbSet<DocumentsVerificationStatus> DocumentsVerificationStatus { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<PromoVideos> PromoVideos { get; set; }
        public DbSet<ProhzReferral> ProhzReferral { get; set; }
        public DbSet<HeardAboutProhz> HeardAboutProhz { get; set; }
        public DbSet<ProhzSalesAssciates> ProhzSalesAssciates { get; set; }
        public DbSet<ProhzLegalFiles> ProhzLegalFiles { get; set; }
    }
}

