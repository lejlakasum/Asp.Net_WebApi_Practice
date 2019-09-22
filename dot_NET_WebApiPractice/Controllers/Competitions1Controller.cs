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
    [RoutePrefix("api/competitions")]
    public class Competitions1Controller : ApiController
    {
        private readonly IBasketballRepository _repository = new BasketballRepository(new BasketballContext());


        public Competitions1Controller(IBasketballRepository repository)
        {
            _repository = repository;

        }

        public Competitions1Controller() { }

        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCompetitionsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Route("{id:int}", Name = "GetCompetitionById")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Competition competition = await _repository.GetCompetitionByIdAsync(id);
                if (competition == null) return NotFound();
                return Ok(competition);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route()]
        public async Task<IHttpActionResult> Post(Competition competition)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.AddCompetition(competition);
                    if (await _repository.SaveChangesAsync())
                    {
                        return CreatedAtRoute("GetCompetitionById", new { id = competition.CompetitionId }, competition);
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

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var competition = await _repository.GetCompetitionByIdAsync(id);
                if (competition == null) return NotFound();
                _repository.DeleteCompetition(competition);
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
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, Competition competition)
        {
            try
            {
                var comp = _repository.GetCompetitionByIdAsync(id);
                if (comp == null) return NotFound();
                if(ModelState.IsValid)
                {
                    comp=competition;
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(comp);
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
        */

        


    }
}