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
        ServiceResult<FullSeason> GetGameWithGameNo(int GameNo);
        ServiceResult<List<FullSeason>> GetFullSeason();
        ServiceResult<List<FullSeason>> GetGamesSinceDate(DateTime date);
        ServiceResult<List<FullSeason>> GetGamesTillDate(DateTime date);
        ServiceResult<List<FullSeason>> GetGamesBetween(DateTime date1, DateTime date2);
        ServiceResult<List<FullSeason>> GetFullSeasonForTeam(Team team);
        ServiceResult AddGame20_21(FullSeason20_21 stats);
        ServiceResult UpdateGame20_21(FullSeason20_21 stats);
        ServiceResult<FullSeason20_21> GetGameWithGameNo20_21(int GameNo);
        ServiceResult<List<FullSeason20_21>> GetFullSeason20_21();
        ServiceResult<List<FullSeason20_21>> GetGamesSinceDate20_21(DateTime date);
        ServiceResult<List<FullSeason20_21>> GetGamesTillDate20_21(DateTime date);
        ServiceResult<List<FullSeason20_21>> GetGamesBetween20_21(DateTime date1, DateTime date2);
        ServiceResult<List<FullSeason20_21>> GetFullSeasonForTeam20_21(Team team);
        ServiceResult AddGame19_20(FullSeason19_20 stats);
        ServiceResult UpdateGame19_20(FullSeason19_20 stats);
        ServiceResult<FullSeason19_20> GetGameWithGameNo19_20(int GameNo);
        ServiceResult<List<FullSeason19_20>> GetFullSeason19_20();
        ServiceResult<List<FullSeason19_20>> GetGamesSinceDate19_20(DateTime date);
        ServiceResult<List<FullSeason19_20>> GetGamesTillDate19_20(DateTime date);
        ServiceResult<List<FullSeason19_20>> GetGamesBetween19_20(DateTime date1, DateTime date2);
        ServiceResult<List<FullSeason19_20>> GetFullSeasonForTeam19_20(Team team);
    }
}
