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

    public class GameTimesRepository : IGameTimesRepository
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;
        public GameTimesRepository(INBAContext db, IUnitOfWork uw)
        {
            this._db = db;
            this._uw = uw;
        }
        public ServiceResult AddGameTime(GameTime gameTime)
        {
            _db.GameTime.Add(gameTime);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateGameTime(GameTime gameTime)
        {
            GameTime game = _db.GameTime.FirstOrDefault(x => x.GameNo == gameTime.GameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            game.GameDate = gameTime.GameDate;
            game.HomeTeam = gameTime.HomeTeam;
            game.AwayTeam = gameTime.AwayTeam;
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeleteGameTime(int gameNo)
        {
            GameTime game = _db.GameTime.FirstOrDefault(x => x.GameNo == gameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            _db.GameTime.Remove(game);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult GetFullSeason()
        {
            List<GameTime> season = _db.GameTime.ToList();
            if (season.Count == 0)
                return  ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetFullSeasonPlayed()
        {
            List<GameTime> season = _db.GameTime.Where(x => x.GameDate <= DateTime.Now.AddHours(-8)).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesToBePlayedToday()
        {
            List<GameTime> games = _db.GameTime
                .Where(x => x.GameDate > DateTime.Now.AddHours(-8) == x.GameDate < DateTime.Now.AddHours(16)).ToList();
            return ServiceResult.Success(games);
        }

        public ServiceResult GetGamesBetween(DateTime date1, DateTime date2)
        {
            List<GameTime> season = _db.GameTime.Where(x => x.GameDate >= date1 && x.GameDate <= date2).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesSince(DateTime date)
        {
            List<GameTime> season = _db.GameTime.Where(x => x.GameDate <= DateTime.Now.AddHours(-8) && x.GameDate >= date).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesTill(DateTime date)
        {
            List<GameTime> season = _db.GameTime.Where(x => x.GameDate <= date).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGameTime(int gameNo)
        {
            GameTime game = _db.GameTime.FirstOrDefault(x => x.GameNo == gameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(game);
        }

        public ServiceResult GetFullSeasonForTeam(Team team)
        {
            var gameTimes = _db.GameTime.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (gameTimes.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(gameTimes);
        }

        public ServiceResult GetFullSeasonForTeamPlayed(Team team)
        {
            var gameTimes = _db.GameTime.Where(x =>
                (x.HomeTeam == team || x.AwayTeam == team) && x.GameDate < DateTime.Now.AddHours(-8)).ToList();
            if (gameTimes.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(gameTimes);
        }



        public ServiceResult AddGameTime19_20(GameTime19_20 gameTime)
        {
            _db.GameTime19_20.Add(gameTime);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateGameTime19_20(GameTime19_20 gameTime)
        {
            GameTime19_20 game = _db.GameTime19_20.FirstOrDefault(x => x.GameNo == gameTime.GameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            game.GameDate = game.GameDate;
            game.HomeTeam = game.HomeTeam;
            game.AwayTeam = game.AwayTeam;
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeleteGameTime19_20(int gameNo)
        {
            GameTime19_20 game = _db.GameTime19_20.FirstOrDefault(x => x.GameNo == gameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            _db.GameTime19_20.Remove(game);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult GetFullSeason19_20()
        {
            List<GameTime19_20> season = _db.GameTime19_20.ToList();
            if (season.Count == 0)
                return  ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetFullSeasonPlayed19_20()
        {
            List<GameTime19_20> season = _db.GameTime19_20.Where(x => x.GameDate <= DateTime.Now.AddHours(-8)).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesBetween19_20(DateTime date1, DateTime date2)
        {
            List<GameTime19_20> season = _db.GameTime19_20.Where(x => x.GameDate >= date1 && x.GameDate <= date2).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesSince19_20(DateTime date)
        {
            List<GameTime19_20> season = _db.GameTime19_20.Where(x => x.GameDate <= DateTime.Now.AddHours(-8) && x.GameDate >= date).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesTill19_20(DateTime date)
        {
            List<GameTime19_20> season = _db.GameTime19_20.Where(x => x.GameDate <= date).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGameTime19_20(int gameNo)
        {
            GameTime19_20 game = _db.GameTime19_20.FirstOrDefault(x => x.GameNo == gameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(game);
        }

        public ServiceResult GetFullSeasonForTeam19_20(Team team)
        {
            var gameTimes = _db.GameTime19_20.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (gameTimes.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(gameTimes);
        }

        public ServiceResult GetFullSeasonForTeamPlayed19_20(Team team)
        {
            var gameTimes = _db.GameTime19_20.Where(x =>
                (x.HomeTeam == team || x.AwayTeam == team) && x.GameDate < DateTime.Now.AddHours(-8)).ToList();
            if (gameTimes.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(gameTimes);
        }



        public ServiceResult AddGameTime20_21(GameTime20_21 gameTime)
        {
            _db.GameTime20_21.Add(gameTime);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdateGameTime20_21(GameTime20_21 gameTime)
        {
            GameTime20_21 game = _db.GameTime20_21.FirstOrDefault(x => x.GameNo == gameTime.GameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            game.GameDate = game.GameDate;
            game.HomeTeam = game.HomeTeam;
            game.AwayTeam = game.AwayTeam;
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeleteGameTime20_21(int gameNo)
        {
            GameTime20_21 game = _db.GameTime20_21.FirstOrDefault(x => x.GameNo == gameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            _db.GameTime20_21.Remove(game);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult GetFullSeason20_21()
        {
            List<GameTime20_21> season = _db.GameTime20_21.ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetFullSeasonPlayed20_21()
        {
            List<GameTime20_21> season = _db.GameTime20_21.Where(x => x.GameDate <= DateTime.Now.AddHours(-8)).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesBetween20_21(DateTime date1, DateTime date2)
        {
            List<GameTime20_21> season = _db.GameTime20_21.Where(x => x.GameDate >= date1 && x.GameDate <= date2).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesSince20_21(DateTime date)
        {
            List<GameTime20_21> season = _db.GameTime20_21.Where(x => x.GameDate <= DateTime.Now.AddHours(-8) && x.GameDate >= date).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGamesTill20_21(DateTime date)
        {
            List<GameTime20_21> season = _db.GameTime20_21.Where(x => x.GameDate <= date).ToList();
            if (season.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(season);
        }

        public ServiceResult GetGameTime20_21(int gameNo)
        {
            GameTime20_21 game = _db.GameTime20_21.FirstOrDefault(x => x.GameNo == gameNo);
            if (game == null)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(game);
        }

        public ServiceResult GetFullSeasonForTeam20_21(Team team)
        {
            var gameTimes = _db.GameTime20_21.Where(x => x.HomeTeam == team || x.AwayTeam == team).ToList();
            if (gameTimes.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(gameTimes);
        }

        public ServiceResult GetFullSeasonForTeamPlayed20_21(Team team)
        {
            var gameTimes = _db.GameTime20_21.Where(x =>
                (x.HomeTeam == team || x.AwayTeam == team) && x.GameDate < DateTime.Now.AddHours(-8)).ToList();
            if (gameTimes.Count == 0)
                return ServiceResult.Failed(ServiceError.GameTimeNotFound);
            return ServiceResult.Success(gameTimes);
        }
    }
}
