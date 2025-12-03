using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBox.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "GameModels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameModels_OwnerId",
                table: "GameModels",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameModels_AspNetUsers_OwnerId",
                table: "GameModels",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameModels_AspNetUsers_OwnerId",
                table: "GameModels");

            migrationBuilder.DropIndex(
                name: "IX_GameModels_OwnerId",
                table: "GameModels");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "GameModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
