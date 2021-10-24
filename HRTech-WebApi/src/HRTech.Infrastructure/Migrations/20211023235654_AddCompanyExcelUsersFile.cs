using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRTech.Infrastructure.Migrations
{
    public partial class AddCompanyExcelUsersFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExcelFileUsersId",
                table: "Companies",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ExcelFileUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FileGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FileName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<byte[]>(type: "longblob", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelFileUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ExcelFileUsersId",
                table: "Companies",
                column: "ExcelFileUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ExcelFileUsers_ExcelFileUsersId",
                table: "Companies",
                column: "ExcelFileUsersId",
                principalTable: "ExcelFileUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ExcelFileUsers_ExcelFileUsersId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "ExcelFileUsers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ExcelFileUsersId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ExcelFileUsersId",
                table: "Companies");
        }
    }
}
