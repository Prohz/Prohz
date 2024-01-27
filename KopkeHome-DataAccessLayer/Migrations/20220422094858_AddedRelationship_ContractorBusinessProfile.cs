using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class AddedRelationship_ContractorBusinessProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileSubContractors_AspNetUsers_UserId",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProfileSubContractors_UserId",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropColumn(
                name: "CommercialLocationContractor",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "IsContactedByContractors",
                table: "BusinessProfile");

            migrationBuilder.DropColumn(
                name: "ServiceCallCharge",
                table: "BusinessProfile");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "BusinessProfile",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BusinessProfile",
                newName: "RoleId");

            migrationBuilder.AddColumn<bool>(
                name: "CommercialLocationContractor",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsContactedByContractors",
                table: "BusinessProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ServiceCallCharge",
                table: "BusinessProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfileSubContractors_UserId",
                table: "BusinessProfileSubContractors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileSubContractors_AspNetUsers_UserId",
                table: "BusinessProfileSubContractors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
