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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LiveScore.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Team
        public IActionResult Index()
        {
            return View(_context.Team.ToList());
        }

        // GET: Team/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var teamModel = _context.Team.FirstOrDefault(m => m.Id == id);
            if (teamModel == null)
            {
                return NotFound();
            }

            return View(teamModel);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamModel teamModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (file != null && file.Length > 0)
                {
                    fileName = file.FileName;
                    var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                    file.CopyTo(new FileStream(path, FileMode.Create));
                }
                teamModel.FileName = fileName;
                _context.Add(teamModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Team, "Id", "Name", teamModel.Id);
            return View(teamModel);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamModel = await _context.Team.FindAsync(id);
            if (teamModel == null)
            {
                return NotFound();
            }
            return View(teamModel);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CoachName")] TeamModel teamModel, IFormFile file)
        {
            if (id != teamModel.Id)
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
                    teamModel.FileName = fileName;
                    _context.Update(teamModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamModelExists(teamModel.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", teamModel.Id);
            return View(teamModel);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamModel = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamModel == null)
            {
                return NotFound();
            }

            return View(teamModel);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var teamModel = await _context.Team.FindAsync(id);
                _context.Team.Remove(teamModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool TeamModelExists(int id)
        {
            return _context.Team.Any(e => e.Id == id);
        }
    }
}
