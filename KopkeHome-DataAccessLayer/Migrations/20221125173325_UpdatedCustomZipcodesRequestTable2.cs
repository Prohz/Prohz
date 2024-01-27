using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class UpdatedCustomZipcodesRequestTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceMonthly",
                table: "CustomZipcodesRequest",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceYearly",
                table: "CustomZipcodesRequest",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceMonthly",
                table: "CustomZipcodesRequest");

            migrationBuilder.DropColumn(
                name: "PriceYearly",
                table: "CustomZipcodesRequest");
        }
    }
}
