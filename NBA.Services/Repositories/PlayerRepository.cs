namespace NBA.Services.Abstraction.Repositories
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Services.Abstraction.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerRepository : IPlayerRepository
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;
        public PlayerRepository(INBAContext db, IUnitOfWork uw)
        {
            this._db = db;
            this._uw = uw;
        }
        public ServiceResult AddPlayer(string name, Team team, int number, string Position, int height, int weight, string country)
        {
            Players player = new()
            {
                Name = name,
                Team = team,
                Number = number,
                Position = Position,
                Height = height,
                Weight = weight,
                Country = country
            };
            _db.Players.AddAsync(player);
            return ServiceResult.Success();
        }

        public ServiceResult DeletePlayerWithId(int id)
        {
            Players player = _db.Players.FirstOrDefault(x => x.Id == id);
            if (player == null)
                return ServiceResult.Failed(ServiceError.RequestNotFound);
            _db.Players.Remove(player);
            _uw.Commit();
            return ServiceResult.Success();
        }

        public ServiceResult<List<Players>> GetAllPlayers()
        {
            List<Players> players = _db.Players.ToList();
            return ServiceResult.Success(players);
        }

        public ServiceResult<List<Players>> GetPlayersWithTeam(Team team)
        {
            List<Players> players = _db.Players.Where(x=>x.Team==team).ToList();
            if (players == null)
                return (ServiceResult<List<Players>>)ServiceResult.Failed(ServiceError.RequestNotFound);
            return ServiceResult.Success(players);
        }

        public ServiceResult<Players> GetPlayerWithId(int Id)
        {
            Players player = _db.Players.FirstOrDefault(x => x.Id == Id);
            if (player == null)
                return (ServiceResult<Players>)ServiceResult.Failed(ServiceError.RequestNotFound);
            return ServiceResult.Success(player);
        }

        public ServiceResult UpdatePlayerWithId(int id, string name, Team team, int number, string Position, int height, int weight, string country)
        {
            Players player = _db.Players.FirstOrDefault(x => x.Id == id);
            if (player == null)
                return ServiceResult.Failed(ServiceError.RequestNotFound);
            player.Name = name;
            player.Team = team;
            player.Number = number;
            player.Position = Position;
            player.Height = height;
            player.Weight = weight;
            player.Country = country;
            return ServiceResult.Success();
        }
    }
}
