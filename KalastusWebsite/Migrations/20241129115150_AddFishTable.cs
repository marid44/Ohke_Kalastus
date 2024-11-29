using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KalastusWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddFishTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FinnishName = table.Column<string>(type: "TEXT", nullable: false),
                    EnglishName = table.Column<string>(type: "TEXT", nullable: false),
                    Habitat = table.Column<string>(type: "TEXT", nullable: false),
                    HabitatFI = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionFI = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionEN = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Fishes",
                columns: new[] { "Id", "DescriptionEN", "DescriptionFI", "EnglishName", "FinnishName", "Habitat", "HabitatFI", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Perch lives in clean freshwater lakes.", "Ahven elää järvissä, joissa on puhdasta vettä.", "Perch", "Ahven", "Freshwater", "Makea vesi", "wwwroot/images/ahven.jpg" },
                    { 2, "Pike is a well-known predatory fish in Finland.", "Hauki on tunnettu petokala Suomessa.", "Pike", "Hauki", "Freshwater", "Makea vesi", "https://upload.wikimedia.org/wikipedia/commons/4/44/Hecht.jpg" },
                    { 3, "Whitefish lives in the Baltic Sea and some lakes.", "Siika elää Itämeressä ja joissakin järvissä.", "Whitefish", "Siika", "Brackish/Sea", "Murtovesi / Meri", "https://upload.wikimedia.org/wikipedia/commons/a/a7/Coregonus_lavaretus.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fishes");
        }
    }
}
