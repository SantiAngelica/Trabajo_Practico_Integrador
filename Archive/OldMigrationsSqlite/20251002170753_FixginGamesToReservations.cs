using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixginGamesToReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_GameId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GameId",
                table: "Reservations",
                column: "GameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_GameId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GameId",
                table: "Reservations",
                column: "GameId");
        }
    }
}
