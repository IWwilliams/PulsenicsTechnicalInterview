using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pulsenics.Data;
using Pulsenics.Models;
using System;
using System.IO;

namespace Pulsenics.Services
{
    public class FileTrackerService
    {
        private readonly string folderPath;
        private FileSystemWatcher fileWatcher;
        private readonly IServiceScopeFactory scopeFactory;
        public FileTrackerService(string folderPath, IServiceScopeFactory scopeFactory)
        {
            this.folderPath = folderPath;
            this.scopeFactory = scopeFactory;
        }

        public void StartTracking()
        {
            

                // Create and configure the FileSystemWatcher
                fileWatcher = new FileSystemWatcher(folderPath);
                fileWatcher.IncludeSubdirectories = false;

            // Subscribe to file events
            fileWatcher.Created += OnFileCreated;
            fileWatcher.Deleted += OnFileDeleted;
            fileWatcher.Changed += OnFileChanged;
            fileWatcher.Renamed += OnFileRenamed;


            // Enable the FileSystemWatcher
            fileWatcher.EnableRaisingEvents = true;
            
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            // File create event handler
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    var filePath = e.FullPath;
                    string fileName = Path.GetFileName(filePath);
                    string extension = Path.GetExtension(filePath);
                    DateTime createdDate = System.IO.File.GetCreationTime(filePath);
                    DateTime lastModifiedDate = System.IO.File.GetLastWriteTime(filePath);

                    var newFile = new Models.File
                    {
                        FileName = fileName,
                        Extension = extension,
                        CreatedDate = createdDate,
                        LastModifiedDate = lastModifiedDate
                    };

                    // Add the new File to the database
                    dbContext.Files.Add(newFile);
                    dbContext.SaveChanges();

                    // Perform additional actions as needed

                    Console.WriteLine($"File {filePath} has been created and added to the database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing the file created event: {ex.Message}");
                }
            }
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            // File deleted event handler
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); try
                {
                    var filePath = e.FullPath;
                    string fileName = Path.GetFileName(filePath);

                    // Check if the file already exists in the database
                    var existingFile = dbContext.Files.FirstOrDefault(f => f.FileName == fileName);

                    if (existingFile != null)
                    {
                        dbContext.Files.Remove(existingFile);

                        // Save the changes to the database
                        dbContext.SaveChanges();

                        Console.WriteLine($"File {fileName} has been removed from the database.");
                    }
                    else
                    {
                        Console.WriteLine($"File {fileName} does not exist in the database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing the file deleted event: {ex.Message}");
                }
            }
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();            // File changed event handler
                try
                {
                    var filePath = e.FullPath;
                    string fileName = Path.GetFileName(filePath);
                    string extension = Path.GetExtension(filePath);
                    DateTime createdDate = System.IO.File.GetCreationTime(filePath);
                    DateTime lastModifiedDate = System.IO.File.GetLastWriteTime(filePath);

                    // Check if the file already exists in the database
                    var existingFile = dbContext.Files.FirstOrDefault(f => f.FileName == fileName);

                    if (existingFile != null)
                    {
                        // Update the existing file properties
                        existingFile.Extension = extension;
                        existingFile.LastModifiedDate = lastModifiedDate;

                        // Save the changes to the database
                        dbContext.SaveChanges();

                        Console.WriteLine($"File {fileName} has been updated in the database.");
                    }
                    else
                    {
                        Console.WriteLine($"File {fileName} does not exist in the database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing the file changed event: {ex.Message}");
                }
            }
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();            // File renamed event handler
                try
                {
                    var oldFilePath = e.OldFullPath;
                    var newFilePath = e.FullPath;
                    string oldFileName = Path.GetFileName(oldFilePath);
                    string newFileName = Path.GetFileName(newFilePath);

                    // Find the file in the database using the old file name
                    var existingFile = dbContext.Files.FirstOrDefault(f => f.FileName == oldFileName);

                    if (existingFile != null)
                    {
                        // Update the file name to the new file name
                        existingFile.FileName = newFileName;

                        // Save the changes to the database
                        dbContext.SaveChanges();

                        Console.WriteLine($"File {oldFileName} has been renamed to {newFileName} in the database.");
                    }
                    else
                    {
                        Console.WriteLine($"File {oldFileName} does not exist in the database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing the file renamed event: {ex.Message}");
                }
            }
        }

        public void StopTracking()
        {
            // Disable the FileSystemWatcher and release resources
            fileWatcher.EnableRaisingEvents = false;
            fileWatcher.Dispose();
        }
    }
}