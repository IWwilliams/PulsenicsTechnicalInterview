using Pulsenics.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pulsenics.Migrations;
using System;
using Pulsenics.Services;

//Set folder path in Data/FolderPath.cs to desired folder
string folderPathString = folderPath.folderPathString;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Setting up the database as a MySql database which I have on my mac
//You must go into appsettings.json and change the connection string 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Creating a singleton function to track the files while the program is running and to update the database accordingly
builder.Services.AddSingleton<FileTrackerService>(sp =>
{
    var folderPath = folderPathString;
    return new FileTrackerService(folderPath, sp.GetRequiredService<IServiceScopeFactory>());
});

var app = builder.Build();

// Executing custom migration logic
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

    // Ensuring the database is created
    dbContext.Database.EnsureCreated();

    // Applying migrations to the database
    dbContext.Database.Migrate();

    //Applying a custom migration on application start-up to compare current folder structure state with database state from previous program instantiation
    CustomMigration.ExecuteMigration(folderPathString,serviceProvider);
}

// Retrieving the FileTrackerService from the service provider
var fileTrackerService = app.Services.GetRequiredService<FileTrackerService>();

// Starting to track the files
fileTrackerService.StartTracking();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Updated routing to allow for multiple features to be passed in API strings
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=File}/{action=Index}/{id1?}/{id2?}");

app.Run();