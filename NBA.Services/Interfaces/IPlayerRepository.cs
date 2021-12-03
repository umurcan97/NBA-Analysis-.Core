namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using System.Collections.Generic;
    using NBA.Services.Abstraction;

    public interface IPlayerRepository
    {
        ServiceResult GetAllPlayers();
        ServiceResult GetPlayerWithId(int Id);
        ServiceResult GetPlayerWithName(string name);
        ServiceResult GetPlayersWithTeam(Team team);
        ServiceResult AddPlayer(Players player);
        ServiceResult AddPlayerList(List<Players> players);
        ServiceResult UpdatePlayer(Players player);
        ServiceResult DeletePlayer(int id);
    }
}
