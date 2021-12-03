namespace NBA.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NBA.Models;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using NBA.Models.Entities;
    using System.Collections.Generic;
    using System.Web.Http.Description;

    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatsController : Controller
    {
        private readonly IPlayerStatRepository _playerStatRepository;

        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addplayerstats")]
        public ServiceResult AddPlayerStats(PlayerStats stats)
        {
            var serviceResult = _playerStatRepository.AddPlayerStat(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updateplayerstats")]
        public ServiceResult UpdatePlayerStats(PlayerStats stats)
        {
            var serviceResult = _playerStatRepository.UpdatePlayerStat(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deleteplayerstats")]
        public ServiceResult DeletePlayerStats(int id)
        {
            var serviceResult = _playerStatRepository.DeletePlayerStat(id);
            return serviceResult;
        }

        public PlayerStatsController(IPlayerStatRepository playerStatRepository)
        {
            this._playerStatRepository = playerStatRepository;
        }
        [ResponseType(typeof(ServiceResult<List<PlayerStats>>))]
        [HttpGet]
        [Route("getall")]
        public ServiceResult GetAll()
        {
            var serviceResult = _playerStatRepository.GetAllPlayerStats();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats>>))]
        [HttpGet]
        [Route("getplayerstatswithplayerid/{playerId}")]
        public ServiceResult GetPlayerStatsWithPlayerId(int playerId)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerId(playerId);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats>>))]
        [HttpGet]
        [Route("getplayerstatswithgameno/{gameNo}")]
        public ServiceResult GetPlayerStatsWithGameNo(int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithGameNo(gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<PlayerStats>))]
        [HttpGet]
        [Route("getplayerstatswithplayeridandgameno/{playerId}&&{gameNo}")]
        public ServiceResult GetPlayerStatsWithPlayerIdandGameNo(int playerId, int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerIdAndGameNo(playerId, gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats>>))]
        [HttpGet]
        [Route("getplayerstatswithteam")]
        public ServiceResult GetPlayerStatsWithTeam(Team team)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithTeam(team);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addplayerstats20_21")]
        public ServiceResult AddPlayerStats20_21(PlayerStats20_21 stats)
        {
            var serviceResult = _playerStatRepository.AddPlayerStat20_21(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updateplayerstats20_21")]
        public ServiceResult UpdatePlayerStats20_21(PlayerStats20_21 stats)
        {
            var serviceResult = _playerStatRepository.UpdatePlayerStat20_21(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deleteplayerstats20_21")]
        public ServiceResult DeletePlayerStats20_21(int id)
        {
            var serviceResult = _playerStatRepository.DeletePlayerStat20_21(id);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats20_21>>))]
        [HttpGet]
        [Route("getall20_21")]
        public ServiceResult GetAll20_21()
        {
            var serviceResult = _playerStatRepository.GetAllPlayerStats20_21();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats20_21>>))]
        [HttpGet]
        [Route("getplayerstatswithplayerid20_21/{playerId}")]
        public ServiceResult GetPlayerStatsWithPlayerId20_21(int playerId)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerId20_21(playerId);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats20_21>>))]
        [HttpGet]
        [Route("getplayerstatswithgameno20_21/{gameNo}")]
        public ServiceResult GetPlayerStatsWithGameNo20_21(int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithGameNo20_21(gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<PlayerStats20_21>))]
        [HttpGet]
        [Route("getplayerstatswithplayeridandgameno20_21/{playerId}&&{gameNo}")]
        public ServiceResult GetPlayerStatsWithPlayerIdandGameNo20_21(int playerId, int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerIdAndGameNo20_21(playerId, gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats20_21>>))]
        [HttpGet]
        [Route("getplayerstatswithteam20_21")]
        public ServiceResult GetPlayerStatsWithTeam20_21(Team team)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithTeam20_21(team);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addplayerstats19_20")]
        public ServiceResult AddPlayerStats19_20(PlayerStats19_20 stats)
        {
            var serviceResult = _playerStatRepository.AddPlayerStat19_20(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updateplayerstats19_20")]
        public ServiceResult UpdatePlayerStats19_20(PlayerStats19_20 stats)
        {
            var serviceResult = _playerStatRepository.UpdatePlayerStat19_20(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deleteplayerstats19_20")]
        public ServiceResult DeletePlayerStats19_20(int id)
        {
            var serviceResult = _playerStatRepository.DeletePlayerStat19_20(id);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats19_20>>))]
        [HttpGet]
        [Route("getall19_20")]
        public ServiceResult GetAll19_20()
        {
            var serviceResult = _playerStatRepository.GetAllPlayerStats19_20();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats19_20>>))]
        [HttpGet]
        [Route("getplayerstatswithplayerid19_20/{playerId}")]
        public ServiceResult GetPlayerStatsWithPlayerId19_20(int playerId)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerId19_20(playerId);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats19_20>>))]
        [HttpGet]
        [Route("getplayerstatswithgameno19_20/{gameNo}")]
        public ServiceResult GetPlayerStatsWithGameNo19_20(int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithGameNo19_20(gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<PlayerStats19_20>))]
        [HttpGet]
        [Route("getplayerstatswithplayeridandgameno19_20/{playerId}&&{gameNo}")]
        public ServiceResult GetPlayerStatsWithPlayerIdandGameNo19_20(int playerId, int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerIdAndGameNo19_20(playerId, gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats19_20>>))]
        [HttpGet]
        [Route("getplayerstatswithteam19_20")]
        public ServiceResult GetPlayerStatsWithTeam19_20(Team team)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithTeam19_20(team);
            return serviceResult;
        }
    }
}
