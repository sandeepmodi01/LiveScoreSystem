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
    
    public class ScoreCardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreCardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScoreCard
        public async Task<IActionResult> Index(int matchId)
        {
            ViewBag.MatchId = matchId;
            var applicationDbContext = _context.ScoreCard.Include(s => s.Match).Include(s => s.Player).Where(a=>a.MatchId == matchId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ScoreCard/Create
        public IActionResult Create(int matchId)
        {
            ViewBag.MatchId = matchId;
            ViewData["PlayerId"] = new SelectList(_context.Player, "Id", "Name");
            return View();
        }

        // POST: ScoreCard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScoreCardModel scoreCardModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scoreCardModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index) , new { matchId = scoreCardModel.MatchId });
            }
            ViewBag.MatchId = scoreCardModel.MatchId;
            ViewData["PlayerId"] = new SelectList(_context.Player, "Id", "Name", scoreCardModel.PlayerId);
            return View(scoreCardModel);
        }

        // GET: ScoreCard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreCardModel = await _context.ScoreCard
                .Include(s => s.Match)
                .Include(s => s.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scoreCardModel == null)
            {
                return NotFound();
            }

            return View(scoreCardModel);
        }

        // POST: ScoreCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scoreCardModel = await _context.ScoreCard.FindAsync(id);
            _context.ScoreCard.Remove(scoreCardModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) , new { matchId = scoreCardModel.MatchId });
        }

        private bool ScoreCardModelExists(int id)
        {
            return _context.ScoreCard.Any(e => e.Id == id);
        }
    }
}
