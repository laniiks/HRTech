using Microsoft.EntityFrameworkCore.Migrations;

namespace HRTech.Infrastructure.Migrations
{
    public partial class AddPhotoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Images",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ApplicationUserId",
                table: "Images",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_ApplicationUserId",
                table: "Images",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_ApplicationUserId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ApplicationUserId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Images");
        }
    }
}
