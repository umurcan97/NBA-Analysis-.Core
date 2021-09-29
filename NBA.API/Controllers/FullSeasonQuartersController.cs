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
    public class FullSeasonQuartersController : Controller
    {
        private readonly IFullSeasonQuartersRepository _fullSeasonQuartersRepository;

        public FullSeasonQuartersController(IFullSeasonQuartersRepository fullSeasonQuartersRepository)
        {
            this._fullSeasonQuartersRepository = fullSeasonQuartersRepository;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addquarter")]
        public ServiceResult AddQuarter(FullSeasonQuarters stats)
        {
            var serviceResult = _fullSeasonQuartersRepository.AddQuarter(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updatequarter")]
        public ServiceResult UpdateQuarter(FullSeasonQuarters stats)
        {
            var serviceResult = _fullSeasonQuartersRepository.UpdateQuarter(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletequarter/{gameno}&&{quarterno}")]
        public ServiceResult DeleteQuarter(int gameNo, int quarterNo)
        {
            var serviceResult = _fullSeasonQuartersRepository.DeleteQuarter(gameNo, quarterNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletequarter/{id}")]
        public ServiceResult DeleteQuarter(int id)
        {
            var serviceResult = _fullSeasonQuartersRepository.DeleteQuarter(id);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters>>))]
        [HttpGet]
        [Route("getforgame/{GameNo}")]
        public ServiceResult<List<FullSeasonQuarters>> GetQuartersWithGameNo(int GameNo)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersWithGameNo(GameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters>>))]
        [HttpGet]
        [Route("getall")]
        public ServiceResult<List<FullSeasonQuarters>> GetFullSeasonQuarters()
        {
            var serviceResult = _fullSeasonQuartersRepository.GetFullSeasonQuarters();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters>>))]
        [HttpGet]
        [Route("getsince/{date}")]
        public ServiceResult<List<FullSeasonQuarters>> GetQuartersSinceDate(DateTime date)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersSinceDate(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters>>))]
        [HttpGet]
        [Route("gettill/{date}")]
        public ServiceResult<List<FullSeasonQuarters>> GetQuartersTillDate(DateTime date)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersTillDate(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters>>))]
        [HttpGet]
        [Route("getbetween/{date1}&&{date2}")]
        public ServiceResult<List<FullSeasonQuarters>> GetQuartersBetween(DateTime date1, DateTime date2)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersBetween(date1, date2);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters>>))]
        [HttpGet]
        [Route("getforteam/{team}")]
        public ServiceResult<List<FullSeasonQuarters>> GetFullSeasonQuartersForTeam(Team team)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetFullSeasonQuartersForTeam(team);
            return serviceResult;
        }



        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addquarter20_21")]
        public ServiceResult AddQuarter20_21(FullSeasonQuarters20_21 stats)
        {
            var serviceResult = _fullSeasonQuartersRepository.AddQuarter20_21(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updatequarter20_21")]
        public ServiceResult UpdateQuarter20_21(FullSeasonQuarters20_21 stats)
        {
            var serviceResult = _fullSeasonQuartersRepository.UpdateQuarter20_21(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletequarter20_21/{gameno}&&{quarterno}")]
        public ServiceResult DeleteQuarter20_21(int gameNo, int quarterNo)
        {
            var serviceResult = _fullSeasonQuartersRepository.DeleteQuarter20_21(gameNo, quarterNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletequarter20_21/{id}")]
        public ServiceResult DeleteQuarter20_21(int id)
        {
            var serviceResult = _fullSeasonQuartersRepository.DeleteQuarter20_21(id);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters20_21>>))]
        [HttpGet]
        [Route("getforgame20_21/{GameNo}")]
        public ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersWithGameNo20_21(int GameNo)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersWithGameNo20_21(GameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters20_21>>))]
        [HttpGet]
        [Route("getall20_21")]
        public ServiceResult<List<FullSeasonQuarters20_21>> GetFullSeasonQuarters20_21()
        {
            var serviceResult = _fullSeasonQuartersRepository.GetFullSeasonQuarters20_21();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters20_21>>))]
        [HttpGet]
        [Route("getsince20_21/{date}")]
        public ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersSinceDate20_21(DateTime date)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersSinceDate20_21(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters20_21>>))]
        [HttpGet]
        [Route("gettill20_21/{date}")]
        public ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersTillDate20_21(DateTime date)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersTillDate20_21(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters20_21>>))]
        [HttpGet]
        [Route("getbetween20_21/{date1}&&{date2}")]
        public ServiceResult<List<FullSeasonQuarters20_21>> GetQuartersBetween20_21(DateTime date1, DateTime date2)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersBetween20_21(date1, date2);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters20_21>>))]
        [HttpGet]
        [Route("getforteam20_21/{team}")]
        public ServiceResult<List<FullSeasonQuarters20_21>> GetFullSeasonQuartersForTeam20_21(Team team)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetFullSeasonQuartersForTeam20_21(team);
            return serviceResult;
        }



        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addquarter19_20")]
        public ServiceResult AddQuarter19_20(FullSeasonQuarters19_20 stats)
        {
            var serviceResult = _fullSeasonQuartersRepository.AddQuarter19_20(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updatequarter19_20")]
        public ServiceResult UpdateQuarter19_20(FullSeasonQuarters19_20 stats)
        {
            var serviceResult = _fullSeasonQuartersRepository.UpdateQuarter19_20(stats);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletequarter19_20/{gameno}&&{quarterno}")]
        public ServiceResult DeleteQuarter19_20(int gameNo, int quarterNo)
        {
            var serviceResult = _fullSeasonQuartersRepository.DeleteQuarter19_20(gameNo, quarterNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deletequarter19_20/{id}")]
        public ServiceResult DeleteQuarter19_20(int id)
        {
            var serviceResult = _fullSeasonQuartersRepository.DeleteQuarter19_20(id);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters19_20>>))]
        [HttpGet]
        [Route("getforgame19_20/{GameNo}")]
        public ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersWithGameNo19_20(int GameNo)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersWithGameNo19_20(GameNo);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters19_20>>))]
        [HttpGet]
        [Route("getall19_20")]
        public ServiceResult<List<FullSeasonQuarters19_20>> GetFullSeasonQuarters19_20()
        {
            var serviceResult = _fullSeasonQuartersRepository.GetFullSeasonQuarters19_20();
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters19_20>>))]
        [HttpGet]
        [Route("getsince19_20/{date}")]
        public ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersSinceDate19_20(DateTime date)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersSinceDate19_20(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters19_20>>))]
        [HttpGet]
        [Route("gettill19_20/{date}")]
        public ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersTillDate19_20(DateTime date)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersTillDate19_20(date);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters19_20>>))]
        [HttpGet]
        [Route("getbetween19_20/{date1}&&{date2}")]
        public ServiceResult<List<FullSeasonQuarters19_20>> GetQuartersBetween19_20(DateTime date1, DateTime date2)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetQuartersBetween19_20(date1, date2);
            return serviceResult;
        }

        [ResponseType(typeof(ServiceResult<List<FullSeasonQuarters19_20>>))]
        [HttpGet]
        [Route("getforteam19_20/{team}")]
        public ServiceResult<List<FullSeasonQuarters19_20>> GetFullSeasonQuartersForTeam19_20(Team team)
        {
            var serviceResult = _fullSeasonQuartersRepository.GetFullSeasonQuartersForTeam19_20(team);
            return serviceResult;
        }
    }
}
