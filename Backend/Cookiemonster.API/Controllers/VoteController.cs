using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<VoteDTO>>> GetAllVotesAsync()
        {
            var votes = await _voteRepository.GetAllAsync();
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
        public async Task<ActionResult<VoteDTO>> GetVoteByIdAsync(int recipeId, int userId)
        {
            var vote = await _voteRepository.GetAsync(recipeId, userId);
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
        public async Task<ActionResult> CreateVoteAsync([FromBody] VoteDTO voteDto)
        {
            if (voteDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vote = _mapper.Map<Vote>(voteDto);
            var createdVote = await _voteRepository.CreateAsync(vote);
            return CreatedAtAction(nameof(GetVoteByIdAsync), new { recipeId = createdVote.RecipeId, userId = createdVote.UserId }, _mapper.Map<VoteDTO>(createdVote));
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
        public async Task<ActionResult> DeleteVoteAsync(int recipeId, int userId)
        {
            var deleted = await _voteRepository.DeleteAsync(recipeId, userId);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}