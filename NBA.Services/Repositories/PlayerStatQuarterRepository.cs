using System;
using System.Linq;
using NBA.Models.DataContext;

namespace NBA.Services.Repositories
{
    using NBA.Models;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using System.Collections.Generic;

    public class PlayerStatQuarterRepository : IPlayerStatQuarterRepository
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;

        public PlayerStatQuarterRepository(INBAContext db, IUnitOfWork uw)
        {
            this._db = db;
            this._uw = uw;
        }

        public ServiceResult AddPlayerStatsQuarter(PlayerStatsQuarter stats)
        {
            _db.PlayerStatsQuarter.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult AddPlayerStatsQuarter19_20(PlayerStatsQuarter19_20 stats)
        {
            _db.PlayerStatsQuarter19_20.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult AddPlayerStatsQuarter20_21(PlayerStatsQuarter20_21 stats)
        {
            _db.PlayerStatsQuarter20_21.Add(stats);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeletePlayerStatsQuarter(int id)
        {
            try
            {
                var stats = _db.PlayerStatsQuarter.Find(id);
                _db.PlayerStatsQuarter.Remove(stats);
                _uw.Commit();
                return ServiceResult.Success();
            }
            catch (Exception)
            {
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            }
        }

        public ServiceResult DeletePlayerStatsQuarter19_20(int id)
        {
            try
            {
                var stats = _db.PlayerStatsQuarter19_20.Find(id);
                _db.PlayerStatsQuarter19_20.Remove(stats);
                _uw.Commit();
                return ServiceResult.Success();
            }
            catch (Exception)
            {
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            }
        }

        public ServiceResult DeletePlayerStatsQuarter20_21(int id)
        {
            try
            {
                var stats = _db.PlayerStatsQuarter20_21.Find(id);
                _db.PlayerStatsQuarter20_21.Remove(stats);
                _uw.Commit();
                return ServiceResult.Success();
            }
            catch (Exception)
            {
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            }
        }

        public ServiceResult GetAllPlayerStatsQuarter()
        {
            var stats = _db.PlayerStatsQuarter.ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetAllPlayerStatsQuarter19_20()
        {
            var stats = _db.PlayerStatsQuarter19_20.ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetAllPlayerStatsQuarter20_21()
        {
            var stats = _db.PlayerStatsQuarter20_21.ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithGameNo(int gameNo)
        {
            var stats = _db.PlayerStatsQuarter.Where(x => x.GameNo == gameNo).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithGameNo19_20(int gameNo)
        {
            var stats = _db.PlayerStatsQuarter19_20.Where(x => x.GameNo == gameNo).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithGameNo20_21(int gameNo)
        {
            var stats = _db.PlayerStatsQuarter20_21.Where(x => x.GameNo == gameNo).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithId(int Id)
        {
            try
            {
                var stats = _db.PlayerStatsQuarter.Find(Id);
                return ServiceResult.Success(stats);
            }
            catch (Exception)
            {
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            }
        }

        public ServiceResult GetPlayerStatsQuarterWithId19_20(int Id)
        {
            try
            {
                var stats = _db.PlayerStatsQuarter19_20.Find(Id);
                return ServiceResult.Success(stats);
            }
            catch (Exception)
            {
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            }
        }

        public ServiceResult GetPlayerStatsQuarterWithId20_21(int Id)
        {
            try
            {
                var stats = _db.PlayerStatsQuarter20_21.Find(Id);
                return ServiceResult.Success(stats);
            }
            catch (Exception)
            {
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            }
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerId(int playerId)
        {
            var stats = _db.PlayerStatsQuarter.Where(x => x.Player.Id == playerId).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerId19_20(int playerId)
        {
            var stats = _db.PlayerStatsQuarter19_20.Where(x => x.Player.Id == playerId).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerId20_21(int playerId)
        {
            var stats = _db.PlayerStatsQuarter20_21.Where(x => x.Player.Id == playerId).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNo(int playerId, int gameNo)
        {
            var stats = _db.PlayerStatsQuarter.Where(x => x.Player.Id == playerId && x.GameNo == gameNo).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNo19_20(int playerId, int gameNo)
        {
            var stats = _db.PlayerStatsQuarter19_20.Where(x => x.Player.Id == playerId && x.GameNo == gameNo).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNo20_21(int playerId, int gameNo)
        {
            var stats = _db.PlayerStatsQuarter20_21.Where(x => x.Player.Id == playerId && x.GameNo == gameNo).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo(int playerId, int gameNo, int quarterNo)
        {
            var stats = _db.PlayerStatsQuarter.FirstOrDefault(x => x.Player.Id == playerId && x.GameNo == gameNo && x.QuarterNo == quarterNo);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo19_20(int playerId, int gameNo, int quarterNo)
        {
            var stats = _db.PlayerStatsQuarter19_20.FirstOrDefault(x => x.Player.Id == playerId && x.GameNo == gameNo && x.QuarterNo == quarterNo);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithPlayerIdAndGameNoAndQuarterNo20_21(int playerId, int gameNo, int quarterNo)
        {
            var stats = _db.PlayerStatsQuarter20_21.FirstOrDefault(x => x.Player.Id == playerId && x.GameNo == gameNo && x.QuarterNo == quarterNo);
            if (stats == null)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithTeam(Team team)
        {
            var stats = _db.PlayerStatsQuarter.Where(x => x.Team == team).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithTeam19_20(Team team)
        {
            var stats = _db.PlayerStatsQuarter19_20.Where(x => x.Team == team).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult GetPlayerStatsQuarterWithTeam20_21(Team team)
        {
            var stats = _db.PlayerStatsQuarter20_21.Where(x => x.Player.Team == team).ToList();
            if (stats.Count == 0)
                return ServiceResult.Failed(ServiceError.PlayerStatsQuarterNotFound);
            return ServiceResult.Success(stats);
        }

        public ServiceResult UpdatePlayerStatsQuarter(PlayerStatsQuarter stats)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResult UpdatePlayerStatsQuarter19_20(PlayerStatsQuarter19_20 stats)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResult UpdatePlayerStatsQuarter20_21(PlayerStatsQuarter20_21 stats)
        {
            throw new System.NotImplementedException();
        }
    }
}
