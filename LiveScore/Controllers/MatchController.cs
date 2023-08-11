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
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Match
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Match.Include(m => m.LoosingTeam).Include(m => m.TeamOne).Include(m => m.TeamTwo).Include(m => m.WinnerTeam);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Match/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchModel = await _context.Match
                .Include(m => m.LoosingTeam)
                .Include(m => m.TeamOne)
                .Include(m => m.TeamTwo)
                .Include(m => m.WinnerTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchModel == null)
            {
                return NotFound();
            }

            return View(matchModel);
        }

        // GET: Match/Create
        public IActionResult Create()
        {
            ViewData["LoosingTeamId"] = new SelectList(_context.Team, "Id", "Name");
            ViewData["TeamOneId"] = new SelectList(_context.Team, "Id", "Name");
            ViewData["TeamTwoId"] = new SelectList(_context.Team, "Id", "Name");
            ViewData["WinnerTeamId"] = new SelectList(_context.Team, "Id", "Name");
            return View();
        }

        // POST: Match/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchModel matchModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoosingTeamId"] = new SelectList(_context.Team, "Id", "Name", matchModel.LoosingTeamId);
            ViewData["TeamOneId"] = new SelectList(_context.Team, "Id", "Name", matchModel.TeamOneId);
            ViewData["TeamTwoId"] = new SelectList(_context.Team, "Id", "Name", matchModel.TeamTwoId);
            ViewData["WinnerTeamId"] = new SelectList(_context.Team, "Id", "Name", matchModel.WinnerTeamId);
            return View(matchModel);
        }

        // GET: Match/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchModel = await _context.Match.FindAsync(id);
            if (matchModel == null)
            {
                return NotFound();
            }
            ViewData["LoosingTeamId"] = new SelectList(_context.Team, "Id", "Name", matchModel.LoosingTeamId);
            ViewData["TeamOneId"] = new SelectList(_context.Team, "Id", "Name", matchModel.TeamOneId);
            ViewData["TeamTwoId"] = new SelectList(_context.Team, "Id", "Name", matchModel.TeamTwoId);
            ViewData["WinnerTeamId"] = new SelectList(_context.Team, "Id", "Name", matchModel.WinnerTeamId);
            return View(matchModel);
        }

        // POST: Match/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MatchModel matchModel)
        {
            if (id != matchModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchModelExists(matchModel.Id))
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
            ViewData["LoosingTeamId"] = new SelectList(_context.Team, "Id", "Name", matchModel.LoosingTeamId);
            ViewData["TeamOneId"] = new SelectList(_context.Team, "Id", "Name", matchModel.TeamOneId);
            ViewData["TeamTwoId"] = new SelectList(_context.Team, "Id", "Name", matchModel.TeamTwoId);
            ViewData["WinnerTeamId"] = new SelectList(_context.Team, "Id", "Name", matchModel.WinnerTeamId);
            return View(matchModel);
        }

        // GET: Match/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchModel = await _context.Match
                .Include(m => m.LoosingTeam)
                .Include(m => m.TeamOne)
                .Include(m => m.TeamTwo)
                .Include(m => m.WinnerTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchModel == null)
            {
                return NotFound();
            }

            return View(matchModel);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchModel = await _context.Match.FindAsync(id);
            _context.Match.Remove(matchModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchModelExists(int id)
        {
            return _context.Match.Any(e => e.Id == id);
        }
    }
}
