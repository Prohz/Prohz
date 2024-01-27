using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class AddedFilesFieldsIntoBusinessProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyPrIsFreeEstimatesoperty",
                table: "BusinessProfile");

            migrationBuilder.AlterColumn<string>(
                name: "EstimateCharge",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "BusinessOrTradeLicenseFiles",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEstimateCharge",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LiabilityInsuranceFile",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkmanCompensationInsuranceFile",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessOrTradeLicenseFiles",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "IsEstimateCharge",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "LiabilityInsuranceFile",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "WorkmanCompensationInsuranceFile",
                table: "BusinessProfile");

            migrationBuilder.AlterColumn<bool>(
                name: "EstimateCharge",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyPrIsFreeEstimatesoperty",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
