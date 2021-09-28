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
    }
}