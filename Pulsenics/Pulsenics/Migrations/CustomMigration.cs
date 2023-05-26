using System;
using Pulsenics.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;

//This migration file compares the database current state with the folder path's files states. 
namespace Pulsenics.Migrations
{
	public class CustomMigration
	{
		public CustomMigration()
		{
		}
        public static void ExecuteMigration(string folderPath, IServiceProvider serviceProvider)
        {
            // Get the DbContext instance from the service provider
            using (var dbContext = serviceProvider.GetRequiredService<AppDbContext>())
            {
                var filesInFolder = Directory.GetFiles(folderPath, "*", SearchOption.TopDirectoryOnly);
                var filesNamesInFolder = Array.ConvertAll(filesInFolder, fileName=>
                {
                    return System.IO.Path.GetFileName(fileName);
                   
                });

                var filesInDatabase = dbContext.Files.Select(f => f.FileName).ToList();
                var filesToRemove = filesInDatabase.Except(filesNamesInFolder);

                //Removing files from database that are no longer in the folder location
                var dbFilesToRemove = dbContext.Files.Where(f => filesToRemove.Contains(f.FileName)).ToList();

                dbContext.Files.RemoveRange(dbFilesToRemove);

                // Update existing file records based on filename match
                foreach (var filePath in filesInFolder)
                {
                    string fileName = Path.GetFileName(filePath);

                    var existingFile = dbContext.Files.FirstOrDefault(f => f.FileName == fileName);
                    if (existingFile != null)
                    {
                        existingFile.CreatedDate = File.GetCreationTime(filePath);
                        existingFile.LastModifiedDate = File.GetLastWriteTime(filePath);
                    }
                    else
                    {
                        // Create a new file record if it doesn't already exist
                        string extension = Path.GetExtension(filePath);
                        DateTime createdDate = File.GetCreationTime(filePath);
                        DateTime lastModifiedDate = File.GetLastWriteTime(filePath);

                        var newFile = new Models.File
                        {
                            FileName = fileName,
                            Extension = extension,
                            CreatedDate = createdDate,
                            LastModifiedDate = lastModifiedDate
                        };

                        dbContext.Files.Add(newFile);
                    }
                }

                // Save the changes to the database
                dbContext.SaveChanges();
            }
        }

    }
}
