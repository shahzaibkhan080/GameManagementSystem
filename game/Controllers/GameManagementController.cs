using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using game.Models;

namespace game.Controllers
{
    public class GameManagementController : Controller
    {
        private readonly NeondbContext _context;

        public GameManagementController(NeondbContext context)
        {
            _context = context;
        }

        // GET: GameManagement
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameManagements.ToListAsync());
        }

        // GET: GameManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameManagement = await _context.GameManagements
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (gameManagement == null)
            {
                return NotFound();
            }

            return View(gameManagement);
        }

        // GET: GameManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,GameName,Country")] GameManagement gameManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameManagement);
        }

        // GET: GameManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameManagement = await _context.GameManagements.FindAsync(id);
            if (gameManagement == null)
            {
                return NotFound();
            }
            return View(gameManagement);
        }

        // POST: GameManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,GameName,Country")] GameManagement gameManagement)
        {
            if (id != gameManagement.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameManagementExists(gameManagement.GameId))
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
            return View(gameManagement);
        }

        // GET: GameManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameManagement = await _context.GameManagements
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (gameManagement == null)
            {
                return NotFound();
            }

            return View(gameManagement);
        }

        // POST: GameManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameManagement = await _context.GameManagements.FindAsync(id);
            if (gameManagement != null)
            {
                _context.GameManagements.Remove(gameManagement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameManagementExists(int id)
        {
            return _context.GameManagements.Any(e => e.GameId == id);
        }
    }
}
