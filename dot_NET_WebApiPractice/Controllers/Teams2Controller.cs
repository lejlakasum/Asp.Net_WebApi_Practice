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
    [RoutePrefix("api/teams")]
    public class Teams2Controller : ApiController
    {
        private readonly IBasketballRepository _repository = new BasketballRepository(new BasketballContext());
        static private readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TeamMappingProfile());
            cfg.AddProfile(new PlayerMappingProfile());
        }
            );
        private readonly IMapper _mapper = new Mapper(config);
        public Teams2Controller() { }

        public Teams2Controller(IBasketballRepository repository, IMapper mapper)
        {
            _repository = repository;
            
        }

        [Route()]
        public async Task<IHttpActionResult> Get(bool includePlayers = false)
        {
            try
            {
                var results = await _repository.GetAllTeamsAsync(includePlayers);
                var mappedResult = _mapper.Map<IEnumerable<TeamModel>>(results);
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{moniker}", Name = "GetTeamByMoniker")]
        public async Task<IHttpActionResult> Get(string moniker, bool includePlayers=false)
        {
            try
            {
                var results = await _repository.GetTeamByMonikerAsync(moniker, includePlayers);
                if (results == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<TeamModel>(results));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route()]
        public async Task<IHttpActionResult> Post(TeamModel model)
        {
            try
            {
                if(await _repository.GetTeamByMonikerAsync(model.Moniker)!=null)
                {
                    ModelState.AddModelError("Moniker", "Moniker must be unique!");
                }
                if (ModelState.IsValid)
                {
                    Team team = _mapper.Map<Team>(model);
                    _repository.AddTeam(team);
                    if (await _repository.SaveChangesAsync())
                    {
                        TeamModel newModel = _mapper.Map<TeamModel>(team);
                        return CreatedAtRoute("GetTeamByMoniker", new { moniker = newModel.Moniker }, newModel);
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Put(string moniker, TeamModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var team = await _repository.GetTeamByMonikerAsync(moniker);
                    if (team == null) return NotFound();

                    _mapper.Map(model, team);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(_mapper.Map<TeamModel>(team));
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
                
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Delete(string moniker)
        {
            try
            {
                var team = await _repository.GetTeamByMonikerAsync(moniker);
                if (team == null)
                {
                    return NotFound();
                }
                _repository.DeleteTeam(team);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}