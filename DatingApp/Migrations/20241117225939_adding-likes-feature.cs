using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.Migrations
{
    /// <inheritdoc />
    public partial class addinglikesfeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActive",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    SourceUserId = table.Column<int>(type: "integer", nullable: false),
                    TragetUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => new { x.SourceUserId, x.TragetUserId });
                    table.ForeignKey(
                        name: "FK_likes_users_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_users_TragetUserId",
                        column: x => x.TragetUserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_likes_TragetUserId",
                table: "likes",
                column: "TragetUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "LookingFor",
                table: "users",
                newName: "lookingFor");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "users",
                newName: "BOD");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "photoUrl");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActive",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "User_email",
                table: "users",
                type: "text",
                nullable: true);
        }
    }
}
