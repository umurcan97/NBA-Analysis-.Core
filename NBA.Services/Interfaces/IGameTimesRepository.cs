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
        ServiceResult GetGameTime(int gameNo);
        ServiceResult GetFullSeasonForTeam(Team team);
        ServiceResult GetFullSeasonForTeamPlayed(Team team);
        ServiceResult GetFullSeason();
        ServiceResult GetFullSeasonPlayed();
        ServiceResult GetGamesToBePlayedToday();
        ServiceResult GetGamesSince(DateTime date);
        ServiceResult GetGamesTill(DateTime date);
        ServiceResult GetGamesBetween(DateTime date1, DateTime date2);
        ServiceResult AddGameTime20_21(GameTime20_21 gameTime);
        ServiceResult UpdateGameTime20_21(GameTime20_21 gameTime);
        ServiceResult DeleteGameTime20_21(int gameNo);
        ServiceResult GetFullSeason20_21();
        ServiceResult GetFullSeasonPlayed20_21();
        ServiceResult GetGamesBetween20_21(DateTime date1, DateTime date2);
        ServiceResult GetGamesSince20_21(DateTime date);
        ServiceResult GetGamesTill20_21(DateTime date);
        ServiceResult GetGameTime20_21(int gameNo);
        ServiceResult GetFullSeasonForTeam20_21(Team team);
        ServiceResult GetFullSeasonForTeamPlayed20_21(Team team);
        ServiceResult AddGameTime19_20(GameTime19_20 gameTime);
        ServiceResult UpdateGameTime19_20(GameTime19_20 gameTime);
        ServiceResult DeleteGameTime19_20(int gameNo);
        ServiceResult GetFullSeason19_20();
        ServiceResult GetFullSeasonPlayed19_20();
        ServiceResult GetGamesBetween19_20(DateTime date1, DateTime date2);
        ServiceResult GetGamesSince19_20(DateTime date);
        ServiceResult GetGamesTill19_20(DateTime date);
        ServiceResult GetGameTime19_20(int gameNo);
        ServiceResult GetFullSeasonForTeam19_20(Team team);
        ServiceResult GetFullSeasonForTeamPlayed19_20(Team team);
    }
}
