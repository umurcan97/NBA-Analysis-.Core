using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace NBA.Services.Repositories
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    public class FullSeasonQuartersRepository : IFullSeasonQuartersRepository
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;
        private readonly IGameTimesRepository _gameTimesRepository;
        public FullSeasonQuartersRepository (INBAContext db,
            IUnitOfWork uw,
            IGameTimesRepository gameTimesRepository)
        {
            this._db = db;
            this._uw = uw;
            this._gameTimesRepository = gameTimesRepository;
        }

        public ServiceResult AddQuarter(FullSeasonQuarters stats)
        {
            _db.FullSeasonQuarters.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateQuarter(FullSeasonQuarters stats)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteQuarter(int gameNo, int quarterNo)
        {
            var stat = _db.FullSeasonQuarters.FirstOrDefault(x => x.GameNo == gameNo && x.QuarterNo == quarterNo);
            if(stat==null)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            _db.FullSeasonQuarters.Remove(stat);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeleteQuarter(int id)
        {
            var stat = _db.FullSeasonQuarters.Find(id);
            if (stat == null)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            _db.FullSeasonQuarters.Remove(stat);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult<List<FullSeasonQuarters>> GetFullSeasonQuarters()
        {
            var stats = _db.FullSeasonQuarters.ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<FullSeasonQuarters>> GetFullSeasonQuartersForTeam(Team team)
        {
            var stats = _db.FullSeasonQuarters.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<FullSeasonQuarters>> GetQuartersBetween(DateTime date1, DateTime date2)
        {
            var seasontimes = _gameTimesRepository.GetGamesBetween(date1, date2);
            if (!seasontimes.Succeeded)
                return (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters> season = new List<FullSeasonQuarters>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult<List<FullSeasonQuarters>> GetQuartersSinceDate(DateTime date)
        {
            var seasontimes = _gameTimesRepository.GetGamesSince(date);
            if (!seasontimes.Succeeded)
                return (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters> season = new List<FullSeasonQuarters>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult<List<FullSeasonQuarters>> GetQuartersTillDate(DateTime date)
        {
            var seasontimes = _gameTimesRepository.GetGamesTill(date);
            if (!seasontimes.Succeeded)
                return (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters> season = new List<FullSeasonQuarters>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult<List<FullSeasonQuarters>> GetQuartersWithGameNo(int GameNo)
        {
            var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == GameNo).ToList();
            if (stats.Count != 4)
            {
                return (ServiceResult<List<FullSeasonQuarters>>)ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            }

            return ServiceResult.Success(stats);
        }
    }
}
