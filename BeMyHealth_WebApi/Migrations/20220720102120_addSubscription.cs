using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeMyHealth_WebApi.Migrations
{
    public partial class addSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTbl",
                table: "SubscriptionTbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomDietTbl",
                table: "CustomDietTbl");

            migrationBuilder.RenameTable(
                name: "SubscriptionTbl",
                newName: "CustomSubscriptions");

            migrationBuilder.RenameTable(
                name: "CustomDietTbl",
                newName: "CustomDietPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomSubscriptions",
                table: "CustomSubscriptions",
                column: "SubscriptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomDietPlans",
                table: "CustomDietPlans",
                column: "SerialNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomSubscriptions",
                table: "CustomSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomDietPlans",
                table: "CustomDietPlans");

            migrationBuilder.RenameTable(
                name: "CustomSubscriptions",
                newName: "SubscriptionTbl");

            migrationBuilder.RenameTable(
                name: "CustomDietPlans",
                newName: "CustomDietTbl");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTbl",
                table: "SubscriptionTbl",
                column: "SubscriptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomDietTbl",
                table: "CustomDietTbl",
                column: "SerialNo");
        }
    }
}
