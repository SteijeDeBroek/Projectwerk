using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPatches;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<VoteDTO>> Get()
        {
            var votes = _voteRepository.GetAll();
            return Ok(_mapper.Map<List<VoteDTO>>(votes));
        }

        // GET: api/votes/5-4
        [HttpGet("VoteById/{recipeId}-{userId}")]
        public ActionResult<VoteDTO> Get(int recipeId, int userId)
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
        public ActionResult CreateVote(VoteDTO vote)
        {
            if (vote == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdVote = _voteRepository.Create(_mapper.Map<Vote>(vote));
            return CreatedAtAction(nameof(Get), vote);
        }

        // PATCH: api/votes/5-4
        [HttpPatch("Vote/{recipeId}-{userId}")]
        public ActionResult PatchVote(int recipeId, int userId, [FromBody] VoteDTOPatch vote)
        {
            if (vote == null || !ModelState.IsValid)
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

            _voteRepository.Update(mappedVote);
            return Ok();
        }

        // DELETE: api/votes/5-4
        [HttpDelete("Vote/{recipeId}-{userId}")]
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