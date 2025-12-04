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
    /// <summary>
    /// MVC controller responsible for CRUD operations on <see cref="GameModel"/> entities.
    /// Contains actions for listing, viewing details, creating, editing and deleting games.
    /// </summary>
    public class GameModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModelsController"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        /// <param name="userManager">User manager used to obtain information about the current user.</param>
        public GameModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// GET: GameModels
        /// Returns a view with all game models.
        /// </summary>
        /// <returns>A task that resolves to an <see cref="IActionResult"/> rendering the index view.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameModels.ToListAsync());
        }

        /// <summary>
        /// GET: GameModels/Details/{id}
        /// Shows details for a single game model.
        /// </summary>
        /// <param name="id">The id of the game to display details for.</param>
        /// <returns>NotFound if id is null or the game does not exist; otherwise the details view.</returns>
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

        /// <summary>
        /// GET: GameModels/Create
        /// Returns the form to create a new game. Requires authentication.
        /// </summary>
        /// <returns>The create view.</returns>
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: GameModels/Create
        /// Creates a new game and assigns the currently authenticated user as the owner.
        /// Prevents overposting by binding only allowed properties.
        /// Requires a valid antiforgery token and authentication.
        /// </summary>
        /// <param name="gameModel">The incoming model bound from the form.</param>
        /// <returns>Redirects to Index on success; otherwise returns the create view with validation errors.</returns>
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

        /// <summary>
        /// GET: GameModels/Edit/{id}
        /// Returns the edit form for the specified game.
        /// </summary>
        /// <param name="id">The id of the game to edit.</param>
        /// <returns>NotFound if id is null or the game does not exist; otherwise the edit view.</returns>
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

        /// <summary>
        /// POST: GameModels/Edit/{id}
        /// Updates an existing game. Protects against overposting by binding allowed properties only.
        /// </summary>
        /// <param name="id">The id of the game being edited.</param>
        /// <param name="gameModel">The model values submitted from the form.</param>
        /// <returns>Redirects to Index on success; otherwise returns the edit view with validation errors.</returns>
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

        /// <summary>
        /// GET: GameModels/Delete/{id}
        /// Shows a confirmation view to delete the specified game.
        /// </summary>
        /// <param name="id">The id of the game to delete.</param>
        /// <returns>NotFound if id is null or the game does not exist; otherwise the delete confirmation view.</returns>
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

        /// <summary>
        /// POST: GameModels/Delete/{id}
        /// Deletes the specified game after confirmation.
        /// </summary>
        /// <param name="id">The id of the game to delete.</param>
        /// <returns>Redirects to the index view after deletion.</returns>
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

        /// <summary>
        /// Checks whether a game exists in the database.
        /// </summary>
        /// <param name="id">The id of the game to check.</param>
        /// <returns>True if the game exists; otherwise false.</returns>
        private bool GameModelExists(int id)
        {
            return _context.GameModels.Any(e => e.Id == id);
        }
    }
}
