using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class AddingRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessProfileSubContractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusinessDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsInBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompanyWebsite = table.Column<bool>(type: "bit", nullable: false),
                    CompanyWebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFacebookPage = table.Column<bool>(type: "bit", nullable: false),
                    FacebookPageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialLocationContractor = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfEmployees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobSiteCrews = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPhoneCallSupport = table.Column<bool>(type: "bit", nullable: false),
                    NormalBusinessHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is24HoursPhoneAnswering = table.Column<bool>(type: "bit", nullable: false),
                    IsOfferEmergencyServices = table.Column<bool>(type: "bit", nullable: false),
                    IsBusinessOrTradeLicense = table.Column<bool>(type: "bit", nullable: false),
                    BusinessOrTradeLicenseFiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLiabilityInsurance = table.Column<bool>(type: "bit", nullable: false),
                    LiabilityInsuranceFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWorkmanCompensationInsurance = table.Column<bool>(type: "bit", nullable: false),
                    WorkmanCompensationInsuranceFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    MC = table.Column<int>(type: "int", nullable: false),
                    Visa = table.Column<int>(type: "int", nullable: false),
                    AmEx = table.Column<int>(type: "int", nullable: false),
                    OtherCreditCard = table.Column<int>(type: "int", nullable: false),
                    PersonalChecks = table.Column<bool>(type: "bit", nullable: false),
                    IsPaymentApps = table.Column<bool>(type: "bit", nullable: false),
                    WhichPaymentApps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEstimateCharge = table.Column<bool>(type: "bit", nullable: false),
                    EstimateCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDesignServices = table.Column<bool>(type: "bit", nullable: false),
                    DesignServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsContactedByHomeowners = table.Column<bool>(type: "bit", nullable: false),
                    IsContactedByContractors = table.Column<bool>(type: "bit", nullable: false),
                    ServiceCallCharge = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProfileSubContractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProfileSubContractors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfileSubContractors_UserId",
                table: "BusinessProfileSubContractors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessProfileSubContractors");
        }
    }
}
