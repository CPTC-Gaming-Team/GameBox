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
                name: "CreatorId",
                table: "GameModels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GameModels_CreatorId",
                table: "GameModels",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameModels_AspNetUsers_CreatorId",
                table: "GameModels",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameModels_AspNetUsers_CreatorId",
                table: "GameModels");

            migrationBuilder.DropIndex(
                name: "IX_GameModels_CreatorId",
                table: "GameModels");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "GameModels");
        }
    }
}
