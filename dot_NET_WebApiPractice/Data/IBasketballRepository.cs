using dot_NET_WebApiPractice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dot_NET_WebApiPractice.Data
{
    public interface IBasketballRepository
    {
        //General
        Task<bool> SaveChangesAsync();

        //Teams
        void AddTeam(Team team);
        void DeleteTeam(Team team);
        Task<Team[]> GetAllTeamsAsync(bool includePlayers = false);
        Task<Team> GetTeamByIdAsync(int id);
        Task<Team> GetTeamByMonikerAsync(string moniker, bool includePlayers = false);

        //Players
        void AddPlayer(Player player);
        void DeletePlayer(Player player);
        Task<Player[]> GetAllPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player[]> GetAllPlayersByTeamAsync(string moniker);
        Task<Player> GetPlayerByTeamAsync(string moniker, int id);

        //Competitions
        void AddCompetition(Competition competition);
        void DeleteCompetition(Competition competition);
        Task<Competition[]> GetAllCompetitionsAsync();
        Task<Competition> GetCompetitionByIdAsync(int id);
/*
        //Games
        void AddGame(Game game);
        void DeleteGame(Game game);
        Task<Game[]> GetAllGames();
        Task<Game[]> GetAllHomeGamesByTeamAsync(string moniker);
        Task<Game[]> GetAllGuestGamesByTeamAsync(string moniker);
        Task<Game[]> GetAllGamesByDateAsync(DateTime date);
        Task<Game> GetGameByIdAsync(int id);

*/

    }
}
