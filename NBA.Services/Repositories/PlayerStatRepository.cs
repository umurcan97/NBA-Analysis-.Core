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
                return (ServiceResult<List<PlayerStats>>)ServiceResult.Failed(ServiceError.PlayerNotFound);
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
    }
}
