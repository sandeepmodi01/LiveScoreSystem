using LiveScore.Data;
using LiveScore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LiveScore.Controllers
{
  
    public class LiveScoreController : Controller
    {
        protected readonly ApplicationDbContext _context;
        public LiveScoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           return View(_context.Match.Include(a => a.TeamOne).Include(a => a.TeamTwo).ToList());
        }
        public IActionResult Teams()
        {
            return View(_context.Team.ToList());
        }
        public IActionResult Players()
        {
            List<TeamModel> Team = _context.Team.ToList();
            List<PlayerModel> Player = _context.Player.ToList();
            return View(Tuple.Create(Team, Player));
        }
  
        public IActionResult Live()
        {
            return View(_context.Match.Include(a => a.TeamOne).Include(a => a.TeamTwo).Include(a => a.WinnerTeam).Include(a => a.LoosingTeam).ToList());
        }
        public IActionResult Upcoming()
        {
            return View(_context.Match.Include(a => a.TeamOne).Include(a => a.TeamTwo).ToList());
        }
        public IActionResult Completed()
        {
            return View(_context.Match.Include(a => a.TeamOne).Include(a => a.TeamTwo).Include(a => a.WinnerTeam).Include(a => a.LoosingTeam).ToList());
        }


        //public IActionResult Scorecard()
        //{
        //    List<PlayerModel> Player = _context.Player.ToList();
        //    List<ScoreCardModel> ScoreCard = _context.ScoreCard.ToList();
        //    return View(Tuple.Create(Player, ScoreCard));
            
        //}

        public IActionResult ContactUs()
        {
            return View();
        }
    }

}
