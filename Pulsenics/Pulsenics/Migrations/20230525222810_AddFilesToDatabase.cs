using Microsoft.EntityFrameworkCore.Migrations;
using Pulsenics.Data;
#nullable disable

namespace Pulsenics.Migrations
{
    /// <inheritdoc />
    public partial class AddFilesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string _folderPath = folderPath.folderPathString; // Replace with the actual folder path

            var files = System.IO.Directory.GetFiles(_folderPath);

            foreach (var filePath in files)
            { 
                string fileName = System.IO.Path.GetFileName(filePath);
                string extension = System.IO.Path.GetExtension(filePath);
                DateTime createdDate = System.IO.File.GetCreationTime(filePath);
                DateTime lastModifiedDate = System.IO.File.GetLastWriteTime(filePath);

                migrationBuilder.InsertData(
                    table: "Files",
                    columns: new[] { "FileName", "Extension", "CreatedDate", "LastModifiedDate" },
                    values: new object[] { fileName, extension, createdDate, lastModifiedDate });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
