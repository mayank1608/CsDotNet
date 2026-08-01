using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CHK_Booking_Status",
                table: "Bookings",
                sql: "[Status] IN ('Confirmed', 'Pending', 'Canceled')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Booking_Status",
                table: "Bookings");
        }
    }
}
