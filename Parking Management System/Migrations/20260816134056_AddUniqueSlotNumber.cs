using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueSlotNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SlotNumber",
                table: "ParkingSlots",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlots_SlotNumber",
                table: "ParkingSlots",
                column: "SlotNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParkingSlots_SlotNumber",
                table: "ParkingSlots");

            migrationBuilder.AlterColumn<string>(
                name: "SlotNumber",
                table: "ParkingSlots",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
