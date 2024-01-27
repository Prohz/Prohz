using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class removeFkFromUMZipcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMembershipZipcodes_ZipCode_ZipCodeId",
                table: "UserMembershipZipcodes");

            migrationBuilder.DropIndex(
                name: "IX_UserMembershipZipcodes_ZipCodeId",
                table: "UserMembershipZipcodes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipZipcodes_ZipCodeId",
                table: "UserMembershipZipcodes",
                column: "ZipCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMembershipZipcodes_ZipCode_ZipCodeId",
                table: "UserMembershipZipcodes",
                column: "ZipCodeId",
                principalTable: "ZipCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
