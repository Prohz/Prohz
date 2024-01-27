using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class addedpersonalcheckandediteddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhichApps",
                table: "BusinessProfile");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPaymentApps",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "PersonalChecks",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WhichPaymentApps",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalChecks",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "WhichPaymentApps",
                table: "BusinessProfile");

            migrationBuilder.AlterColumn<int>(
                name: "IsPaymentApps",
                table: "BusinessProfile",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "WhichApps",
                table: "BusinessProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
