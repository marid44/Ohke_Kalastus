using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddUploadedByToMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "MediaFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "MediaFiles");
        }
    }
}
