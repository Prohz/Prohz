using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class AddedIsVerfiedColumnsInBusinessDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDocumentsVerified",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropColumn(
                name: "IsDocumentsVerified",
                table: "BusinessProfile");
        }
    }
}
