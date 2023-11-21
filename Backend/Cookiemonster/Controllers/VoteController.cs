using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.Controllers
{
    [Route("api/votes")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly VoteRepository _voteRepository;

        public VoteController(VoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        // GET: api/votes
        [HttpGet]
        public ActionResult<IEnumerable<Vote>> Get()
        {
            var votes = _voteRepository.GetAll();
            return Ok(votes);
        }

        // GET: api/votes/{recipeId}/{userId}
        [HttpGet("{recipeId}/{userId}")]
        public ActionResult<Vote> Get(int recipeId, int userId)
        {
            var vote = _voteRepository.Get(recipeId, userId);
            if (vote == null)
            {
                return NotFound();
            }
            return Ok(vote);
        }

        // POST: api/votes
        [HttpPost]
        public ActionResult CreateVote(Vote vote)
        {
            _voteRepository.Create(vote);
            return Ok();
        }

        // DELETE: api/votes/{recipeId}/{userId}
        [HttpDelete("{recipeId}/{userId}")]
        public ActionResult DeleteVote(int recipeId, int userId)
        {
            var deleted = _voteRepository.Delete(recipeId, userId);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}