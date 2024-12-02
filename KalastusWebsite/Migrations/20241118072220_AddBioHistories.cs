using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddBioHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BioHistory_UserProfiles_UserProfileId",
                table: "BioHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BioHistory",
                table: "BioHistory");

            migrationBuilder.RenameTable(
                name: "BioHistory",
                newName: "BioHistories");

            migrationBuilder.RenameIndex(
                name: "IX_BioHistory_UserProfileId",
                table: "BioHistories",
                newName: "IX_BioHistories_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BioHistories",
                table: "BioHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BioHistories_UserProfiles_UserProfileId",
                table: "BioHistories",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BioHistories_UserProfiles_UserProfileId",
                table: "BioHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BioHistories",
                table: "BioHistories");

            migrationBuilder.RenameTable(
                name: "BioHistories",
                newName: "BioHistory");

            migrationBuilder.RenameIndex(
                name: "IX_BioHistories_UserProfileId",
                table: "BioHistory",
                newName: "IX_BioHistory_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BioHistory",
                table: "BioHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BioHistory_UserProfiles_UserProfileId",
                table: "BioHistory",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
