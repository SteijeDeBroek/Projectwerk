using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.API.Controllers
{
    [Route("votes")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IRepository<Vote> _voteRepository;

        public VoteController(IRepository<Vote> voteRepository)
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

        // GET: api/votes/5-4
        [HttpGet("{recipeId}-{userId}")]
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
            if (vote == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _voteRepository.Create(vote);
            return CreatedAtAction(nameof(Get), new { id = (vote.RecipeId, vote.UserId) }, vote);
        }

        // PATCH: api/votes/5-4
        [HttpPatch("{recipeId}-{userId}")]
        public ActionResult PatchRecipe(int recipeId, int userId, [FromBody] Vote vote)
        {
            if (vote == null || vote.RecipeId != recipeId || vote.UserId != userId || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _voteRepository.Update(vote);
            return Ok();
        }

        // DELETE: api/votes/5-4
        [HttpDelete("{recipeId}-{userId}")]
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