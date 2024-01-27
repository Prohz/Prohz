using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class tABLErENAME : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileSubContractors_AspNetUsers_UserId",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessProfileSubContractors",
                table: "BusinessProfileSubContractors");

            migrationBuilder.RenameTable(
                name: "BusinessProfileSubContractors",
                newName: "BusinessProfileOtherContractors");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessProfileSubContractors_UserId",
                table: "BusinessProfileOtherContractors",
                newName: "IX_BusinessProfileOtherContractors_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessProfileOtherContractors",
                table: "BusinessProfileOtherContractors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileOtherContractors_AspNetUsers_UserId",
                table: "BusinessProfileOtherContractors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileOtherContractors_AspNetUsers_UserId",
                table: "BusinessProfileOtherContractors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessProfileOtherContractors",
                table: "BusinessProfileOtherContractors");

            migrationBuilder.RenameTable(
                name: "BusinessProfileOtherContractors",
                newName: "BusinessProfileSubContractors");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessProfileOtherContractors_UserId",
                table: "BusinessProfileSubContractors",
                newName: "IX_BusinessProfileSubContractors_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessProfileSubContractors",
                table: "BusinessProfileSubContractors",
                column: "Id");

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
