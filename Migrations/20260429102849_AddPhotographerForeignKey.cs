using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtAuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotographerForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AppUsers_CreatedBy",
                table: "Jobs");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PhotographerId",
                table: "Jobs",
                column: "PhotographerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AppUsers_CreatedBy",
                table: "Jobs",
                column: "CreatedBy",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AppUsers_PhotographerId",
                table: "Jobs",
                column: "PhotographerId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AppUsers_CreatedBy",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AppUsers_PhotographerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_PhotographerId",
                table: "Jobs");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AppUsers_CreatedBy",
                table: "Jobs",
                column: "CreatedBy",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
