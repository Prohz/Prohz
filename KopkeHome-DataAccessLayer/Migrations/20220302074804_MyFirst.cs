using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class MyFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddBusinessProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsInBusiness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompanyWebsite = table.Column<bool>(type: "bit", nullable: false),
                    CompanyWebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFacebookPage = table.Column<bool>(type: "bit", nullable: false),
                    FacebookPageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommercialLocation = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfEmployees = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobSiteCrews = table.Column<bool>(type: "bit", nullable: false),
                    IsPhoneCallSupport = table.Column<bool>(type: "bit", nullable: false),
                    NormalBusinessHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is24HoursPhoneAnswering = table.Column<bool>(type: "bit", nullable: false),
                    IsOfferEmergencyServices = table.Column<bool>(type: "bit", nullable: false),
                    IsBusinessOrTradeLicense = table.Column<bool>(type: "bit", nullable: false),
                    IsLiabilityInsurance = table.Column<bool>(type: "bit", nullable: false),
                    IsWorkmanCompensationInsurance = table.Column<bool>(type: "bit", nullable: false),
                    IsCash = table.Column<int>(type: "int", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MyPrIsFreeEstimatesoperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimateCharge = table.Column<bool>(type: "bit", nullable: false),
                    IsDesignServices = table.Column<bool>(type: "bit", nullable: false),
                    DesignServices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsContactedByHomeowners = table.Column<bool>(type: "bit", nullable: false),
                    IsContactedBySubcontractors = table.Column<bool>(type: "bit", nullable: false),
                    IsContactedByContractors = table.Column<bool>(type: "bit", nullable: false),
                    ServiceCallCharge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MC = table.Column<int>(type: "int", nullable: false),
                    Visa = table.Column<int>(type: "int", nullable: false),
                    AmEx = table.Column<int>(type: "int", nullable: false),
                    OtherCreditCard = table.Column<int>(type: "int", nullable: false),
                    IsPaymentApps = table.Column<int>(type: "int", nullable: false),
                    WhichApps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddBusinessProfile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddBusinessProfile");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "AspNetUsers");
        }
    }
}
