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
        ServiceResult<List<PlayerStats20_21>> GetAllPlayerStats20_21();
        ServiceResult<PlayerStats20_21> GetPlayerStatWithId20_21(int Id);
        ServiceResult<PlayerStats20_21> GetPlayerStatsWithPlayerIdAndGameNo20_21(int playerId, int gameNo);
        ServiceResult<List<PlayerStats20_21>> GetPlayerStatsWithGameNo20_21(int gameNo);
        ServiceResult<List<PlayerStats20_21>> GetPlayerStatsWithPlayerId20_21(int playerId);
        ServiceResult<List<PlayerStats20_21>> GetPlayerStatsWithTeam20_21(Team team);
        ServiceResult AddPlayerStat20_21(PlayerStats20_21 stats);
        ServiceResult UpdatePlayerStat20_21(PlayerStats20_21 stats);
        ServiceResult DeletePlayerStat20_21(int id);
        ServiceResult<List<PlayerStats19_20>> GetAllPlayerStats19_20();
        ServiceResult<PlayerStats19_20> GetPlayerStatWithId19_20(int Id);
        ServiceResult<PlayerStats19_20> GetPlayerStatsWithPlayerIdAndGameNo19_20(int playerId, int gameNo);
        ServiceResult<List<PlayerStats19_20>> GetPlayerStatsWithGameNo19_20(int gameNo);
        ServiceResult<List<PlayerStats19_20>> GetPlayerStatsWithPlayerId19_20(int playerId);
        ServiceResult<List<PlayerStats19_20>> GetPlayerStatsWithTeam19_20(Team team);
        ServiceResult AddPlayerStat19_20(PlayerStats19_20 stats);
        ServiceResult UpdatePlayerStat19_20(PlayerStats19_20 stats);
        ServiceResult DeletePlayerStat19_20(int id);
    }
}
