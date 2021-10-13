using System.Linq;

namespace NBA.Services.Repositories
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using System.Collections.Generic;

    public class PlayerStatRepository : IPlayerStatRepository
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;
        private readonly IGameTimesRepository _gameTimesRepository;

        public PlayerStatRepository(INBAContext db, 
            IUnitOfWork uw,
            IGameTimesRepository gameTimesRepository)
        {
            this._db = db;
            this._uw = uw;
            this._gameTimesRepository = gameTimesRepository;
        }
        public ServiceResult AddPlayerStat(PlayerStats stats)
        {
            _db.PlayerStats.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeletePlayerStat(int id)
        {
            var stats = _db.PlayerStats.Find(id);
            if(stats==null)
                return ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            _db.PlayerStats.Remove(stats);
            return ServiceResult.Success();
        }

        public ServiceResult<List<PlayerStats>> GetAllPlayerStats()
        {
            var stats = _db.PlayerStats.ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats>> GetPlayerStatsWithPlayerId(int playerId)
        {
            var player = _db.Players.Find(playerId);
            if (player == null)
                return (ServiceResult<List<PlayerStats>>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            var stats = _db.PlayerStats.Where(x=>x.Player==player).ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats>> GetPlayerStatsWithGameNo(int gameNo)
        {
            var playerStats = _db.PlayerStats.Where(x=>x.GameNo==gameNo).ToList();
            if (playerStats.Count == 0)
                return (ServiceResult<List<PlayerStats>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(playerStats);
        }

        public ServiceResult<PlayerStats> GetPlayerStatsWithPlayerIdAndGameNo(int playerId, int gameNo)
        {
            var player = _db.Players.Find(playerId);
            if (player == null)
                return (ServiceResult<PlayerStats>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            var stats = _db.PlayerStats.First(x => x.Player == player && x.GameNo == gameNo);
            if (stats == null)
                return (ServiceResult<PlayerStats>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats>> GetPlayerStatsWithTeam(Team team)
        {
            var stats = _db.PlayerStats.Where(x => x.Team == team).ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats>>) ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<PlayerStats> GetPlayerStatWithId(int Id)
        {
            var stat = _db.PlayerStats.Find(Id);
            if (stat == null)
                return (ServiceResult<PlayerStats>) ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stat);
        }

        public ServiceResult UpdatePlayerStat(PlayerStats stats)
        {
            throw new System.NotImplementedException();
        }



        public ServiceResult AddPlayerStat20_21(PlayerStats20_21 stats)
        {
            _db.PlayerStats20_21.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeletePlayerStat20_21(int id)
        {
            var stats = _db.PlayerStats20_21.Find(id);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            _db.PlayerStats20_21.Remove(stats);
            return ServiceResult.Success();
        }

        public ServiceResult<List<PlayerStats20_21>> GetAllPlayerStats20_21()
        {
            var stats = _db.PlayerStats20_21.ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats20_21>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats20_21>> GetPlayerStatsWithPlayerId20_21(int playerId)
        {
            var player = _db.Players.Find(playerId);
            if (player == null)
                return (ServiceResult<List<PlayerStats20_21>>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            var stats = _db.PlayerStats20_21.Where(x => x.Player == player).ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats20_21>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats20_21>> GetPlayerStatsWithGameNo20_21(int gameNo)
        {
            var playerStats = _db.PlayerStats20_21.Where(x => x.GameNo == gameNo).ToList();
            if (playerStats.Count == 0)
                return (ServiceResult<List<PlayerStats20_21>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(playerStats);
        }

        public ServiceResult<PlayerStats20_21> GetPlayerStatsWithPlayerIdAndGameNo20_21(int playerId, int gameNo)
        {
            var player = _db.Players.Find(playerId);
            if (player == null)
                return (ServiceResult<PlayerStats20_21>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            var stats = _db.PlayerStats20_21.First(x => x.Player == player && x.GameNo == gameNo);
            if (stats == null)
                return (ServiceResult<PlayerStats20_21>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats20_21>> GetPlayerStatsWithTeam20_21(Team team)
        {
            var stats = _db.PlayerStats20_21.Where(x => x.Team == team).ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats20_21>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<PlayerStats20_21> GetPlayerStatWithId20_21(int Id)
        {
            var stat = _db.PlayerStats20_21.Find(Id);
            if (stat == null)
                return (ServiceResult<PlayerStats20_21>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stat);
        }

        public ServiceResult UpdatePlayerStat20_21(PlayerStats20_21 stats)
        {
            throw new System.NotImplementedException();
        }



        public ServiceResult AddPlayerStat19_20(PlayerStats19_20 stats)
        {
            _db.PlayerStats19_20.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeletePlayerStat19_20(int id)
        {
            var stats = _db.PlayerStats19_20.Find(id);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            _db.PlayerStats19_20.Remove(stats);
            return ServiceResult.Success();
        }

        public ServiceResult<List<PlayerStats19_20>> GetAllPlayerStats19_20()
        {
            var stats = _db.PlayerStats19_20.ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats19_20>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats19_20>> GetPlayerStatsWithPlayerId19_20(int playerId)
        {
            var player = _db.Players.Find(playerId);
            if (player == null)
                return (ServiceResult<List<PlayerStats19_20>>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            var stats = _db.PlayerStats19_20.Where(x => x.Player == player).ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats19_20>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats19_20>> GetPlayerStatsWithGameNo19_20(int gameNo)
        {
            var playerStats = _db.PlayerStats19_20.Where(x => x.GameNo == gameNo).ToList();
            if (playerStats.Count == 0)
                return (ServiceResult<List<PlayerStats19_20>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(playerStats);
        }

        public ServiceResult<PlayerStats19_20> GetPlayerStatsWithPlayerIdAndGameNo19_20(int playerId, int gameNo)
        {
            var player = _db.Players.Find(playerId);
            if (player == null)
                return (ServiceResult<PlayerStats19_20>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            var stats = _db.PlayerStats19_20.First(x => x.Player == player && x.GameNo == gameNo);
            if (stats == null)
                return (ServiceResult<PlayerStats19_20>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<List<PlayerStats19_20>> GetPlayerStatsWithTeam19_20(Team team)
        {
            var stats = _db.PlayerStats19_20.Where(x => x.Team == team).ToList();
            if (stats.Count == 0)
                return (ServiceResult<List<PlayerStats19_20>>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult<PlayerStats19_20> GetPlayerStatWithId19_20(int Id)
        {
            var stat = _db.PlayerStats19_20.Find(Id);
            if (stat == null)
                return (ServiceResult<PlayerStats19_20>)ServiceResult.Failed(ServiceError.PlayerStatNotFound);
            return ServiceResult.Success(stat);
        }

        public ServiceResult UpdatePlayerStat19_20(PlayerStats19_20 stats)
        {
            throw new System.NotImplementedException();
        }

    }
}
