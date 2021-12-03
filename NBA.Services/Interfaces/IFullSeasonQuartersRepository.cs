namespace NBA.Services.Interfaces
{
    using NBA.Models;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using System;
    using System.Collections.Generic;
    public interface IFullSeasonQuartersRepository
    {
        ServiceResult AddQuarter(FullSeasonQuarters stats);
        ServiceResult UpdateQuarter(FullSeasonQuarters stats);
        ServiceResult DeleteQuarter(int gameNo, int quarterNo);
        ServiceResult DeleteQuarter(int id);
        ServiceResult GetQuartersWithGameNo(int GameNo);
        ServiceResult GetQuartersWithGameNoAndQuarterNo(int GameNo, int QuarterNo);
        ServiceResult GetFullSeasonQuarters();
        ServiceResult GetQuartersSinceDate(DateTime date);
        ServiceResult GetQuartersTillDate(DateTime date);
        ServiceResult GetQuartersBetween(DateTime date1, DateTime date2);
        ServiceResult GetFullSeasonQuartersForTeam(Team team);
        ServiceResult AddQuarter20_21(FullSeasonQuarters20_21 stats);
        ServiceResult UpdateQuarter20_21(FullSeasonQuarters20_21 stats);
        ServiceResult DeleteQuarter20_21(int gameNo, int quarterNo);
        ServiceResult DeleteQuarter20_21(int id);
        ServiceResult GetQuartersWithGameNo20_21(int GameNo);
        ServiceResult GetQuartersWithGameNoAndQuarterNo20_21(int GameNo, int QuarterNo);
        ServiceResult GetFullSeasonQuarters20_21();
        ServiceResult GetQuartersSinceDate20_21(DateTime date);
        ServiceResult GetQuartersTillDate20_21(DateTime date);
        ServiceResult GetQuartersBetween20_21(DateTime date1, DateTime date2);
        ServiceResult GetFullSeasonQuartersForTeam20_21(Team team);
        ServiceResult AddQuarter19_20(FullSeasonQuarters19_20 stats);
        ServiceResult UpdateQuarter19_20(FullSeasonQuarters19_20 stats);
        ServiceResult DeleteQuarter19_20(int gameNo, int quarterNo);
        ServiceResult DeleteQuarter19_20(int id);
        ServiceResult GetQuartersWithGameNo19_20(int GameNo);
        ServiceResult GetQuartersWithGameNoAndQuarterNo19_20(int GameNo, int QuarterNo);
        ServiceResult GetFullSeasonQuarters19_20();
        ServiceResult GetQuartersSinceDate19_20(DateTime date);
        ServiceResult GetQuartersTillDate19_20(DateTime date);
        ServiceResult GetQuartersBetween19_20(DateTime date1, DateTime date2);
        ServiceResult GetFullSeasonQuartersForTeam19_20(Team team);
    }
}