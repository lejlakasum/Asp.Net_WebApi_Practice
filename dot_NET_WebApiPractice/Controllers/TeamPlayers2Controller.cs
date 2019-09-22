using AutoMapper;
using dot_NET_WebApiPractice.Data;
using dot_NET_WebApiPractice.Data.Entities;
using dot_NET_WebApiPractice.Models;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace dot_NET_WebApiPractice.Controllers
{
    [ApiVersion("2.0")]
    [RoutePrefix("api/teams/{moniker}/players")]
    public class TeamPlayers2Controller : ApiController
    {
        private readonly IBasketballRepository _repository = new BasketballRepository(new BasketballContext());
        static private readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TeamMappingProfile());
            cfg.AddProfile(new PlayerMappingProfile());
        }
            );
        private readonly IMapper _mapper = new Mapper(config);
        public TeamPlayers2Controller() { }

        public TeamPlayers2Controller(IBasketballRepository repository, IMapper mapper)
        {
            _repository = repository;

        }

        [Route()]
        public async Task<IHttpActionResult> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetAllPlayersByTeamAsync(moniker);
                return Ok(_mapper.Map<IEnumerable<PlayerModel>>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{id:int}", Name = "GetPlayerByTeam")]
        public async Task<IHttpActionResult> Get(string moniker, int id)
        {
            try
            {
                var result = await _repository.GetPlayerByTeamAsync(moniker, id);
                if (result == null) return NotFound();
                return Ok(_mapper.Map<PlayerModel>(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route()]
        public async Task<IHttpActionResult> Post(string moniker, PlayerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var team = await _repository.GetTeamByMonikerAsync(moniker);
                    if (team == null) return NotFound();
                    var player = _mapper.Map<Player>(model);
                    player.Team = team;
                    _repository.AddPlayer(player);
                    if (await _repository.SaveChangesAsync())
                    {
                        var newModel = _mapper.Map<PlayerModel>(player);
                        return CreatedAtRoute("GetPlayerByTeam", new { id = player.PlayerId }, newModel);
                    }
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(string moniker, int id, PlayerModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var player = await _repository.GetPlayerByTeamAsync(moniker, id);
                    if (player == null) return NotFound();
                    _mapper.Map(model, player);
                    if(await _repository.SaveChangesAsync())
                    {
                        var newModel = _mapper.Map<PlayerModel>(player);
                        return Ok(newModel);
                    }
                    return InternalServerError();
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(string moniker, int id)
        {
            try
            {
                    var player = await _repository.GetPlayerByTeamAsync(moniker, id);
                    if (player == null) return NotFound();
                    _repository.DeletePlayer(player);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok();
                    }
                    return InternalServerError();
                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}