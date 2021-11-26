using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRTech.Infrastructure.Migrations
{
    public partial class AddEvaluationInCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Evaluations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_CompanyId",
                table: "Evaluations",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Companies_CompanyId",
                table: "Evaluations",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Companies_CompanyId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_CompanyId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Evaluations");
        }
    }
}
