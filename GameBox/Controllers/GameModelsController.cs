using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameBox.Data;
using GameBox.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GameBox.Controllers
{
    public class GameModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GameModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: GameModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameModels.ToListAsync());
        }

        // GET: GameModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _context.GameModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameModel == null)
            {
                return NotFound();
            }

            return View(gameModel);
        }

        // GET: GameModels/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] GameModel gameModel)
        {
            // Ensure the user is authenticated
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Challenge();
            }

            if (ModelState.IsValid)
            {
                // Set OwnerId to the currently logged-in user's Id (prevent overposting of OwnerId)
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Forbid();
                }

                gameModel.OwnerId = userId;

                _context.Add(gameModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameModel);
        }

        // GET: GameModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _context.GameModels.FindAsync(id);
            if (gameModel == null)
            {
                return NotFound();
            }
            return View(gameModel);
        }

        // POST: GameModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Rating")] GameModel gameModel)
        {
            if (id != gameModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameModelExists(gameModel.Id))
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
            return View(gameModel);
        }

        // GET: GameModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _context.GameModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameModel == null)
            {
                return NotFound();
            }

            return View(gameModel);
        }

        // POST: GameModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameModel = await _context.GameModels.FindAsync(id);
            if (gameModel != null)
            {
                _context.GameModels.Remove(gameModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameModelExists(int id)
        {
            return _context.GameModels.Any(e => e.Id == id);
        }
    }
}
