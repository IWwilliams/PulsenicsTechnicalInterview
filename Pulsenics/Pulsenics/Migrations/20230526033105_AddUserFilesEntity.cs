using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pulsenics.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFilesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFile_Files_FileId",
                table: "UserFile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFile_Users_UserId",
                table: "UserFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFile",
                table: "UserFile");

            migrationBuilder.RenameTable(
                name: "UserFile",
                newName: "UserFiles");

            migrationBuilder.RenameIndex(
                name: "IX_UserFile_FileId",
                table: "UserFiles",
                newName: "IX_UserFiles_FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFiles",
                table: "UserFiles",
                columns: new[] { "UserId", "FileId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_Files_FileId",
                table: "UserFiles",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_Users_UserId",
                table: "UserFiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_Files_FileId",
                table: "UserFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_Users_UserId",
                table: "UserFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFiles",
                table: "UserFiles");

            migrationBuilder.RenameTable(
                name: "UserFiles",
                newName: "UserFile");

            migrationBuilder.RenameIndex(
                name: "IX_UserFiles_FileId",
                table: "UserFile",
                newName: "IX_UserFile_FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFile",
                table: "UserFile",
                columns: new[] { "UserId", "FileId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFile_Files_FileId",
                table: "UserFile",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFile_Users_UserId",
                table: "UserFile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
