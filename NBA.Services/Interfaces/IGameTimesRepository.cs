namespace NBA.Services.Interfaces
{
    using NBA.Services.Abstraction;
    using NBA.Models.Entities;
    using NBA.Models;
    using System;
    using System.Collections.Generic;

    public interface IGameTimesRepository
    {
        ServiceResult AddGameTime(GameTime gameTime);
        ServiceResult UpdateGameTime(GameTime gameTime);
        ServiceResult DeleteGameTime(int gameNo);
        ServiceResult<GameTime> GetGameTime(int gameNo);
        ServiceResult<List<GameTime>> GetFullSeasonForTeam(Team team);
        ServiceResult<List<GameTime>> GetFullSeason();
        ServiceResult<List<GameTime>> GetGamesToBePlayedToday();
        ServiceResult<List<GameTime>> GetGamesSince(DateTime date);
        ServiceResult<List<GameTime>> GetGamesTill(DateTime date);
        ServiceResult<List<GameTime>> GetGamesBetween(DateTime date1, DateTime date2);
        ServiceResult AddGameTime19_20(GameTime19_20 gameTime);
        ServiceResult UpdateGameTime19_20(GameTime19_20 gameTime);
        ServiceResult DeleteGameTime19_20(int gameNo);
        ServiceResult<List<GameTime19_20>> GetFullSeason19_20();
        ServiceResult<List<GameTime19_20>> GetGamesBetween19_20(DateTime date1, DateTime date2);
        ServiceResult<List<GameTime19_20>> GetGamesSince19_20(DateTime date);
        ServiceResult<List<GameTime19_20>> GetGamesTill19_20(DateTime date);
        ServiceResult<GameTime19_20> GetGameTime19_20(int gameNo);
        ServiceResult<List<GameTime19_20>> GetFullSeasonForTeam19_20(Team team);
        ServiceResult AddGameTime20_21(GameTime20_21 gameTime);
        ServiceResult UpdateGameTime20_21(GameTime20_21 gameTime);
        ServiceResult DeleteGameTime20_21(int gameNo);
        ServiceResult<List<GameTime20_21>> GetFullSeason20_21();
        ServiceResult<List<GameTime20_21>> GetGamesBetween20_21(DateTime date1, DateTime date2);
        ServiceResult<List<GameTime20_21>> GetGamesSince20_21(DateTime date);
        ServiceResult<List<GameTime20_21>> GetGamesTill20_21(DateTime date);
        ServiceResult<GameTime20_21> GetGameTime20_21(int gameNo);
        ServiceResult<List<GameTime20_21>> GetFullSeasonForTeam20_21(Team team);
    }
}
