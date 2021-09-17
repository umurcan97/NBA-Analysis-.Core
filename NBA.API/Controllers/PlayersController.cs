namespace NBA.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NBA.API.DTO;
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Services.Abstraction;
    using NBA.Services.Abstraction.Interfaces;
    using System.Collections.Generic;
    using System.Web.Http.Description;

    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly INBAContext _db;
        private readonly IUnitOfWork _uw;
        private readonly IPlayerRepository _playerRepository;
        public PlayersController(
            INBAContext db,
            IUnitOfWork uw,
            IPlayerRepository playerRepository
            )
        {
            _db = db;
            _uw = uw;
            _playerRepository = playerRepository;
        }
        [ResponseType(typeof(ServiceResult<List<Players>>))]
        [HttpGet]
        [Route("getall")]
        public ServiceResult<List<Players>> GetAll()
        {
            ServiceResult<List<Players>> serviceResult = _playerRepository.GetAllPlayers();
            return serviceResult;
        }
        [ResponseType(typeof(ServiceResult))]
        [HttpPost]
        [Route("addplayer")]
        public ServiceResult AddPlayer(AddPlayerModel model)
        {
            ServiceResult serviceResult = _playerRepository.AddPlayer(model.Name,model.Team,model.Number,model.Position,model.Height,model.Weight,model.Country);
            return serviceResult;
        }
        [ResponseType(typeof(ServiceResult))]
        [HttpPut]
        [Route("updateplayer")]
        public ServiceResult UpdatePlayer(int id,UpdatePlayerModel model)
        {
            ServiceResult serviceResult = _playerRepository.UpdatePlayerWithId(id, model.Name, model.Team, model.Number, model.Position, model.Height, model.Weight, model.Country);
            return serviceResult;
        }
        [ResponseType(typeof(ServiceResult))]
        [HttpDelete]
        [Route("deleteplayer")]
        public ServiceResult DeletePlayer (int id)
        {
            ServiceResult serviceResult = _playerRepository.DeletePlayerWithId(id);
            return serviceResult;
        }
    }
}
