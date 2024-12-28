using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class MediaVoteSystemRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_MediaFiles_MediaId",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "MediaVotes");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_MediaId_UserId",
                table: "MediaVotes",
                newName: "IX_MediaVotes_MediaId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaVotes",
                table: "MediaVotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaVotes_MediaFiles_MediaId",
                table: "MediaVotes",
                column: "MediaId",
                principalTable: "MediaFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaVotes_MediaFiles_MediaId",
                table: "MediaVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaVotes",
                table: "MediaVotes");

            migrationBuilder.RenameTable(
                name: "MediaVotes",
                newName: "Votes");

            migrationBuilder.RenameIndex(
                name: "IX_MediaVotes_MediaId_UserId",
                table: "Votes",
                newName: "IX_Votes_MediaId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_MediaFiles_MediaId",
                table: "Votes",
                column: "MediaId",
                principalTable: "MediaFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
