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
            return  ServiceResult.Success();
        }

        public ServiceResult UpdateGame(FullSeason stats)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<FullSeason> GetGameWithGameNo(int GameNo)
        {
            FullSeason stats = _db.FullSeason.FirstOrDefault(x => x.GameNo == GameNo);
            if (stats == null)
                return (ServiceResult<FullSeason>)ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return  ServiceResult.Success(stats);
        }

        public ServiceResult<List<FullSeason>> GetFullSeason()
        {
            List<FullSeason> season = _db.FullSeason.ToList();
            return ServiceResult.Success(season);
        }

        public ServiceResult<List<FullSeason>> GetGamesSinceDate(DateTime date)
        {
            var seasontimes = _gameTimesRepository.GetGamesSince(date);
            if (!seasontimes.Succeeded)
                return (ServiceResult<List<FullSeason>>) ServiceResult.Failed(seasontimes.Error);
            List<FullSeason> season = new List<FullSeason>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult<List<FullSeason>> GetGamesTillDate(DateTime date)
        {
            var seasontimes = _gameTimesRepository.GetGamesTill(date);
            if (!seasontimes.Succeeded)
                return (ServiceResult<List<FullSeason>>)ServiceResult.Failed(seasontimes.Error);
            List<FullSeason> season = new List<FullSeason>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult<List<FullSeason>> GetGamesBetween(DateTime date1, DateTime date2)
        {
            var seasontimes = _gameTimesRepository.GetGamesBetween(date1, date2);
            if (!seasontimes.Succeeded)
                return (ServiceResult<List<FullSeason>>)ServiceResult.Failed(seasontimes.Error);
            List<FullSeason> season = new List<FullSeason>();
            foreach (var game in seasontimes.Data)
            {
                season.Add(_db.FullSeason.FirstOrDefault(x => x.GameNo == game.GameNo));
            }

            return ServiceResult.Success(season);
        }

        public ServiceResult<List<FullSeason>> GetFullSeasonForTeam(Team team)
        {
            var season = _db.FullSeason.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (season.Count==0)
                return (ServiceResult<List<FullSeason>>)ServiceResult.Failed(ServiceError.FullSeasonNotFound);
            return ServiceResult.Success(season);
        }
    }
}
