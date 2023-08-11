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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace LiveScore.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PlayerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Player.Include(p => p.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.Player
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerModel == null)
            {
                return NotFound();
            }

            return View(playerModel);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name");
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerModel playerModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if(file != null && file.Length > 0)
                {
                    fileName = file.FileName;
                    var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                    file.CopyTo(new FileStream(path, FileMode.Create));
                }
                playerModel.FileName = fileName;
                _context.Add(playerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", playerModel.TeamId);
            return View(playerModel);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.Player.FindAsync(id);
            if (playerModel == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", playerModel.TeamId);
            return View(playerModel);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsBatsman,BattingStyle,BowlingStyle,TeamId")] PlayerModel playerModel, IFormFile file)
        {
            if (id != playerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if (file != null && file.Length > 0)
                    {
                        fileName = file.FileName;
                        var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                        file.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    playerModel.FileName = fileName;
                    _context.Update(playerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerModelExists(playerModel.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", playerModel.TeamId);
            return View(playerModel);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.Player
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerModel == null)
            {
                return NotFound();
            }

            return View(playerModel);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerModel = await _context.Player.FindAsync(id);
            _context.Player.Remove(playerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerModelExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
    }
}
