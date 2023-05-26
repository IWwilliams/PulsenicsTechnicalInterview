using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pulsenics.Data;
using Pulsenics.Models;

namespace Pulsenics.Controllers
{
    public class FileController : Controller
    {
        private readonly AppDbContext _context;

        public FileController(AppDbContext context)
        {
            _context = context;
        }
  

        public async Task<IActionResult> Index(string? searchTerm)
        {
            if (searchTerm is null)
            {
                    return _context.Files != null ?
                                             View(await _context.Files.ToListAsync()) :
                                             Problem("Entity set 'AppDbContext.Files'  is null.");
             }
            
            IQueryable<Models.File> query = _context.Files;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(f => f.FileName.Contains(searchTerm));
            }

            return View(await query.ToListAsync());
        }

        // GET: File/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var @file = await _context.Files
                 .Include(f => f.UserFiles)
                  .ThenInclude(uf => uf.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@file == null)
            {
                return NotFound();
            }

            return View(@file);
        }

        public async Task<IActionResult> Assign(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var @file = await _context.Files
                .Include(f => f.UserFiles)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@file == null)
            {
                return NotFound();
            }

            var users = _context.Users.ToList();
            ViewBag.Users = users;

            return View(@file);
        }

        // POST: File/Assign/fileId & userId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(int id, int userId)
        {
           
            // Retrieve the file based on the provided ID
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            // Retrieve the user based on the provided ID
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Assign the user to the file
            var userFile = new UserFile
            {
                FileId = file.Id,
                UserId = user.Id
            };

            var existingUserFile = _context.UserFiles.FirstOrDefault(uf => (uf.FileId == userFile.FileId & uf.UserId == userFile.UserId));
            if (existingUserFile==null)
            {
                _context.UserFiles.Add(userFile);
                await _context.SaveChangesAsync();

                var users = _context.Users.ToList();
                ViewBag.Users = users;

                // Redirect back to the Assign function with the file ID
                return RedirectToAction("Assign", new { id = file.Id });
            }
            else
            {
                return RedirectToAction("Assign", new { id = file.Id });
            }
           
        }

        // GET: File/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: File/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,Extension,CreatedDate,LastModifiedDate")] Models.File @file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@file);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@file);
        }


        // GET: File/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var @file = await _context.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@file == null)
            {
                return NotFound();
            }

            return View(@file);
        }

        // POST: User/DeleteAssignment/userId & fileId
        public async Task<IActionResult> DeleteAssignment(int userId, int fileId)
        {
            var userFile = await _context.UserFiles
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FileId == fileId);

            if (userFile == null)
            {
                return NotFound();
            }

            _context.UserFiles.Remove(userFile);
            await _context.SaveChangesAsync();

            return RedirectToAction("Assign", new { id = fileId });
        }

        // POST: File/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Files == null)
            {
                return Problem("Entity set 'AppDbContext.Files'  is null.");
            }
            var @file = await _context.Files.FindAsync(id);
            if (@file != null)
            {
                _context.Files.Remove(@file);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
          return (_context.Files?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
