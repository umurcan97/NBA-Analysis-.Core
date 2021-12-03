namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using System.Collections.Generic;
    public interface IPlayerStatQuarterRepository
    {
        ServiceResult GetAllPlayerStatsQuarter();
        ServiceResult GetPlayerStatsQuarterWithId(int Id);
        ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNo(int playerId, int gameNo);
        ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo(int playerId, int gameNo, int quarterNo);
        ServiceResult GetPlayerStatsQuarterWithGameNo(int gameNo);
        ServiceResult GetPlayerStatsQuarterWithPlayerId(int playerId);
        ServiceResult GetPlayerStatsQuarterWithTeam(Team team);
        ServiceResult AddPlayerStatsQuarter(PlayerStatsQuarter stats);
        ServiceResult UpdatePlayerStatsQuarter(PlayerStatsQuarter stats);
        ServiceResult DeletePlayerStatsQuarter(int id);
        ServiceResult GetAllPlayerStatsQuarter20_21();
        ServiceResult GetPlayerStatsQuarterWithId20_21(int Id);
        ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNo20_21(int playerId, int gameNo);
        ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo20_21(int playerId, int gameNo, int quarterNo);
        ServiceResult GetPlayerStatsQuarterWithGameNo20_21(int gameNo);
        ServiceResult GetPlayerStatsQuarterWithPlayerId20_21(int playerId);
        ServiceResult GetPlayerStatsQuarterWithTeam20_21(Team team);
        ServiceResult AddPlayerStatsQuarter20_21(PlayerStatsQuarter20_21 stats);
        ServiceResult UpdatePlayerStatsQuarter20_21(PlayerStatsQuarter20_21 stats);
        ServiceResult DeletePlayerStatsQuarter20_21(int id);
        ServiceResult GetAllPlayerStatsQuarter19_20();
        ServiceResult GetPlayerStatsQuarterWithId19_20(int Id);
        ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNo19_20(int playerId, int gameNo);
        ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo19_20(int playerId, int gameNo, int quarterNo);
        ServiceResult GetPlayerStatsQuarterWithGameNo19_20(int gameNo);
        ServiceResult GetPlayerStatsQuarterWithPlayerId19_20(int playerId);
        ServiceResult GetPlayerStatsQuarterWithTeam19_20(Team team);
        ServiceResult AddPlayerStatsQuarter19_20(PlayerStatsQuarter19_20 stats);
        ServiceResult UpdatePlayerStatsQuarter19_20(PlayerStatsQuarter19_20 stats);
        ServiceResult DeletePlayerStatsQuarter19_20(int id);
    }
}
