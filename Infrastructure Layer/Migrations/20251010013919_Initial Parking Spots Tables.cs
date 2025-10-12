using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Parking_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialParkingSpotsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpot_ParkingAreas_ParkingAreaId",
                table: "ParkingSpot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot");

            migrationBuilder.RenameTable(
                name: "ParkingSpot",
                newName: "ParkingSpots");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingSpot_ParkingAreaId",
                table: "ParkingSpots",
                newName: "IX_ParkingSpots_ParkingAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_ParkingAreas_ParkingAreaId",
                table: "ParkingSpots",
                column: "ParkingAreaId",
                principalTable: "ParkingAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_ParkingAreas_ParkingAreaId",
                table: "ParkingSpots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots");

            migrationBuilder.RenameTable(
                name: "ParkingSpots",
                newName: "ParkingSpot");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingSpots_ParkingAreaId",
                table: "ParkingSpot",
                newName: "IX_ParkingSpot_ParkingAreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpot_ParkingAreas_ParkingAreaId",
                table: "ParkingSpot",
                column: "ParkingAreaId",
                principalTable: "ParkingAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
