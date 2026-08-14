using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberOffice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UniqueMemberId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStatus = table.Column<int>(type: "int", nullable: true),
                    WorkStatusModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDocumentsVerified = table.Column<bool>(type: "bit", nullable: false),
                    HeardAboutProhzFrom = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomMembershipPlanRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NumberOfZipcodes = table.Column<int>(type: "int", nullable: false),
                    Descrption = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NumberOfCategories = table.Column<int>(type: "int", nullable: false),
                    MobileApp = table.Column<bool>(type: "bit", nullable: false),
                    WebApp = table.Column<bool>(type: "bit", nullable: false),
                    IsYearly = table.Column<bool>(type: "bit", nullable: false),
                    PriceMonthly = table.Column<double>(type: "float", nullable: false),
                    PriceYearly = table.Column<double>(type: "float", nullable: false),
                    StripePriceMonthly = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    StripePriceYearly = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsPlanCreated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomMembershipPlanRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsVerificationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsVerificationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeardAboutProhz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeardFrom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeardAboutProhz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProhzLegalFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProhzLegalFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProhzReferral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SalesPersonName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    IsRegistrationComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProhzReferral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProhzSalesAssciates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinedMemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinedMemberMemberId = table.Column<long>(type: "bigint", nullable: false),
                    JoinedMemberEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRegistred = table.Column<bool>(type: "bit", nullable: false),
                    JoinedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProhzSalesAssciates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoVideos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    USAStateCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "UniqueMemberId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueMemberId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerifyOTP",
                columns: table => new
                {
                    OtpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyOTP", x => x.OtpId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipBenefitsPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCodes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PricePerMonth = table.Column<decimal>(type: "money", nullable: false),
                    PricePerYear = table.Column<decimal>(type: "money", nullable: false),
                    Website = table.Column<bool>(type: "bit", nullable: false),
                    PhoneApp = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyStripePriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnuallyStripePriceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipBenefitsPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipBenefitsPlan_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusinessDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    YearsInBusiness = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsCompanyWebsite = table.Column<bool>(type: "bit", nullable: false),
                    CompanyWebsiteURL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsFacebookPage = table.Column<bool>(type: "bit", nullable: false),
                    FacebookPageURL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommercialLocation = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfEmployees = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobSiteCrews = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPhoneCallSupport = table.Column<bool>(type: "bit", nullable: false),
                    NormalBusinessHours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Is24HoursPhoneAnswering = table.Column<bool>(type: "bit", nullable: false),
                    IsOfferEmergencyServices = table.Column<bool>(type: "bit", nullable: false),
                    IsBusinessOrTradeLicense = table.Column<bool>(type: "bit", nullable: false),
                    IsLiabilityInsurance = table.Column<bool>(type: "bit", nullable: false),
                    IsWorkmanCompensationInsurance = table.Column<bool>(type: "bit", nullable: false),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    IsEstimateCharge = table.Column<bool>(type: "bit", nullable: false),
                    EstimateCharge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDesignServices = table.Column<bool>(type: "bit", nullable: false),
                    DesignServices = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsContactedByHomeowners = table.Column<bool>(type: "bit", nullable: false),
                    IsContactedBySubcontractors = table.Column<bool>(type: "bit", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WorkmanCompensationInsuranceFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LiabilityInsuranceFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BusinessOrTradeLicenseFiles = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MC = table.Column<int>(type: "int", nullable: false),
                    Visa = table.Column<int>(type: "int", nullable: false),
                    AmEx = table.Column<int>(type: "int", nullable: false),
                    OtherCreditCard = table.Column<int>(type: "int", nullable: false),
                    IsPaymentApps = table.Column<bool>(type: "bit", nullable: false),
                    PersonalChecks = table.Column<bool>(type: "bit", nullable: false),
                    WhichPaymentApps = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProfileOtherContractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusinessDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    YearsInBusiness = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsCompanyWebsite = table.Column<bool>(type: "bit", nullable: false),
                    CompanyWebsiteURL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsFacebookPage = table.Column<bool>(type: "bit", nullable: false),
                    FacebookPageURL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommercialLocationContractor = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfEmployees = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobSiteCrews = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPhoneCallSupport = table.Column<bool>(type: "bit", nullable: false),
                    NormalBusinessHours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Is24HoursPhoneAnswering = table.Column<bool>(type: "bit", nullable: false),
                    IsOfferEmergencyServices = table.Column<bool>(type: "bit", nullable: false),
                    IsBusinessOrTradeLicense = table.Column<bool>(type: "bit", nullable: false),
                    BusinessOrTradeLicenseFiles = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsLiabilityInsurance = table.Column<bool>(type: "bit", nullable: false),
                    LiabilityInsuranceFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsWorkmanCompensationInsurance = table.Column<bool>(type: "bit", nullable: false),
                    WorkmanCompensationInsuranceFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    MC = table.Column<int>(type: "int", nullable: false),
                    Visa = table.Column<int>(type: "int", nullable: false),
                    AmEx = table.Column<int>(type: "int", nullable: false),
                    OtherCreditCard = table.Column<int>(type: "int", nullable: false),
                    PersonalChecks = table.Column<bool>(type: "bit", nullable: false),
                    IsPaymentApps = table.Column<bool>(type: "bit", nullable: false),
                    WhichPaymentApps = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsEstimateCharge = table.Column<bool>(type: "bit", nullable: false),
                    EstimateCharge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDesignServices = table.Column<bool>(type: "bit", nullable: false),
                    DesignServices = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsContactedByHomeowners = table.Column<bool>(type: "bit", nullable: false),
                    IsContactedByContractors = table.Column<bool>(type: "bit", nullable: false),
                    ServiceCallCharge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProfileOtherContractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProfileOtherContractors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorsReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorsReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorsReview_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMembershipSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    StripeStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    StripeCustomerID = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    StripePriceId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InvoiceUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancelledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpgradedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DowngradedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Extensions = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembershipSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMembershipSubscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkGallery_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMembershipCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembershipCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMembershipCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembershipCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembershipCategories_MembershipBenefitsPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipBenefitsPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMembershipZipcodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ZipCodeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembershipZipcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMembershipZipcodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembershipZipcodes_MembershipBenefitsPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipBenefitsPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZipCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZipCode_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersZipcodesAndCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ZipCodeId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersZipcodesAndCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersZipcodesAndCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersZipcodesAndCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersZipcodesAndCategories_MembershipBenefitsPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipBenefitsPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersZipcodesAndCategories_ZipCode_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "ZipCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfile_UserId",
                table: "BusinessProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfileOtherContractors_UserId",
                table: "BusinessProfileOtherContractors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorsReview_UserId",
                table: "ContractorsReview",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipBenefitsPlan_RoleId",
                table: "MembershipBenefitsPlan",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipCategories_CategoriesId",
                table: "UserMembershipCategories",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipCategories_PlanId",
                table: "UserMembershipCategories",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipCategories_UserId",
                table: "UserMembershipCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipSubscriptions_UserId",
                table: "UserMembershipSubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipZipcodes_PlanId",
                table: "UserMembershipZipcodes",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipZipcodes_UserId",
                table: "UserMembershipZipcodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersZipcodesAndCategories_CategoriesId",
                table: "UsersZipcodesAndCategories",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersZipcodesAndCategories_PlanId",
                table: "UsersZipcodesAndCategories",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersZipcodesAndCategories_UserId",
                table: "UsersZipcodesAndCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersZipcodesAndCategories_ZipCodeId",
                table: "UsersZipcodesAndCategories",
                column: "ZipCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGallery_UserId",
                table: "WorkGallery",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ZipCode_CityId",
                table: "ZipCode",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BusinessProfile");

            migrationBuilder.DropTable(
                name: "BusinessProfileOtherContractors");

            migrationBuilder.DropTable(
                name: "ContractorsReview");

            migrationBuilder.DropTable(
                name: "CustomMembershipPlanRequest");

            migrationBuilder.DropTable(
                name: "DocumentsVerificationStatus");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "HeardAboutProhz");

            migrationBuilder.DropTable(
                name: "ProhzLegalFiles");

            migrationBuilder.DropTable(
                name: "ProhzReferral");

            migrationBuilder.DropTable(
                name: "ProhzSalesAssciates");

            migrationBuilder.DropTable(
                name: "PromoVideos");

            migrationBuilder.DropTable(
                name: "UniqueMemberId");

            migrationBuilder.DropTable(
                name: "UserMembershipCategories");

            migrationBuilder.DropTable(
                name: "UserMembershipSubscriptions");

            migrationBuilder.DropTable(
                name: "UserMembershipZipcodes");

            migrationBuilder.DropTable(
                name: "UsersZipcodesAndCategories");

            migrationBuilder.DropTable(
                name: "VerifyOTP");

            migrationBuilder.DropTable(
                name: "WorkGallery");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "MembershipBenefitsPlan");

            migrationBuilder.DropTable(
                name: "ZipCode");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
