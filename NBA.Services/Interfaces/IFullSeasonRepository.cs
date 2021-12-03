namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using System;
    using System.Collections.Generic;
    public interface IFullSeasonRepository
    {
        ServiceResult AddGame(FullSeason stats);
        ServiceResult UpdateGame(FullSeason stats);
        ServiceResult GetGameWithGameNo(int GameNo);
        ServiceResult GetFullSeason();
        ServiceResult GetGamesSinceDate(DateTime date);
        ServiceResult GetGamesTillDate(DateTime date);
        ServiceResult GetGamesBetween(DateTime date1, DateTime date2);
        ServiceResult GetFullSeasonForTeam(Team team);
        ServiceResult AddGame20_21(FullSeason20_21 stats);
        ServiceResult UpdateGame20_21(FullSeason20_21 stats);
        ServiceResult GetGameWithGameNo20_21(int GameNo);
        ServiceResult GetFullSeason20_21();
        ServiceResult GetGamesSinceDate20_21(DateTime date);
        ServiceResult GetGamesTillDate20_21(DateTime date);
        ServiceResult GetGamesBetween20_21(DateTime date1, DateTime date2);
        ServiceResult GetFullSeasonForTeam20_21(Team team);
        ServiceResult AddGame19_20(FullSeason19_20 stats);
        ServiceResult UpdateGame19_20(FullSeason19_20 stats);
        ServiceResult GetGameWithGameNo19_20(int GameNo);
        ServiceResult GetFullSeason19_20();
        ServiceResult GetGamesSinceDate19_20(DateTime date);
        ServiceResult GetGamesTillDate19_20(DateTime date);
        ServiceResult GetGamesBetween19_20(DateTime date1, DateTime date2);
        ServiceResult GetFullSeasonForTeam19_20(Team team);
    }
}
