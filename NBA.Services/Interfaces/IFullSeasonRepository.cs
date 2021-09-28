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
    }
}
