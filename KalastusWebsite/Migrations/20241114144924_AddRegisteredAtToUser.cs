using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisteredAtToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "Users",
                newName: "RegisteredAt");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_UploadedBy",
                table: "MediaFiles",
                column: "UploadedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_Users_UploadedBy",
                table: "MediaFiles",
                column: "UploadedBy",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_Users_UploadedBy",
                table: "MediaFiles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_MediaFiles_UploadedBy",
                table: "MediaFiles");

            migrationBuilder.RenameColumn(
                name: "RegisteredAt",
                table: "Users",
                newName: "JoinDate");
        }
    }
}
