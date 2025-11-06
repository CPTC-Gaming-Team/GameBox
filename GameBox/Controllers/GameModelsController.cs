using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameBox.Data;
using GameBox.Models;

namespace GameBox.Controllers
{
    public class GameModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameModels.ToListAsync());
        }

        // GET: GameModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var gameModel = await _context.GameModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameModel == null) return NotFound();

            return View(gameModel);
        }

        // GET: GameModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Rating")] GameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameModel);
        }

        // GET: GameModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var gameModel = await _context.GameModels.FindAsync(id);
            if (gameModel == null) return NotFound();
            return View(gameModel);
        }

        // POST: GameModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Rating")] GameModel gameModel)
        {
            if (id != gameModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameModelExists(gameModel.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gameModel);
        }

        // GET: GameModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var gameModel = await _context.GameModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameModel == null) return NotFound();

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
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GameModelExists(int id)
        {
            return _context.GameModels.Any(e => e.Id == id);
        }
    }
}