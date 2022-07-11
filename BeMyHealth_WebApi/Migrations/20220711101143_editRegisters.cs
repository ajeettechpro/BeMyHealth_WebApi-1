using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeMyHealth_WebApi.Migrations
{
    public partial class editRegisters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginDate",
                table: "Users",
                newName: "UpdatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Users",
                newName: "LastLoginDate");
        }
    }
}
