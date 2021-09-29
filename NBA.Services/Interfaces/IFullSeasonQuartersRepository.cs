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
        ServiceResult<List<FullSeasonQuarters>> GetQuartersWithGameNo(int GameNo);
        ServiceResult<List<FullSeasonQuarters>> GetFullSeasonQuarters();
        ServiceResult<List<FullSeasonQuarters>> GetQuartersSinceDate(DateTime date);
        ServiceResult<List<FullSeasonQuarters>> GetQuartersTillDate(DateTime date);
        ServiceResult<List<FullSeasonQuarters>> GetQuartersBetween(DateTime date1, DateTime date2);
        ServiceResult<List<FullSeasonQuarters>> GetFullSeasonQuartersForTeam(Team team);
        ServiceResult AddQuarter20_21(FullSeasonQuarters20_21 stats);
        ServiceResult UpdateQuarter20_21(FullSeasonQuarters20_21 stats);
        ServiceResult DeleteQuarter20_21(int gameNo, int quarterNo);
        ServiceResult DeleteQuarter20_21(int id);
        ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersWithGameNo20_21(int GameNo);
        ServiceResult<List<FullSeasonQuarters20_21>> GetFullSeasonQuarters20_21();
        ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersSinceDate20_21(DateTime date);
        ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersTillDate20_21(DateTime date);
        ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersBetween20_21(DateTime date1, DateTime date2);
        ServiceResult<List<FullSeasonQuarters20_21>> GetFullSeasonQuartersForTeam20_21(Team team);
        ServiceResult AddQuarter19_20(FullSeasonQuarters19_20 stats);
        ServiceResult UpdateQuarter19_20(FullSeasonQuarters19_20 stats);
        ServiceResult DeleteQuarter19_20(int gameNo, int quarterNo);
        ServiceResult DeleteQuarter19_20(int id);
        ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersWithGameNo19_20(int GameNo);
        ServiceResult<List<FullSeasonQuarters19_20>> GetFullSeasonQuarters19_20();
        ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersSinceDate19_20(DateTime date);
        ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersTillDate19_20(DateTime date);
        ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersBetween19_20(DateTime date1, DateTime date2);
        ServiceResult<List<FullSeasonQuarters19_20>> GetFullSeasonQuartersForTeam19_20(Team team);
    }
}