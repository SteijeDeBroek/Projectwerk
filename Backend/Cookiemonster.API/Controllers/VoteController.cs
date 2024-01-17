using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
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

        [HttpGet("AllVotes")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all votes",
            Description = "Retrieves all votes.",
            OperationId = "GetAllVotes")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Votes not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<IEnumerable<VoteDTO>>> GetAllVotesAsync()
        {
            try
            {
                var votes = await _voteRepository.GetAllAsync();
                if (votes == null) return NotFound();
                return Ok(_mapper.Map<List<VoteDTO>>(votes));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("VoteById/{recipeId}-{userId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get a vote by RecipeId and UserId",
            Description = "Retrieves a vote by RecipeId and UserId.",
            OperationId = "GetVoteById")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Vote not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<VoteDTO>> GetVoteByIdAsync(int recipeId, int userId)
        {
            try
            {
                var vote = await _voteRepository.GetAsync(recipeId, userId);
                if (vote == null) return NotFound();
                return Ok(_mapper.Map<VoteDTO>(vote));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost("Vote")]
        [Consumes("application/json")]
        [SwaggerOperation(
            Summary = "Create a new vote",
            Description = "Creates a new vote.",
            OperationId = "CreateVote")]
        [SwaggerResponse(201, "Vote created")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> CreateVoteAsync([FromBody] VoteDTO voteDto)
        {
            try
            {
                if (voteDto == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var vote = _mapper.Map<Vote>(voteDto);
                var createdVote = await _voteRepository.CreateAsync(vote);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
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

        [HttpDelete("Vote/{recipeId}-{userId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete a vote by RecipeId and UserId",
            Description = "Deletes a vote by RecipeId and UserId.",
            OperationId = "DeleteVote")]
        [SwaggerResponse(200, "Vote deleted")]
        [SwaggerResponse(404, "Vote not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> DeleteVoteAsync(int recipeId, int userId)
        {
            try
            {
                var deleted = await _voteRepository.DeleteAsync(recipeId, userId);
                if (!deleted) return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
