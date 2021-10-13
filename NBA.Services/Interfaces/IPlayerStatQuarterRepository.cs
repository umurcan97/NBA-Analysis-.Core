namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using System.Collections.Generic;
    public interface IPlayerStatQuarterRepository
    {
        ServiceResult<List<PlayerStatsQuarter>> GetAllPlayerStatsQuarter();
        ServiceResult<PlayerStatsQuarter> GetPlayerStatsQuarterWithId(int Id);
        ServiceResult<List<PlayerStatsQuarter>> GetPlayerStatsQuarterWithPlayerIdAndGameNo(int playerId, int gameNo);
        ServiceResult<PlayerStatsQuarter> GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo(int playerId, int gameNo, int quarterNo);
        ServiceResult<List<PlayerStatsQuarter>> GetPlayerStatsQuarterWithGameNo(int gameNo);
        ServiceResult<List<PlayerStatsQuarter>> GetPlayerStatsQuarterWithPlayerId(int playerId);
        ServiceResult<List<PlayerStatsQuarter>> GetPlayerStatsQuarterWithTeam(Team team);
        ServiceResult AddPlayerStatsQuarter(PlayerStatsQuarter stats);
        ServiceResult UpdatePlayerStatsQuarter(PlayerStatsQuarter stats);
        ServiceResult DeletePlayerStatsQuarter(int id);
        ServiceResult<List<PlayerStatsQuarter20_21>> GetAllPlayerStatsQuarter20_21();
        ServiceResult<PlayerStatsQuarter20_21> GetPlayerStatsQuarterWithId20_21(int Id);
        ServiceResult<List<PlayerStatsQuarter20_21>> GetPlayerStatsQuarterWithPlayerIdAndGameNo20_21(int playerId, int gameNo);
        ServiceResult<PlayerStatsQuarter20_21> GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo20_21(int playerId, int gameNo, int quarterNo);
        ServiceResult<List<PlayerStatsQuarter20_21>> GetPlayerStatsQuarterWithGameNo20_21(int gameNo);
        ServiceResult<List<PlayerStatsQuarter20_21>> GetPlayerStatsQuarterWithPlayerId20_21(int playerId);
        ServiceResult<List<PlayerStatsQuarter20_21>> GetPlayerStatsQuarterWithTeam20_21(Team team);
        ServiceResult AddPlayerStatsQuarter20_21(PlayerStatsQuarter20_21 stats);
        ServiceResult UpdatePlayerStatsQuarter20_21(PlayerStatsQuarter20_21 stats);
        ServiceResult DeletePlayerStatsQuarter20_21(int id);
        ServiceResult<List<PlayerStatsQuarter19_20>> GetAllPlayerStatsQuarter19_20();
        ServiceResult<PlayerStatsQuarter19_20> GetPlayerStatsQuarterWithId19_20(int Id);
        ServiceResult<List<PlayerStatsQuarter19_20>> GetPlayerStatsQuarterWithPlayerIdAndGameNo19_20(int playerId, int gameNo);
        ServiceResult<PlayerStatsQuarter19_20> GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo19_20(int playerId, int gameNo, int quarterNo);
        ServiceResult<List<PlayerStatsQuarter19_20>> GetPlayerStatsQuarterWithGameNo19_20(int gameNo);
        ServiceResult<List<PlayerStatsQuarter19_20>> GetPlayerStatsQuarterWithPlayerId19_20(int playerId);
        ServiceResult<List<PlayerStatsQuarter19_20>> GetPlayerStatsQuarterWithTeam19_20(Team team);
        ServiceResult AddPlayerStatsQuarter19_20(PlayerStatsQuarter19_20 stats);
        ServiceResult UpdatePlayerStatsQuarter19_20(PlayerStatsQuarter19_20 stats);
        ServiceResult DeletePlayerStatsQuarter19_20(int id);
    }
}
