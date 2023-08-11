using LiveScore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveScore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<ExtrasModel> Extras { get; set; }
        public DbSet<MatchModel> Match { get; set; }
        public DbSet<PlayerModel> Player { get; set; }
        public DbSet<ScoreCardModel> ScoreCard { get; set; }
        public DbSet<TeamModel> Team { get; set; }

    }
}
