using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopkeHome_DataAccessLayer.Migrations
{
    public partial class subscriptiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipSubscription_AspNetUsers_UserId",
                table: "MembershipSubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipSubscription_Categories_CategoriesId",
                table: "MembershipSubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipSubscription_ZipCode_ZipCodeId",
                table: "MembershipSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipSubscription",
                table: "MembershipSubscription");

            migrationBuilder.RenameTable(
                name: "MembershipSubscription",
                newName: "UsersZipcodesAndCategories");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipSubscription_ZipCodeId",
                table: "UsersZipcodesAndCategories",
                newName: "IX_UsersZipcodesAndCategories_ZipCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipSubscription_UserId",
                table: "UsersZipcodesAndCategories",
                newName: "IX_UsersZipcodesAndCategories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipSubscription_CategoriesId",
                table: "UsersZipcodesAndCategories",
                newName: "IX_UsersZipcodesAndCategories_CategoriesId");

            migrationBuilder.AddColumn<string>(
                name: "AnnuallyStripePriceId",
                table: "MembershipBenefitsPlan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MonthlyStripePriceId",
                table: "MembershipBenefitsPlan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersZipcodesAndCategories",
                table: "UsersZipcodesAndCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserMembershipSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripeCustomerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembershipSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMembershipSubscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersZipcodesAndCategories_PlanId",
                table: "UsersZipcodesAndCategories",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipSubscriptions_UserId",
                table: "UserMembershipSubscriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersZipcodesAndCategories_AspNetUsers_UserId",
                table: "UsersZipcodesAndCategories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersZipcodesAndCategories_Categories_CategoriesId",
                table: "UsersZipcodesAndCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersZipcodesAndCategories_MembershipBenefitsPlan_PlanId",
                table: "UsersZipcodesAndCategories",
                column: "PlanId",
                principalTable: "MembershipBenefitsPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersZipcodesAndCategories_ZipCode_ZipCodeId",
                table: "UsersZipcodesAndCategories",
                column: "ZipCodeId",
                principalTable: "ZipCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersZipcodesAndCategories_AspNetUsers_UserId",
                table: "UsersZipcodesAndCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersZipcodesAndCategories_Categories_CategoriesId",
                table: "UsersZipcodesAndCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersZipcodesAndCategories_MembershipBenefitsPlan_PlanId",
                table: "UsersZipcodesAndCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersZipcodesAndCategories_ZipCode_ZipCodeId",
                table: "UsersZipcodesAndCategories");

            migrationBuilder.DropTable(
                name: "UserMembershipSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersZipcodesAndCategories",
                table: "UsersZipcodesAndCategories");

            migrationBuilder.DropIndex(
                name: "IX_UsersZipcodesAndCategories_PlanId",
                table: "UsersZipcodesAndCategories");

            migrationBuilder.DropColumn(
                name: "AnnuallyStripePriceId",
                table: "MembershipBenefitsPlan");

            migrationBuilder.DropColumn(
                name: "MonthlyStripePriceId",
                table: "MembershipBenefitsPlan");

            migrationBuilder.RenameTable(
                name: "UsersZipcodesAndCategories",
                newName: "MembershipSubscription");

            migrationBuilder.RenameIndex(
                name: "IX_UsersZipcodesAndCategories_ZipCodeId",
                table: "MembershipSubscription",
                newName: "IX_MembershipSubscription_ZipCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersZipcodesAndCategories_UserId",
                table: "MembershipSubscription",
                newName: "IX_MembershipSubscription_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersZipcodesAndCategories_CategoriesId",
                table: "MembershipSubscription",
                newName: "IX_MembershipSubscription_CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipSubscription",
                table: "MembershipSubscription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipSubscription_AspNetUsers_UserId",
                table: "MembershipSubscription",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipSubscription_Categories_CategoriesId",
                table: "MembershipSubscription",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipSubscription_ZipCode_ZipCodeId",
                table: "MembershipSubscription",
                column: "ZipCodeId",
                principalTable: "ZipCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
