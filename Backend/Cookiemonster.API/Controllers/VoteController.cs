using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPatches;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace Cookiemonster.API.Controllers
{
    [Route("votes")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IRepository<Vote> _voteRepository;
        private readonly IMapper _mapper;

        public VoteController(IRepository<Vote> voteRepository, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
        }

        // GET: api/votes
        [HttpGet("AllVotes")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all votes",
            Description = "Retrieves all votes.",
            OperationId = "GetAllVotes"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        public ActionResult<IEnumerable<VoteDTO>> GetAllVotes()
        {
            var votes = _voteRepository.GetAll();
            return Ok(_mapper.Map<List<VoteDTO>>(votes));
        }

        // GET: api/votes/5-4
        [HttpGet("VoteById/{recipeId}-{userId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get a vote by RecipeId and UserId",
            Description = "Retrieves a vote by RecipeId and UserId.",
            OperationId = "GetVoteById"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        public ActionResult<VoteDTO> GetVoteById(int recipeId, int userId)
        {
            var vote = _voteRepository.Get(recipeId, userId);
            if (vote == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VoteDTO>(vote));
        }

        // POST: api/votes
        [HttpPost("Vote")]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Create a new vote",
            Description = "Creates a new vote.",
            OperationId = "CreateVote"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        public ActionResult CreateVote(VoteDTO vote)
        {
            if (vote == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdVote = _voteRepository.Create(_mapper.Map<Vote>(vote));
            return CreatedAtAction(nameof(GetVoteById), new { recipeId = createdVote.RecipeId, userId = createdVote.UserId }, vote);
        }

        /*
        [HttpPatch("Vote/{recipeId}-{userId}")]
        public ActionResult PatchVote(int recipeId, int userId, [FromBody] VoteDTOPatch vote)
        {
            if (vote == null or !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var previousVote = _voteRepository.Get(recipeId, userId);
            if (previousVote == null)
            {
                return NotFound();
            }

            Vote mappedVote = _mapper.Map<Vote>(vote);
            mappedVote.RecipeId = recipeId;
            mappedVote.UserId = userId;

            _voteRepository.Update(mappedVote, x => new { x.RecipeId, x.UserId });

            return Ok();
        }
        */

        // DELETE: api/votes/5-4
        [HttpDelete("Vote/{recipeId}-{userId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete a vote by RecipeId and UserId",
            Description = "Deletes a vote by RecipeId and UserId.",
            OperationId = "DeleteVote"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
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
