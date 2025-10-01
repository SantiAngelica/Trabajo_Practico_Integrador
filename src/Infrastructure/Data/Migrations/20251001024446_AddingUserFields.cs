using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComent_Users_UserId",
                table: "UserComent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserField_Users_UserId",
                table: "UserField");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPosition_Users_UserId",
                table: "UserPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPosition",
                table: "UserPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserField",
                table: "UserField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComent",
                table: "UserComent");

            migrationBuilder.RenameTable(
                name: "UserPosition",
                newName: "UserPositions");

            migrationBuilder.RenameTable(
                name: "UserField",
                newName: "UserFields");

            migrationBuilder.RenameTable(
                name: "UserComent",
                newName: "UserComents");

            migrationBuilder.RenameIndex(
                name: "IX_UserPosition_UserId",
                table: "UserPositions",
                newName: "IX_UserPositions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserField_UserId",
                table: "UserFields",
                newName: "IX_UserFields_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserComent_UserId",
                table: "UserComents",
                newName: "IX_UserComents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPositions",
                table: "UserPositions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFields",
                table: "UserFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComents",
                table: "UserComents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComents_Users_UserId",
                table: "UserComents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFields_Users_UserId",
                table: "UserFields",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPositions_Users_UserId",
                table: "UserPositions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComents_Users_UserId",
                table: "UserComents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFields_Users_UserId",
                table: "UserFields");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPositions_Users_UserId",
                table: "UserPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPositions",
                table: "UserPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFields",
                table: "UserFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComents",
                table: "UserComents");

            migrationBuilder.RenameTable(
                name: "UserPositions",
                newName: "UserPosition");

            migrationBuilder.RenameTable(
                name: "UserFields",
                newName: "UserField");

            migrationBuilder.RenameTable(
                name: "UserComents",
                newName: "UserComent");

            migrationBuilder.RenameIndex(
                name: "IX_UserPositions_UserId",
                table: "UserPosition",
                newName: "IX_UserPosition_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFields_UserId",
                table: "UserField",
                newName: "IX_UserField_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserComents_UserId",
                table: "UserComent",
                newName: "IX_UserComent_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPosition",
                table: "UserPosition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserField",
                table: "UserField",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComent",
                table: "UserComent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComent_Users_UserId",
                table: "UserComent",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserField_Users_UserId",
                table: "UserField",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosition_Users_UserId",
                table: "UserPosition",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
