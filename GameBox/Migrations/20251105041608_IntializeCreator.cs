using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBox.Migrations
{
    /// <inheritdoc />
    public partial class IntializeCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "GameModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GameModels_OwnerId",
                table: "GameModels",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameModels_AspNetUsers_OwnerId",
                table: "GameModels",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "GameModels");
        }
    }
}
