using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManagementWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Events", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Organizer",
                columns: table => new
                {
                    OrganizerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizerPwd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Organizer", x => x.OrganizerID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Members", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_tbl_Members_tbl_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "tbl_Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OrganizeEvent",
                columns: table => new
                {
                    OrganizeEventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    OrganizerID = table.Column<int>(type: "int", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OrganizeEvent", x => x.OrganizeEventID);
                    table.ForeignKey(
                        name: "FK_tbl_OrganizeEvent_tbl_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "tbl_Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_OrganizeEvent_tbl_Organizer_OrganizerID",
                        column: x => x.OrganizerID,
                        principalTable: "tbl_Organizer",
                        principalColumn: "OrganizerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "tbl_Events",
                columns: new[] { "EventID", "EventType" },
                values: new object[,]
                {
                    { 1001, "Conference" },
                    { 1002, "Seminar" },
                    { 1003, "Workshop" },
                    { 1004, "Awards and Competition" }
                });

            migrationBuilder.InsertData(
                table: "tbl_Organizer",
                columns: new[] { "OrganizerID", "OrganizerPwd", "UserName" },
                values: new object[,]
                {
                    { 101, "pass1", "Organizer1" },
                    { 102, "pass2", "Organizer2" },
                    { 103, "pass3", "Organizer3" },
                    { 104, "pass4", "Organizer4" },
                    { 105, "pass5", "Organizer5" }
                });

            migrationBuilder.InsertData(
                table: "tbl_Members",
                columns: new[] { "MemberID", "BirthDate", "Email", "EventID", "MemberName" },
                values: new object[,]
                {
                    { 2010, new DateTime(1992, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aron@gmail.com", 1001, "Aron" },
                    { 2011, new DateTime(1993, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Caroline@gmail.com", 1002, "Caroline" },
                    { 2012, new DateTime(1992, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clinton@gmail.com", 1003, "Clinton" },
                    { 2013, new DateTime(1995, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cole@gmail.com", 1003, "Cole" },
                    { 2014, new DateTime(1992, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "David@gmail.com", 1003, "David" },
                    { 2015, new DateTime(1992, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "George@gmail.com", 1003, "George" },
                    { 2016, new DateTime(1990, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joan@gmail.com", 1002, "Joan" },
                    { 2017, new DateTime(1993, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Myra@gmail.com", 1001, "Myra" },
                    { 2018, new DateTime(1991, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luke@gmail.com", 1004, "Luke" },
                    { 2019, new DateTime(1991, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah@gmail.com", 1004, "Sarah" },
                    { 2020, new DateTime(1998, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jack@gmail.com", 1001, "Jack" }
                });

            migrationBuilder.InsertData(
                table: "tbl_OrganizeEvent",
                columns: new[] { "OrganizeEventID", "City", "EndDate", "EventDesc", "EventID", "EventLocation", "OrganizerID", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, "Mysore", new DateTime(2022, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Conference for 3 days", 1001, "Radison Blu Hotel", 105, 50000m, new DateTime(2022, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Bangalore", new DateTime(2022, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seminar for 1 day", 1002, "Grand Mercure", 103, 20000m, new DateTime(2022, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Mysore", new DateTime(2022, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Conference for 4 days", 1001, "Golden Landmark Resorts", 101, 24000m, new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Bangalore", new DateTime(2022, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workshop for 2 days", 1003, "Ibis Hotel", 104, 50000m, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Hyderabad", new DateTime(2022, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workshop for 5 days", 1003, "Royalton Hotel", 102, 100000m, new DateTime(2022, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Bangalore", new DateTime(2022, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Awards and Competition for 1 day", 1004, "Courtyard Marriott Hotel", 105, 25000m, new DateTime(2022, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Bangalore", new DateTime(2022, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Awards and Competition for 1 day", 1004, "ClayWorks Create", 103, 30000m, new DateTime(2022, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Hyderabad", new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Conference for 4 days", 1001, "Taj Deccan", 102, 140000m, new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Mysore", new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Conference for 2 days", 1001, "White Orchids Resort", 101, 50000m, new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Members_EventID",
                table: "tbl_Members",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrganizeEvent_EventID",
                table: "tbl_OrganizeEvent",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrganizeEvent_OrganizerID",
                table: "tbl_OrganizeEvent",
                column: "OrganizerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Members");

            migrationBuilder.DropTable(
                name: "tbl_OrganizeEvent");

            migrationBuilder.DropTable(
                name: "tbl_Events");

            migrationBuilder.DropTable(
                name: "tbl_Organizer");
        }
    }
}
