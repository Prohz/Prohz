using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfileSubContractors_UserId",
                table: "BusinessProfileSubContractors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfile_UserId",
                table: "BusinessProfile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfile_AspNetUsers_UserId",
                table: "BusinessProfile",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfileSubContractors_AspNetUsers_UserId",
                table: "BusinessProfileSubContractors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfile_AspNetUsers_UserId",
                table: "BusinessProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfileSubContractors_AspNetUsers_UserId",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProfileSubContractors_UserId",
                table: "BusinessProfileSubContractors");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProfile_UserId",
                table: "BusinessProfile");
        }
    }
}
