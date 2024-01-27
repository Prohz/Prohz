using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class UpdatedCustomZipcodesRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descrption",
                table: "CustomZipcodesRequest",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "MobileApp",
                table: "CustomZipcodesRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfCategories",
                table: "CustomZipcodesRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StripePriceMonthly",
                table: "CustomZipcodesRequest",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripePriceYearly",
                table: "CustomZipcodesRequest",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "WebApp",
                table: "CustomZipcodesRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileApp",
                table: "CustomZipcodesRequest");

            migrationBuilder.DropColumn(
                name: "NumberOfCategories",
                table: "CustomZipcodesRequest");

            migrationBuilder.DropColumn(
                name: "StripePriceMonthly",
                table: "CustomZipcodesRequest");

            migrationBuilder.DropColumn(
                name: "StripePriceYearly",
                table: "CustomZipcodesRequest");

            migrationBuilder.DropColumn(
                name: "WebApp",
                table: "CustomZipcodesRequest");

            migrationBuilder.AlterColumn<string>(
                name: "Descrption",
                table: "CustomZipcodesRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
