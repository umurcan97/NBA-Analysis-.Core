namespace NBA.Services.Repositories
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class FullSeasonRepository: IFullSeasonRepository
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;
        private readonly IGameTimesRepository _gameTimesRepository;
        public FullSeasonRepository(INBAContext db, 
            IUnitOfWork uw,
            IGameTimesRepository gameTimesRepository)
        {
            this._db = db;
            this._uw = uw;
            this._gameTimesRepository = gameTimesRepository;
        }

        public ServiceResult AddGame(FullSeason stats)
        {
            _db.FullSeason.Add(stats);
            _uw.Commit();
            return  ServiceResult.Success();
        }

        public ServiceResult UpdateGame(FullSeason stats)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetGameWithGameNo(int GameNo)
        {
            FullSeason stats = _db.FullSeason.FirstOrDefault(x => x.GameNo == GameNo);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return  ServiceResult.Success(stats);
        }

        public ServiceResult GetFullSeason()
        {
            List<FullSeason> season = _db.FullSeason.ToList();
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesSinceDate(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesSince(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason> season = new List<FullSeason>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesTillDate(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesTill(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason> season = new List<FullSeason>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesBetween(DateTime date1, DateTime date2)
        {
            var seasontimes = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesBetween(date1, date2);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason> season = new List<FullSeason>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetFullSeasonForTeam(Team team)
        {
            var season = _db.FullSeason.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (season.Count==0)
                return ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return ServiceResult.Success(season);
        }



        public ServiceResult AddGame20_21(FullSeason20_21 stats)
        {
            _db.FullSeason20_21.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateGame20_21(FullSeason20_21 stats)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetGameWithGameNo20_21(int GameNo)
        {
            FullSeason20_21 stats = _db.FullSeason20_21.FirstOrDefault(x => x.GameNo == GameNo);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetFullSeason20_21()
        {
            List<FullSeason20_21> season = _db.FullSeason20_21.ToList();
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesSinceDate20_21(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime20_21>>)_gameTimesRepository.GetGamesSince20_21(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason20_21> season = new List<FullSeason20_21>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason20_21.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesTillDate20_21(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime20_21>>)_gameTimesRepository.GetGamesTill20_21(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason20_21> season = new List<FullSeason20_21>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason20_21.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesBetween20_21(DateTime date1, DateTime date2)
        {
            var seasontimes = (ServiceResult<List<GameTime20_21>>)_gameTimesRepository.GetGamesBetween(date1, date2);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason20_21> season = new List<FullSeason20_21>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason20_21.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetFullSeasonForTeam20_21(Team team)
        {
            var season = _db.FullSeason20_21.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return ServiceResult.Success(season);
        }



        public ServiceResult AddGame19_20(FullSeason19_20 stats)
        {
            _db.FullSeason19_20.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateGame19_20(FullSeason19_20 stats)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetGameWithGameNo19_20(int GameNo)
        {
            FullSeason19_20 stats = _db.FullSeason19_20.FirstOrDefault(x => x.GameNo == GameNo);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetFullSeason19_20()
        {
            List<FullSeason19_20> season = _db.FullSeason19_20.ToList();
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesSinceDate19_20(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime19_20>>)_gameTimesRepository.GetGamesSince19_20(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason19_20> season = new List<FullSeason19_20>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason19_20.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesTillDate19_20(DateTime date)
        {
            var seasontimes = (ServiceResult<List<GameTime19_20>>)_gameTimesRepository.GetGamesTill19_20(date);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason19_20> season = new List<FullSeason19_20>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason19_20.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesBetween19_20(DateTime date1, DateTime date2)
        {
            var seasontimes = (ServiceResult<List<GameTime19_20>>)_gameTimesRepository.GetGamesBetween(date1, date2);
            if (!seasontimes.Succeeded)
                return ServiceResult.Failed(seasontimes.Error);
            List<FullSeason19_20> season = new List<FullSeason19_20>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason19_20.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult GetFullSeasonForTeam19_20(Team team)
        {
            var season = _db.FullSeason19_20.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return ServiceResult.Success(season);
        }
    }
}
