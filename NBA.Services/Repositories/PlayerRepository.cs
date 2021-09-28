﻿namespace NBA.Services.Abstraction.Repositories
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using System.Collections.Generic;
    using System.Linq;
    using NBA.Services.Interfaces;

    public class PlayerRepository : IPlayerRepository
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;
        public PlayerRepository(INBAContext db, IUnitOfWork uw)
        {
            this._db = db;
            this._uw = uw;
        }

        public ServiceResult<List<Players>> GetAllPlayers()
        {
            List<Players> players = _db.Players.ToList();
            return ServiceResult.Success(players);
        }

        public ServiceResult<List<Players>> GetPlayersWithTeam(Team team)
        {
            List<Players> players = _db.Players.Where(x => x.Team == team).ToList();
            if (players.Count == 0)
                return (ServiceResult<List<Players>>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            return ServiceResult.Success(players);
        }

        public ServiceResult<Players> GetPlayerWithId(int Id)
        {
            Players player = _db.Players.FirstOrDefault(x => x.Id == Id);
            if (player == null)
                return (ServiceResult<Players>)ServiceResult.Failed(ServiceError.PlayerNotFound);
            return ServiceResult.Success(player);
        }

        public ServiceResult<Players> GetPlayerWithName(string name)
        {
            var player = this._db.Players.FirstOrDefault(x => x.Name == name);
            if (player.Name == "")
                return (ServiceResult<Players>) ServiceResult.Failed(ServiceError.PlayerNotFound);
            return ServiceResult.Success(player);
        }

        public ServiceResult AddPlayer(Players player)
        {
            _db.Players.AddAsync(player);
            _uw.Commit();
            return ServiceResult.Success();
        }
        public ServiceResult AddPlayerList(List<Players> players)
        {
            _db.Players.AddRange(players);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult UpdatePlayer(Players player)
        {
            Players old = _db.Players.First(x => x.Id == player.Id);
            old.Name = player.Name;
            old.Team = player.Team;
            old.Number = player.Number;
            old.Position = player.Position;
            old.Height = player.Height;
            old.Weight = player.Weight;
            old.Country = player.Country;
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult DeletePlayer(int id)
        {
            Players player = _db.Players.FirstOrDefault(x => x.Id == id);
            if (player == null)
                return ServiceResult.Failed(ServiceError.PlayerNotFound);
            _db.Players.Remove(player);
            _uw.Commit();
            return ServiceResult.Success();
        }
    }
}
