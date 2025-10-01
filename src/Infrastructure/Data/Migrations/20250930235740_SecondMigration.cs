using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameUser_Games_GamesId",
                table: "GameUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GameUser_Users_UsersId",
                table: "GameUser");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameUser",
                table: "GameUser");

            migrationBuilder.DropColumn(
                name: "IdField",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IdGame",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IdGame",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "IdUserCreator",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "GameUser",
                newName: "GamePlayers");

            migrationBuilder.RenameColumn(
                name: "IdSchedule",
                table: "Reservations",
                newName: "GameId1");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Participations",
                newName: "GameId1");

            migrationBuilder.RenameIndex(
                name: "IX_GameUser_UsersId",
                table: "GamePlayers",
                newName: "IX_GamePlayers_UsersId");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Schedules",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Schedules",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "ScheduleId",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "FieldId",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Propertys",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Propertys",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Participations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Participations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Fields",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Fields",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "UsersId",
                table: "GamePlayers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlayers",
                table: "GamePlayers",
                columns: new[] { "GamesId", "UsersId" });

            migrationBuilder.CreateTable(
                name: "UserComent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserComent_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserField",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Field = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserField_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPosition",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPosition_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GameId1",
                table: "Reservations",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_GameId1",
                table: "Participations",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreatorId",
                table: "Games",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComent_UserId",
                table: "UserComent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserField_UserId",
                table: "UserField",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPosition_UserId",
                table: "UserPosition",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayers_Games_GamesId",
                table: "GamePlayers",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayers_Users_UsersId",
                table: "GamePlayers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_CreatorId",
                table: "Games",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayers_Games_GamesId",
                table: "GamePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayers_Users_UsersId",
                table: "GamePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_CreatorId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Games_GameId1",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Games_GameId1",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "UserComent");

            migrationBuilder.DropTable(
                name: "UserField");

            migrationBuilder.DropTable(
                name: "UserPosition");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_GameId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_GameId1",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreatorId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlayers",
                table: "GamePlayers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "GamePlayers",
                newName: "GameUser");

            migrationBuilder.RenameColumn(
                name: "GameId1",
                table: "Reservations",
                newName: "IdSchedule");

            migrationBuilder.RenameColumn(
                name: "GameId1",
                table: "Participations",
                newName: "IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayers_UsersId",
                table: "GameUser",
                newName: "IX_GameUser_UsersId");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Schedules",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Schedules",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "IdField",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdGame",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Propertys",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Propertys",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Participations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Participations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "IdGame",
                table: "Participations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUserCreator",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Fields",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Fields",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "GameUser",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameUser",
                table: "GameUser",
                columns: new[] { "GamesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GameId",
                table: "Reservations",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_GameId",
                table: "Participations",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameUser_Games_GamesId",
                table: "GameUser",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameUser_Users_UsersId",
                table: "GameUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
