using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorColumnToComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "MediaFiles");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Comments",
                newName: "Author");

            migrationBuilder.AlterColumn<int>(
                name: "ConversationId",
                table: "Comments",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "Comments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ConversationId",
                table: "Comments",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MediaId",
                table: "Comments",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Conversations_ConversationId",
                table: "Comments",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_MediaFiles_MediaId",
                table: "Comments",
                column: "MediaId",
                principalTable: "MediaFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Conversations_ConversationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_MediaFiles_MediaId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ConversationId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MediaId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Comments",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "MediaFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ConversationId",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
