using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeMyHealth_WebApi.Migrations
{
    public partial class editRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Users",
                newName: "LastLoginDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginDate",
                table: "Users",
                newName: "UpdatedDate");
        }
    }
}
