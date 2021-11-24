using Microsoft.EntityFrameworkCore.Migrations;

namespace HRTech.Infrastructure.Migrations
{
    public partial class AddSuccessEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnglishSkillSuccess",
                table: "Evaluations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HardSkillSuccess",
                table: "Evaluations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftSkillSuccess",
                table: "Evaluations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishSkillSuccess",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "HardSkillSuccess",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SoftSkillSuccess",
                table: "Evaluations");
        }
    }
}
