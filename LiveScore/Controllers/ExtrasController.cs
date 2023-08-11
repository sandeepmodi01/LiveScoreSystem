using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LiveScore.Data;
using LiveScore.Models;
using Microsoft.AspNetCore.Authorization;

namespace LiveScore.Controllers
{
    [Authorize]
    public class ExtrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExtrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Extras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Extras.Include(e => e.Match).Include(e => e.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Extras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extrasModel = await _context.Extras
                .Include(e => e.Match)
                .Include(e => e.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extrasModel == null)
            {
                return NotFound();
            }

            return View(extrasModel);
        }

        // GET: Extras/Create
        public IActionResult Create()
        {
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name");
            return View();
        }

        // POST: Extras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MatchId,TeamId,Run")] ExtrasModel extrasModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extrasModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "Id", extrasModel.MatchId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", extrasModel.TeamId);
            return View(extrasModel);
        }

        // GET: Extras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extrasModel = await _context.Extras.FindAsync(id);
            if (extrasModel == null)
            {
                return NotFound();
            }
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "Name", extrasModel.MatchId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", extrasModel.TeamId);
            return View(extrasModel);
        }

        // POST: Extras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MatchId,TeamId,Run")] ExtrasModel extrasModel)
        {
            if (id != extrasModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extrasModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtrasModelExists(extrasModel.Id))
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
            ViewData["MatchId"] = new SelectList(_context.Match, "Id", "Id", extrasModel.MatchId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", extrasModel.TeamId);
            return View(extrasModel);
        }

        // GET: Extras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extrasModel = await _context.Extras
                .Include(e => e.Match)
                .Include(e => e.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extrasModel == null)
            {
                return NotFound();
            }

            return View(extrasModel);
        }

        // POST: Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var extrasModel = await _context.Extras.FindAsync(id);
            _context.Extras.Remove(extrasModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtrasModelExists(int id)
        {
            return _context.Extras.Any(e => e.Id == id);
        }
    }
}
