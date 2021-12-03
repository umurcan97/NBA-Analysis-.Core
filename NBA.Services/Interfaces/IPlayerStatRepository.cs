namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using System.Collections.Generic;
    using NBA.Services.Abstraction;
    using NBA.Models.Entities;

    public interface IPlayerStatRepository
    {
        ServiceResult GetAllPlayerStats();
        ServiceResult GetPlayerStatWithId(int Id);
        ServiceResult GetPlayerStatsWithPlayerIdAndGameNo(int playerId, int gameNo);
        ServiceResult GetPlayerStatsWithGameNo(int gameNo);
        ServiceResult GetPlayerStatsWithPlayerId(int playerId);
        ServiceResult GetPlayerStatsWithTeam(Team team);
        ServiceResult AddPlayerStat(PlayerStats stats);
        ServiceResult UpdatePlayerStat(PlayerStats stats);
        ServiceResult DeletePlayerStat(int id);
        ServiceResult GetAllPlayerStats20_21();
        ServiceResult GetPlayerStatWithId20_21(int Id);
        ServiceResult GetPlayerStatsWithPlayerIdAndGameNo20_21(int playerId, int gameNo);
        ServiceResult GetPlayerStatsWithGameNo20_21(int gameNo);
        ServiceResult GetPlayerStatsWithPlayerId20_21(int playerId);
        ServiceResult GetPlayerStatsWithTeam20_21(Team team);
        ServiceResult AddPlayerStat20_21(PlayerStats20_21 stats);
        ServiceResult UpdatePlayerStat20_21(PlayerStats20_21 stats);
        ServiceResult DeletePlayerStat20_21(int id);
        ServiceResult GetAllPlayerStats19_20();
        ServiceResult GetPlayerStatWithId19_20(int Id);
        ServiceResult GetPlayerStatsWithPlayerIdAndGameNo19_20(int playerId, int gameNo);
        ServiceResult GetPlayerStatsWithGameNo19_20(int gameNo);
        ServiceResult GetPlayerStatsWithPlayerId19_20(int playerId);
        ServiceResult GetPlayerStatsWithTeam19_20(Team team);
        ServiceResult AddPlayerStat19_20(PlayerStats19_20 stats);
        ServiceResult UpdatePlayerStat19_20(PlayerStats19_20 stats);
        ServiceResult DeletePlayerStat19_20(int id);
    }
}
