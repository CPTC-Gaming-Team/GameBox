using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBox.Migrations
{
    /// <inheritdoc />
    public partial class RenameUploaderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameModels_AspNetUsers_UploaderID",
                table: "GameModels");

            migrationBuilder.RenameColumn(
                name: "UploaderID",
                table: "GameModels",
                newName: "UploaderId");

            migrationBuilder.RenameIndex(
                name: "IX_GameModels_UploaderID",
                table: "GameModels",
                newName: "IX_GameModels_UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameModels_AspNetUsers_UploaderId",
                table: "GameModels",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameModels_AspNetUsers_UploaderId",
                table: "GameModels");

            migrationBuilder.RenameColumn(
                name: "UploaderId",
                table: "GameModels",
                newName: "UploaderID");

            migrationBuilder.RenameIndex(
                name: "IX_GameModels_UploaderId",
                table: "GameModels",
                newName: "IX_GameModels_UploaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameModels_AspNetUsers_UploaderID",
                table: "GameModels",
                column: "UploaderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
