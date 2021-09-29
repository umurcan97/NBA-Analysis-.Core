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
    public class GameTimesController : Controller
    {
        private readonly IGameTimesRepository _gameTimesRepository;

        public GameTimesController(IGameTimesRepository gameTimesRepository)
        {
            this._gameTimesRepository = gameTimesRepository;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addgame")]
        ServiceResult AddGame(GameTime gametime)
        {
            var serviceResult = _gameTimesRepository.AddGameTime(gametime);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updategame")]
        ServiceResult UpdateGame(GameTime gametime)
        {
            var serviceResult = _gameTimesRepository.UpdateGameTime(gametime);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletegame/{gameno}")]
        ServiceResult DeleteGame(int gameno)
        {
            var serviceResult = _gameTimesRepository.DeleteGameTime(gameno);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<GameTime>))]
        [HttpGet]
        [Route("getgame/{gameno}")]
        ServiceResult<GameTime> GetGameTime(int gameno)
        {
            var serviceResult = _gameTimesRepository.GetGameTime(gameno);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime>>))]
        [HttpGet]
        [Route("getforteam/{team}")]
        ServiceResult<List<GameTime>> GetFullSeasonForTeam(Team team)
        {
            var serviceresult = _gameTimesRepository.GetFullSeasonForTeam(team);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime>>))]
        [HttpGet]
        [Route("getall")]
        ServiceResult<List<GameTime>> GetFullSeason()
        {
            var serviceresult = _gameTimesRepository.GetFullSeason();
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime>>))]
        [HttpGet]
        [Route("gettoday")]
        ServiceResult<List<GameTime>> GetGamesToBePlayedToday()
        {
            var serviceresult = _gameTimesRepository.GetGamesToBePlayedToday();
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime>>))]
        [HttpGet]
        [Route("getsince/{date}")]
        ServiceResult<List<GameTime>> GetGamesSince(DateTime date)
        {
            var serviceresult = _gameTimesRepository.GetGamesSince(date);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime>>))]
        [HttpGet]
        [Route("gettill/{date}")]
        ServiceResult<List<GameTime>> GetGamesTill(DateTime date)
        {
            var serviceresult = _gameTimesRepository.GetGamesTill(date);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime>>))]
        [HttpGet]
        [Route("getbetween/{date1}&&{date2}")]
        ServiceResult<List<GameTime>> GetGamesBetween(DateTime date1, DateTime date2)
        {
            var serviceresult = _gameTimesRepository.GetGamesBetween(date1, date2);
            return serviceresult;
        }



        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addgame20_21")]
        ServiceResult AddGame20_21(GameTime20_21 gametime)
        {
            var serviceResult = _gameTimesRepository.AddGameTime20_21(gametime);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updategame20_21")]
        ServiceResult UpdateGame20_21(GameTime20_21 gametime)
        {
            var serviceResult = _gameTimesRepository.UpdateGameTime20_21(gametime);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletegame20_21/{gameno}")]
        ServiceResult DeleteGame20_21(int gameno)
        {
            var serviceResult = _gameTimesRepository.DeleteGameTime20_21(gameno);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<GameTime20_21>))]
        [HttpGet]
        [Route("getgame20_21/{gameno}")]
        ServiceResult<GameTime20_21> GetGameTime20_21(int gameno)
        {
            var serviceResult = _gameTimesRepository.GetGameTime20_21(gameno);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime20_21>>))]
        [HttpGet]
        [Route("getforteam20_21/{team}")]
        ServiceResult<List<GameTime20_21>> GetFullSeasonForTeam20_21(Team team)
        {
            var serviceresult = _gameTimesRepository.GetFullSeasonForTeam20_21(team);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime20_21>>))]
        [HttpGet]
        [Route("getall20_21")]
        ServiceResult<List<GameTime20_21>> GetFullSeason20_21()
        {
            var serviceresult = _gameTimesRepository.GetFullSeason20_21();
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime20_21>>))]
        [HttpGet]
        [Route("getsince20_21/{date}")]
        ServiceResult<List<GameTime20_21>> GetGamesSince20_21(DateTime date)
        {
            var serviceresult = _gameTimesRepository.GetGamesSince20_21(date);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime20_21>>))]
        [HttpGet]
        [Route("gettill20_21/{date}")]
        ServiceResult<List<GameTime20_21>> GetGamesTill20_21(DateTime date)
        {
            var serviceresult = _gameTimesRepository.GetGamesTill20_21(date);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime20_21>>))]
        [HttpGet]
        [Route("getbetween20_21/{date1}&&{date2}")]
        ServiceResult<List<GameTime20_21>> GetGamesBetween20_21(DateTime date1, DateTime date2)
        {
            var serviceresult = _gameTimesRepository.GetGamesBetween20_21(date1, date2);
            return serviceresult;
        }



        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addgame19_20")]
        ServiceResult AddGame19_20(GameTime19_20 gametime)
        {
            var serviceResult = _gameTimesRepository.AddGameTime19_20(gametime);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updategame19_20")]
        ServiceResult UpdateGame19_20(GameTime19_20 gametime)
        {
            var serviceResult = _gameTimesRepository.UpdateGameTime19_20(gametime);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletegame19_20/{gameno}")]
        ServiceResult DeleteGame19_20(int gameno)
        {
            var serviceResult = _gameTimesRepository.DeleteGameTime19_20(gameno);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<GameTime19_20>))]
        [HttpGet]
        [Route("getgame19_20/{gameno}")]
        ServiceResult<GameTime19_20> GetGameTime19_20(int gameno)
        {
            var serviceResult = _gameTimesRepository.GetGameTime19_20(gameno);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime19_20>>))]
        [HttpGet]
        [Route("getforteam19_20/{team}")]
        ServiceResult<List<GameTime19_20>> GetFullSeasonForTeam19_20(Team team)
        {
            var serviceresult = _gameTimesRepository.GetFullSeasonForTeam19_20(team);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime19_20>>))]
        [HttpGet]
        [Route("getall19_20")]
        ServiceResult<List<GameTime19_20>> GetFullSeason19_20()
        {
            var serviceresult = _gameTimesRepository.GetFullSeason19_20();
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime19_20>>))]
        [HttpGet]
        [Route("getsince19_20/{date}")]
        ServiceResult<List<GameTime19_20>> GetGamesSince19_20(DateTime date)
        {
            var serviceresult = _gameTimesRepository.GetGamesSince19_20(date);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime19_20>>))]
        [HttpGet]
        [Route("gettill19_20/{date}")]
        ServiceResult<List<GameTime19_20>> GetGamesTill19_20(DateTime date)
        {
            var serviceresult = _gameTimesRepository.GetGamesTill19_20(date);
            return serviceresult;
        }

        [ResponseType(typeof(ServiceResult<List<GameTime19_20>>))]
        [HttpGet]
        [Route("getbetween19_20/{date1}&&{date2}")]
        ServiceResult<List<GameTime19_20>> GetGamesBetween19_20(DateTime date1, DateTime date2)
        {
            var serviceresult = _gameTimesRepository.GetGamesBetween19_20(date1, date2);
            return serviceresult;
        }
    }
}
