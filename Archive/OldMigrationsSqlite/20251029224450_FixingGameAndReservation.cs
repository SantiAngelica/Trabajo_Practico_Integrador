using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingGameAndReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Games_GameId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Games_GameId",
                table: "Reservations",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Games_GameId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Games_GameId",
                table: "Reservations",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
