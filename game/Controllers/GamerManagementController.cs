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
    public class GamerManagementController : Controller
    {
        private readonly NeondbContext _context;

        public GamerManagementController(NeondbContext context)
        {
            _context = context;
        }

        // GET: GamerManagement
        public async Task<IActionResult> Index()
        {
            return View(await _context.GamerManagements.ToListAsync());
        }

        // GET: GamerManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamerManagement = await _context.GamerManagements
                .FirstOrDefaultAsync(m => m.GamerId == id);
            if (gamerManagement == null)
            {
                return NotFound();
            }

            return View(gamerManagement);
        }

        // GET: GamerManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamerManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GamerId,GamerName,Country")] GamerManagement gamerManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamerManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamerManagement);
        }

        // GET: GamerManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamerManagement = await _context.GamerManagements.FindAsync(id);
            if (gamerManagement == null)
            {
                return NotFound();
            }
            return View(gamerManagement);
        }

        // POST: GamerManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GamerId,GamerName,Country")] GamerManagement gamerManagement)
        {
            if (id != gamerManagement.GamerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamerManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamerManagementExists(gamerManagement.GamerId))
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
            return View(gamerManagement);
        }

        // GET: GamerManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamerManagement = await _context.GamerManagements
                .FirstOrDefaultAsync(m => m.GamerId == id);
            if (gamerManagement == null)
            {
                return NotFound();
            }

            return View(gamerManagement);
        }

        // POST: GamerManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamerManagement = await _context.GamerManagements.FindAsync(id);
            if (gamerManagement != null)
            {
                _context.GamerManagements.Remove(gamerManagement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamerManagementExists(int id)
        {
            return _context.GamerManagements.Any(e => e.GamerId == id);
        }
    }
}
