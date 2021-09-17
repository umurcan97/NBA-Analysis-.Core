namespace NBA.Services.Abstraction.Interfaces
{
    using NBA.Models;
    using System.Collections.Generic;

    public interface IPlayerRepository
    {
        ServiceResult<List<Players>> GetAllPlayers();
        ServiceResult<Players> GetPlayerWithId(int Id);
        ServiceResult<List<Players>> GetPlayersWithTeam(Team team);
        ServiceResult AddPlayer(string name, Team team, int number, string Position, int height, int weight, string country);
        ServiceResult UpdatePlayerWithId(int id, string name, Team team, int number, string Position, int height, int weight, string country);
        ServiceResult DeletePlayerWithId(int id);
    }
}
