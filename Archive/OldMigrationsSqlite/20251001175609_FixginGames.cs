using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixginGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Games_GameId1",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Games_GameId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_GameId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_GameId1",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Participations");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Games",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "GamesId",
                table: "GamePlayers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GameId",
                table: "Reservations",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_GameId",
                table: "Participations",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Games_GameId",
                table: "Participations",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Participations_Games_GameId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Games_GameId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_GameId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_GameId",
                table: "Participations");

            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "Participations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "GamesId",
                table: "GamePlayers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GameId1",
                table: "Reservations",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_GameId1",
                table: "Participations",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Games_GameId1",
                table: "Participations",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Games_GameId1",
                table: "Reservations",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
