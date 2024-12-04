using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFishImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fishes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/ahven.jpg");

            migrationBuilder.UpdateData(
                table: "Fishes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/hauki.jpg");

            migrationBuilder.UpdateData(
                table: "Fishes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/siika.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fishes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "wwwroot/images/ahven.jpg");

            migrationBuilder.UpdateData(
                table: "Fishes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/4/44/Hecht.jpg");

            migrationBuilder.UpdateData(
                table: "Fishes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/a/a7/Coregonus_lavaretus.jpg");
        }
    }
}
