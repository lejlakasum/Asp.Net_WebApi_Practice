using dot_NET_WebApiPractice.Data.Entities;
using dot_NET_WebApiPractice.Migrations;
using dot_NET_WebApiPractice.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace dot_NET_WebApiPractice.Data
{
    public class BasketballContext : DbContext
    {
        public BasketballContext() : base("name=BasketballConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BasketballContext, Configuration>());
        }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOptional<Team>(g => g.HomeTeam)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

    }
}