using Microsoft.EntityFrameworkCore.Migrations;

namespace HRTech.Infrastructure.Migrations
{
    public partial class AddEvaluations1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserIdExpertSoftSkills",
                table: "Evaluations",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserIdExpertHardSkills",
                table: "Evaluations",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserIdExpertEnglishSkills",
                table: "Evaluations",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Evaluations",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ApplicationUserId",
                table: "Evaluations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ApplicationUserIdExpertEnglishSkills",
                table: "Evaluations",
                column: "ApplicationUserIdExpertEnglishSkills");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ApplicationUserIdExpertHardSkills",
                table: "Evaluations",
                column: "ApplicationUserIdExpertHardSkills");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ApplicationUserIdExpertSoftSkills",
                table: "Evaluations",
                column: "ApplicationUserIdExpertSoftSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserId",
                table: "Evaluations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserIdExpertEnglishSkills",
                table: "Evaluations",
                column: "ApplicationUserIdExpertEnglishSkills",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserIdExpertHardSkills",
                table: "Evaluations",
                column: "ApplicationUserIdExpertHardSkills",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserIdExpertSoftSkills",
                table: "Evaluations",
                column: "ApplicationUserIdExpertSoftSkills",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserIdExpertEnglishSkills",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserIdExpertHardSkills",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_AspNetUsers_ApplicationUserIdExpertSoftSkills",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ApplicationUserId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ApplicationUserIdExpertEnglishSkills",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ApplicationUserIdExpertHardSkills",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ApplicationUserIdExpertSoftSkills",
                table: "Evaluations");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserIdExpertSoftSkills",
                table: "Evaluations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserIdExpertHardSkills",
                table: "Evaluations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserIdExpertEnglishSkills",
                table: "Evaluations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Evaluations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
