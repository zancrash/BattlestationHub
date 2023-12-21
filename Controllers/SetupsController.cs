using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BattlestationHub.Data;
using BattlestationHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BattlestationHub.Controllers
{
    public class SetupsController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;


        public SetupsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        // GET: Setups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Battlestation.ToListAsync());
        }

        // GET: Jokes/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Jokes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Battlestation.Where( j => j.SetupName.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Setups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setup = await _context.Battlestation
                .Include(s => s.Favourites)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setup == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            ViewData["IsSetupSaved"] = setup.Favourites.Any(f => f.IsSaved(user.Id, setup.Id));

            return View(setup);
        }

        // GET: Setups/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Setups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,SetupName,SetupDesc,SetupImgFile")] Setup setup)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the current user's ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                setup.UserId = userId;

                // Generate unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString("yymmssfff") + "_" + setup.SetupImgFile.FileName;

                // Get wwwroot path
                string webRootPath = _webHostEnvironment.WebRootPath;


                // Ensure 'images' directory exists
                string imagesDirectory = Path.Combine(webRootPath, "images");
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                // Create filepath
                string filePath = Path.Combine(imagesDirectory, uniqueFileName);

                // Save image to the file system
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await setup.SetupImgFile.CopyToAsync(fileStream);
                }

                //setup.ImgPath = filePath;
                setup.ImgPath = Path.Combine("images", uniqueFileName);

                // Insert Record
                _context.Add(setup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             return View(setup);
        }

        public async Task<IActionResult> SaveSetup(int id)
        {
            var setup = await _context.Battlestation
                    .Include(s => s.Favourites)
                    .FirstOrDefaultAsync(s => s.Id == id);

            if (setup == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            // Check if the user has already favorited the setup
            if (!setup.Favourites.Any(f => f.UserId == user.Id))
            {
                // Add the favorite
                setup.Favourites.Add(new Favourite { UserId = user.Id });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> UnsaveSetup(int id)
        {
            var setup = await _context.Battlestation
                    .Include(s => s.Favourites)
                    .FirstOrDefaultAsync(s => s.Id == id);

            if (setup == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            // Remove the favorite
            var favourite = setup.Favourites.FirstOrDefault(f => f.UserId == user.Id);
            if (favourite != null)
            {
                setup.Favourites.Remove(favourite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Setups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setup = await _context.Battlestation.FindAsync(id);
            if (setup == null)
            {
                return NotFound();
            }
            return View(setup);
        }

        // POST: Setups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SetupName,SetupDesc")] Setup setup)
        {
            if (id != setup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetupExists(setup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(setup);
        }

        // GET: Setups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setup = await _context.Battlestation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setup == null)
            {
                return NotFound();
            }

            return View(setup);
        }

        // POST: Setups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setup = await _context.Battlestation.FindAsync(id);
            if (setup != null)
            {
                _context.Battlestation.Remove(setup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetupExists(int id)
        {
            return _context.Battlestation.Any(e => e.Id == id);
        }
    }
}
