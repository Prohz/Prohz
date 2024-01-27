using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class UserMembershipZipcodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMembershipCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembershipCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMembershipCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembershipCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembershipCategories_MembershipBenefitsPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipBenefitsPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMembershipZipcodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ZipCodeId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembershipZipcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMembershipZipcodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembershipZipcodes_MembershipBenefitsPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipBenefitsPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembershipZipcodes_ZipCode_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "ZipCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipCategories_CategoriesId",
                table: "UserMembershipCategories",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipCategories_PlanId",
                table: "UserMembershipCategories",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipCategories_UserId",
                table: "UserMembershipCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipZipcodes_PlanId",
                table: "UserMembershipZipcodes",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipZipcodes_UserId",
                table: "UserMembershipZipcodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipZipcodes_ZipCodeId",
                table: "UserMembershipZipcodes",
                column: "ZipCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMembershipCategories");

            migrationBuilder.DropTable(
                name: "UserMembershipZipcodes");
        }
    }
}
