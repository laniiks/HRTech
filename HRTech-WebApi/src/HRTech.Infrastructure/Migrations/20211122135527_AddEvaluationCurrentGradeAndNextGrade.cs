using Microsoft.EntityFrameworkCore.Migrations;

namespace HRTech.Infrastructure.Migrations
{
    public partial class AddEvaluationCurrentGradeAndNextGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentGradeId",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NextGradeId",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_CurrentGradeId",
                table: "Evaluations",
                column: "CurrentGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_NextGradeId",
                table: "Evaluations",
                column: "NextGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Grades_CurrentGradeId",
                table: "Evaluations",
                column: "CurrentGradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Grades_NextGradeId",
                table: "Evaluations",
                column: "NextGradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Grades_CurrentGradeId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Grades_NextGradeId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_CurrentGradeId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_NextGradeId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CurrentGradeId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NextGradeId",
                table: "Evaluations");
        }
    }
}
