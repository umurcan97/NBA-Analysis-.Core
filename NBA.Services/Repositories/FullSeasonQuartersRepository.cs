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

        public ServiceResult GetFullSeasonQuarters()
        {
            var stats = _db.FullSeasonQuarters.ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetFullSeasonQuartersForTeam(Team team)
        {
            var stats = _db.FullSeasonQuarters.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetQuartersBetween(DateTime date1, DateTime date2)
        {
            var seasontimes = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesBetween(date1, date2);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters> season = new List<FullSeasonQuarters>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersSinceDate(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesSince(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters> season = new List<FullSeasonQuarters>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersTillDate(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesTill(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters> season = new List<FullSeasonQuarters>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersWithGameNo(int GameNo)
        {
            var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == GameNo).ToList();
            if (stats.Count != 4)
            {
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            }

            return ServiceResult.Success(stats);
        }

        public ServiceResult GetQuartersWithGameNoAndQuarterNo(int GameNo, int QuarterNo)
        {
            var stats = _db.FullSeasonQuarters.Where(x => x.GameNo == GameNo && x.QuarterNo == QuarterNo)
                .FirstOrDefault();
            if (stats == null)
            {
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            }

            return ServiceResult.Success(stats);
        }



        public ServiceResult AddQuarter20_21(FullSeasonQuarters20_21 stats)
        {
            _db.FullSeasonQuarters20_21.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateQuarter20_21(FullSeasonQuarters20_21 stats)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteQuarter20_21(int gameNo, int quarterNo)
        {
            var stat = _db.FullSeasonQuarters20_21.FirstOrDefault(x => x.GameNo == gameNo && x.QuarterNo == quarterNo);
            if (stat == null)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            _db.FullSeasonQuarters20_21.Remove(stat);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeleteQuarter20_21(int id)
        {
            var stat = _db.FullSeasonQuarters20_21.Find(id);
            if (stat == null)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            _db.FullSeasonQuarters20_21.Remove(stat);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult GetFullSeasonQuarters20_21()
        {
            var stats = _db.FullSeasonQuarters20_21.ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetFullSeasonQuartersForTeam20_21(Team team)
        {
            var stats = _db.FullSeasonQuarters20_21.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetQuartersBetween20_21(DateTime date1, DateTime date2)
        {
            var seasontimes = (ServiceResult<List<GameTime20_21>>)_gameTimesRepository.GetGamesBetween20_21(date1, date2);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters20_21> season = new List<FullSeasonQuarters20_21>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters20_21.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersSinceDate20_21(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime20_21>>)_gameTimesRepository.GetGamesSince20_21(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters20_21> season = new List<FullSeasonQuarters20_21>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters20_21.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersTillDate20_21(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime20_21>>)_gameTimesRepository.GetGamesTill20_21(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters20_21> season = new List<FullSeasonQuarters20_21>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters20_21.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersWithGameNo20_21(int GameNo)
        {
            var stats = _db.FullSeasonQuarters20_21.Where(x => x.GameNo == GameNo).ToList();
            if (stats.Count != 4)
            {
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            }

            return ServiceResult.Success(stats);
        }

        public ServiceResult GetQuartersWithGameNoAndQuarterNo20_21(int GameNo, int QuarterNo)
        {
            var stats = _db.FullSeasonQuarters20_21.Where(x => x.GameNo == GameNo && x.QuarterNo == QuarterNo)
                .FirstOrDefault();
            if (stats == null)
            {
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            }

            return ServiceResult.Success(stats);
        }



        public ServiceResult AddQuarter19_20(FullSeasonQuarters19_20 stats)
        {
            _db.FullSeasonQuarters19_20.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateQuarter19_20(FullSeasonQuarters19_20 stats)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteQuarter19_20(int gameNo, int quarterNo)
        {
            var stat = _db.FullSeasonQuarters19_20.FirstOrDefault(x => x.GameNo == gameNo && x.QuarterNo == quarterNo);
            if (stat == null)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            _db.FullSeasonQuarters19_20.Remove(stat);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeleteQuarter19_20(int id)
        {
            var stat = _db.FullSeasonQuarters19_20.Find(id);
            if (stat == null)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            _db.FullSeasonQuarters19_20.Remove(stat);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult GetFullSeasonQuarters19_20()
        {
            var stats = _db.FullSeasonQuarters19_20.ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetFullSeasonQuartersForTeam19_20(Team team)
        {
            var stats = _db.FullSeasonQuarters19_20.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetQuartersBetween19_20(DateTime date1, DateTime date2)
        {
            var seasontimes = (ServiceResult<List<GameTime19_20>>)_gameTimesRepository.GetGamesBetween19_20(date1, date2);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters19_20> season = new List<FullSeasonQuarters19_20>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters19_20.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersSinceDate19_20(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime19_20>>)_gameTimesRepository.GetGamesSince19_20(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters19_20> season = new List<FullSeasonQuarters19_20>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters19_20.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersTillDate19_20(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime19_20>>)_gameTimesRepository.GetGamesTill19_20(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeasonQuarters19_20> season = new List<FullSeasonQuarters19_20>();
            foreach (var game in seasontimes.Data)
            {
                var stats = _db.FullSeasonQuarters19_20.Where(x => x.GameNo == game.GameNo).ToList();
                if (stats.Count != 4)
                {
                    var result = ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
                    result.Error.Message += " eksik maç: " + game.GameNo;
                    return result;
                }
                season.AddRange(stats);
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetQuartersWithGameNo19_20(int GameNo)
        {
            var stats = _db.FullSeasonQuarters19_20.Where(x => x.GameNo == GameNo).ToList();
            if (stats.Count != 4)
            {
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            }

            return ServiceResult.Success(stats);
        }

        public ServiceResult GetQuartersWithGameNoAndQuarterNo19_20(int GameNo, int QuarterNo)
        {
            var stats = _db.FullSeasonQuarters19_20.Where(x => x.GameNo == GameNo && x.QuarterNo == QuarterNo)
                .FirstOrDefault();
            if (stats == null)
            {
                return ServiceResult.Failed(ServiceError.FullSeasonQuarterNotFound);
            }

            return ServiceResult.Success(stats);
        }
    }
}
