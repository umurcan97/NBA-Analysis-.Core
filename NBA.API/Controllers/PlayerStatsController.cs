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
        public ServiceResult<List<PlayerStats>> GetAll()
        {
            var serviceResult = _playerStatRepository.GetAllPlayerStats();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats>>))]
        [HttpGet]
        [Route("getplayerstatswithplayerid/{playerId}")]
        public ServiceResult<List<PlayerStats>> GetPlayerStatsWithPlayerId(int playerId)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerId(playerId);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats>>))]
        [HttpGet]
        [Route("getplayerstatswithgameno/{gameNo}")]
        public ServiceResult<List<PlayerStats>> GetPlayerStatsWithGameNo(int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithGameNo(gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<PlayerStats>))]
        [HttpGet]
        [Route("getplayerstatswithplayeridandgameno/{playerId}&&{gameNo}")]
        public ServiceResult<PlayerStats> GetPlayerStatsWithPlayerIdandGameNo(int playerId, int gameNo)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithPlayerIdAndGameNo(playerId, gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<PlayerStats>>))]
        [HttpGet]
        [Route("getplayerstatswithteam")]
        public ServiceResult<List<PlayerStats>> GetPlayerStatsWithTeam(Team team)
        {
            var serviceResult = _playerStatRepository.GetPlayerStatsWithTeam(team);
            return serviceResult;
        }
    }
}
