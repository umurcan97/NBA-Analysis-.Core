namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using System.Collections.Generic;
    using NBA.Services.Abstraction;
    using NBA.Models.Entities;

    public interface IPlayerStatRepository
    {
        ServiceResult<List<PlayerStats>> GetAllPlayerStats();
        ServiceResult<PlayerStats> GetPlayerStatWithId(int Id);
        ServiceResult<PlayerStats> GetPlayerStatsWithPlayerIdAndGameNo(int playerId, int gameNo);
        ServiceResult<List<PlayerStats>> GetPlayerStatsWithGameNo(int gameNo);
        ServiceResult<List<PlayerStats>> GetPlayerStatsWithPlayerId(int playerId);
        ServiceResult<List<PlayerStats>> GetPlayerStatsWithTeam(Team team);
        ServiceResult AddPlayerStat(PlayerStats stats);
        ServiceResult UpdatePlayerStat(PlayerStats stats);
        ServiceResult DeletePlayerStat(int id);
    }
}
