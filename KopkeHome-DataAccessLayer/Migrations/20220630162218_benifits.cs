using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class benifits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BenefitName",
                table: "MembershipBenefitsPlan");

            migrationBuilder.RenameColumn(
                name: "Silver",
                table: "MembershipBenefitsPlan",
                newName: "ZipCodes");

            migrationBuilder.RenameColumn(
                name: "Gold",
                table: "MembershipBenefitsPlan",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Bronze",
                table: "MembershipBenefitsPlan",
                newName: "Categories");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerMonth",
                table: "MembershipBenefitsPlan",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerYear",
                table: "MembershipBenefitsPlan",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerMonth",
                table: "MembershipBenefitsPlan");

            migrationBuilder.DropColumn(
                name: "PricePerYear",
                table: "MembershipBenefitsPlan");

            migrationBuilder.RenameColumn(
                name: "ZipCodes",
                table: "MembershipBenefitsPlan",
                newName: "Silver");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "MembershipBenefitsPlan",
                newName: "Gold");

            migrationBuilder.RenameColumn(
                name: "Categories",
                table: "MembershipBenefitsPlan",
                newName: "Bronze");

            migrationBuilder.AddColumn<string>(
                name: "BenefitName",
                table: "MembershipBenefitsPlan",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
