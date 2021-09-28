namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using System.Collections.Generic;
    using NBA.Services.Abstraction;

    public interface IPlayerRepository
    {
        ServiceResult<List<Players>> GetAllPlayers();
        ServiceResult<Players> GetPlayerWithId(int Id);
        ServiceResult<Players> GetPlayerWithName(string name);
        ServiceResult<List<Players>> GetPlayersWithTeam(Team team);
        ServiceResult AddPlayer(Players player);
        ServiceResult AddPlayerList(List<Players> players);
        ServiceResult UpdatePlayer(Players player);
        ServiceResult DeletePlayer(int id);
    }
}
