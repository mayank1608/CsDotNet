using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoodReadsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class GoodReadsDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Authors",
                columns: table => new
                {
                    Author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Authors", x => x.Author_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Books",
                columns: table => new
                {
                    Book_isbn = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Book_edition = table.Column<int>(type: "int", nullable: false),
                    Author_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Books", x => x.Book_isbn);
                    table.ForeignKey(
                        name: "FK_tbl_Books_tbl_Authors_Author_id",
                        column: x => x.Author_id,
                        principalTable: "tbl_Authors",
                        principalColumn: "Author_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Reviews",
                columns: table => new
                {
                    Review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_isbn = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Reviews", x => x.Review_id);
                    table.ForeignKey(
                        name: "FK_tbl_Reviews_tbl_Books_Book_isbn",
                        column: x => x.Book_isbn,
                        principalTable: "tbl_Books",
                        principalColumn: "Book_isbn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tbl_Authors",
                columns: new[] { "Author_id", "Author_name" },
                values: new object[,]
                {
                    { 1, "Robert Martin" },
                    { 2, "Martin Fowler" },
                    { 3, "Andrew Hunt" }
                });

            migrationBuilder.InsertData(
                table: "tbl_Books",
                columns: new[] { "Book_isbn", "Author_id", "Book_edition", "Title" },
                values: new object[,]
                {
                    { 1001, 1, 1, "Clean Code" },
                    { 1002, 1, 1, "Clean Architecture" },
                    { 1003, 2, 2, "Refactoring" },
                    { 1004, 3, 2, "The Pragmatic Programmer" }
                });

            migrationBuilder.InsertData(
                table: "tbl_Reviews",
                columns: new[] { "Review_id", "Book_isbn", "Rating", "ReviewText" },
                values: new object[,]
                {
                    { 1, 1001, 5, "Excellent book for developers" },
                    { 2, 1001, 4, "Very informative" },
                    { 3, 1003, 5, "Must read for legacy code" },
                    { 4, 1004, 4, "Good practical examples" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Books_Author_id",
                table: "tbl_Books",
                column: "Author_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Reviews_Book_isbn",
                table: "tbl_Reviews",
                column: "Book_isbn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Reviews");

            migrationBuilder.DropTable(
                name: "tbl_Books");

            migrationBuilder.DropTable(
                name: "tbl_Authors");
        }
    }
}
