using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddVotesToConversations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Conversations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Conversations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Conversations");
        }
    }
}
