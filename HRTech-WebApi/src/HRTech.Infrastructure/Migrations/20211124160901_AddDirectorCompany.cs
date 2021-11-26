using Microsoft.EntityFrameworkCore.Migrations;

namespace HRTech.Infrastructure.Migrations
{
    public partial class AddDirectorCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDirector",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDirector",
                table: "AspNetUsers");
        }
    }
}
