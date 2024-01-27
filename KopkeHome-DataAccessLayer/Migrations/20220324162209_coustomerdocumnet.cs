using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class coustomerdocumnet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessProfile",
                table: "BusinessProfile");

            migrationBuilder.RenameTable(
                name: "BusinessProfile",
                newName: "BusinessProfile");

            migrationBuilder.AlterColumn<string>(
                name: "JobSiteCrews",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCash",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "CommercialLocationContractor",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "BusinessProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessProfile",
                table: "BusinessProfile",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessProfile",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "CommercialLocationContractor",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "BusinessProfile");

            migrationBuilder.RenameTable(
                name: "BusinessProfile",
                newName: "BusinessProfile");

            migrationBuilder.AlterColumn<bool>(
                name: "JobSiteCrews",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IsCash",
                table: "BusinessProfile",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessProfile",
                table: "BusinessProfile",
                column: "Id");
        }
    }
}
