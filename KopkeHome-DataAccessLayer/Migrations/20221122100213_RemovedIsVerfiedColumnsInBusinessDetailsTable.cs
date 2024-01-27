using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class RemovedIsVerfiedColumnsInBusinessDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDocumentsVerified",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropColumn(
                name: "IsDocumentsVerified",
                table: "BusinessProfile");

            migrationBuilder.AddColumn<int>(
                name: "VerificationStatus",
                table: "BusinessProfileSubContractors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VerificationStatus",
                table: "BusinessProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDocumentsVerified",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "IsDocumentsVerified",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDocumentsVerified",
                table: "BusinessProfileSubContractors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDocumentsVerified",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
