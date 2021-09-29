using NBA.Models;

namespace NBA.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using NBA.Models.Entities;
    using System.Collections.Generic;
    using System.Web.Http.Description;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class FullSeasonController : Controller
    {
        private readonly IFullSeasonRepository _fullSeasonRepository;

        public FullSeasonController(IFullSeasonRepository fullSeasonRepository)
        {
            this._fullSeasonRepository = fullSeasonRepository;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addgame")]
        ServiceResult AddGame(FullSeason stats)
        {
            var serviceResult = _fullSeasonRepository.AddGame(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updategame")]
        ServiceResult UpdateGame(FullSeason stats)
        {
            var serviceResult = _fullSeasonRepository.UpdateGame(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<FullSeason>))]
        [HttpGet]
        [Route("getgame/{gameno}")]
        ServiceResult<FullSeason> GetGameWithGameNo(int gameNo)
        {
            var serviceResult = _fullSeasonRepository.GetGameWithGameNo(gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason>>))]
        [HttpGet]
        [Route("getall")]
        public ServiceResult<List<FullSeason>> GetAll()
        {
            var serviceResult = _fullSeasonRepository.GetFullSeason();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason>>))]
        [HttpGet]
        [Route("getgamessince/{date}")]
        public ServiceResult<List<FullSeason>> GetGamesSinceDate(DateTime date)
        {
            var serviceResult = _fullSeasonRepository.GetGamesSinceDate(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason>>))]
        [HttpGet]
        [Route("getgamestilldate/{date}")]
        public ServiceResult<List<FullSeason>> GetGamesTillDate(DateTime date)
        {
            var serviceResult = _fullSeasonRepository.GetGamesTillDate(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason>>))]
        [HttpGet]
        [Route("getgamesbetween/{date1}&&{date2}")]
        public ServiceResult<List<FullSeason>> GetGamesBetween(DateTime date1, DateTime date2)
        {
            var serviceResult = _fullSeasonRepository.GetGamesBetween(date1, date2);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason>>))]
        [HttpGet]
        [Route("getseasonwithteam/{team}")]
        public ServiceResult<List<FullSeason>> GetFullSeasonForTeam(Team team)
        {
            var serviceResult = _fullSeasonRepository.GetFullSeasonForTeam(team);
            return serviceResult;
        }



        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addgame20_21")]
        ServiceResult AddGame20_21(FullSeason20_21 stats)
        {
            var serviceResult = _fullSeasonRepository.AddGame20_21(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updategame20_21")]
        ServiceResult UpdateGame20_21(FullSeason20_21 stats)
        {
            var serviceResult = _fullSeasonRepository.UpdateGame20_21(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<FullSeason20_21>))]
        [HttpGet]
        [Route("getgame20_21/{gameno}")]
        ServiceResult<FullSeason20_21> GetGameWithGameNo20_21(int gameNo)
        {
            var serviceResult = _fullSeasonRepository.GetGameWithGameNo20_21(gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason20_21>>))]
        [HttpGet]
        [Route("getall20_21")]
        public ServiceResult<List<FullSeason20_21>> GetAll20_21()
        {
            var serviceResult = _fullSeasonRepository.GetFullSeason20_21();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason20_21>>))]
        [HttpGet]
        [Route("getgamessince20_21/{date}")]
        public ServiceResult<List<FullSeason20_21>> GetGamesSinceDate20_21(DateTime date)
        {
            var serviceResult = _fullSeasonRepository.GetGamesSinceDate20_21(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason20_21>>))]
        [HttpGet]
        [Route("getgamestilldate20_21/{date}")]
        public ServiceResult<List<FullSeason20_21>> GetGamesTillDate20_21(DateTime date)
        {
            var serviceResult = _fullSeasonRepository.GetGamesTillDate20_21(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason20_21>>))]
        [HttpGet]
        [Route("getgamesbetween20_21/{date1}&&{date2}")]
        public ServiceResult<List<FullSeason20_21>> GetGamesBetween20_21(DateTime date1, DateTime date2)
        {
            var serviceResult = _fullSeasonRepository.GetGamesBetween20_21(date1, date2);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason20_21>>))]
        [HttpGet]
        [Route("getseasonwithteam20_21/{team}")]
        public ServiceResult<List<FullSeason20_21>> GetFullSeasonForTeam20_21(Team team)
        {
            var serviceResult = _fullSeasonRepository.GetFullSeasonForTeam20_21(team);
            return serviceResult;
        }



        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addgame19_20")]
        ServiceResult AddGame19_20(FullSeason19_20 stats)
        {
            var serviceResult = _fullSeasonRepository.AddGame19_20(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updategame19_20")]
        ServiceResult UpdateGame19_20(FullSeason19_20 stats)
        {
            var serviceResult = _fullSeasonRepository.UpdateGame19_20(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<FullSeason19_20>))]
        [HttpGet]
        [Route("getgame19_20/{gameno}")]
        ServiceResult<FullSeason19_20> GetGameWithGameNo19_20(int gameNo)
        {
            var serviceResult = _fullSeasonRepository.GetGameWithGameNo19_20(gameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason19_20>>))]
        [HttpGet]
        [Route("getall19_20")]
        public ServiceResult<List<FullSeason19_20>> GetAll19_20()
        {
            var serviceResult = _fullSeasonRepository.GetFullSeason19_20();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason19_20>>))]
        [HttpGet]
        [Route("getgamessince19_20/{date}")]
        public ServiceResult<List<FullSeason19_20>> GetGamesSinceDate19_20(DateTime date)
        {
            var serviceResult = _fullSeasonRepository.GetGamesSinceDate19_20(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason19_20>>))]
        [HttpGet]
        [Route("getgamestilldate19_20/{date}")]
        public ServiceResult<List<FullSeason19_20>> GetGamesTillDate19_20(DateTime date)
        {
            var serviceResult = _fullSeasonRepository.GetGamesTillDate19_20(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason19_20>>))]
        [HttpGet]
        [Route("getgamesbetween19_20/{date1}&&{date2}")]
        public ServiceResult<List<FullSeason19_20>> GetGamesBetween19_20(DateTime date1, DateTime date2)
        {
            var serviceResult = _fullSeasonRepository.GetGamesBetween19_20(date1, date2);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeason19_20>>))]
        [HttpGet]
        [Route("getseasonwithteam19_20/{team}")]
        public ServiceResult<List<FullSeason19_20>> GetFullSeasonForTeam19_20(Team team)
        {
            var serviceResult = _fullSeasonRepository.GetFullSeasonForTeam19_20(team);
            return serviceResult;
        }
    }
}
