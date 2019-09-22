using dot_NET_WebApiPractice.Data;
using dot_NET_WebApiPractice.Data.Entities;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace dot_NET_WebApiPractice.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/teams")]  
    public class Teams1Controller : ApiController
    {
        private readonly IBasketballRepository _repository = new BasketballRepository(new BasketballContext());

        public Teams1Controller() { }

        public Teams1Controller(IBasketballRepository repository)
        {
            _repository = repository;
        }

        [Route()]
        public async Task<IHttpActionResult> Post(Team team)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _repository.AddTeam(team);
                    if(await _repository.SaveChangesAsync())
                    {
                        
                        return CreatedAtRoute("GetTeamById", new { id = team.TeamId }, team);
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

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var team = await _repository.GetTeamByIdAsync(id);
                if(team==null)
                {
                    return NotFound();
                }
                _repository.DeleteTeam(team);
                if(await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /*
        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllTeamsAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        */

        [Route("{id:int}", Name = "GetTeamById")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var results = await _repository.GetTeamByIdAsync(id);
                if(results==null)
                {
                    return NotFound();
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}