using dot_NET_WebApiPractice.Data;
using dot_NET_WebApiPractice.Data.Entities;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace dot_NET_WebApiPractice.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/players")]
    public class Players1Controller : ApiController
    {
        private readonly IBasketballRepository _repository = new BasketballRepository(new BasketballContext());

        public Players1Controller() { }

        public Players1Controller(IBasketballRepository repository)
        {
            _repository = repository;
        }

        [Route()]
        public async Task<IHttpActionResult> Post(Player player)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _repository.AddPlayer(player);

                    if(await _repository.SaveChangesAsync())
                    {
                        return CreatedAtRoute("GetPlayerById", new { id = player.PlayerId }, player);
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

        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllPlayersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{id:int}", Name = "GetPlayerById")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var player = await _repository.GetPlayerByIdAsync(id);
                if(player==null)
                {
                    return NotFound();
                }
                return Ok(player);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var player = await _repository.GetPlayerByIdAsync(id);
                if(player==null)
                {
                    return NotFound();
                }
                _repository.DeletePlayer(player);
                if(await _repository.SaveChangesAsync())
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