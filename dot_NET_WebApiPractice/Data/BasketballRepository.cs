using dot_NET_WebApiPractice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace dot_NET_WebApiPractice.Data
{
    public class BasketballRepository : IBasketballRepository
    {

        private readonly BasketballContext _context;

        public BasketballRepository(BasketballContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {           
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Teams

        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
        }

        public void DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
        }

        public async Task<Team[]> GetAllTeamsAsync(bool includePlayers=false)
        {
            IQueryable<Team> query = _context.Teams;
            if(includePlayers)
            {
                query = _context.Teams.Include(t => t.Players);
            }
            return await query.ToArrayAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            IQueryable<Team> query = _context.Teams.Where(t => t.TeamId == id).Include(t => t.Players);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Team> GetTeamByMonikerAsync(string moniker, bool includePlayers = false)
        {
            IQueryable<Team> query = _context.Teams.Where(t => t.Moniker == moniker);
            if (includePlayers)
            {
                query = _context.Teams.Include(t => t.Players);
            }
            return await query.FirstOrDefaultAsync();
        }


        //Competitions

        public void AddCompetition(Competition competition)
        {
            _context.Competitions.Add(competition);
        }

        public void DeleteCompetition(Competition competition)
        {
            _context.Competitions.Remove(competition);
        }

        //public async Task<Competition[]> GetAllCompetitionsAsync()
        public async Task<Competition[]> GetAllCompetitionsAsync()
        {
            IQueryable<Competition> query = _context.Competitions.Include(c => c.Games);
            return await query.ToArrayAsync();
            
        }

        public async Task<Competition> GetCompetitionByIdAsync(int id)
        {
            IQueryable<Competition> query = _context.Competitions.Where(c => c.CompetitionId == id).Include(c => c.Games);
            return await query.FirstOrDefaultAsync();
        }

        //Players

        public void AddPlayer(Player player)
        {
            _context.Players.Add(player);
        }

        public void DeletePlayer(Player player)
        {
            _context.Players.Remove(player);
        }

        public async Task<Player[]> GetAllPlayersAsync()
        {
            IQueryable<Player> query = _context.Players;
            return await query.ToArrayAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            IQueryable<Player> query = _context.Players.Where(p => p.PlayerId==id).Include(p => p.Team);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Player[]> GetAllPlayersByTeamAsync(string moniker)
        {
            IQueryable<Player> query = _context.Players.Where(ply => ply.Team.Moniker == moniker);
            return await query.ToArrayAsync();
        }

        public async Task<Player> GetPlayerByTeamAsync(string moniker, int id)
        {
            IQueryable<Player> query = _context.Players.Where(ply => ply.Team.Moniker == moniker && ply.PlayerId==id );
            return await query.FirstOrDefaultAsync();
        }
    }
}