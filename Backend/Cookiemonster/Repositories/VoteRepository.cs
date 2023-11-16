using Cookiemonster.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Repositories
{
    public class VoteRepository
    {
        private readonly Repository<Vote> _voteRepository;

        public VoteRepository(Repository<Vote> voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public Vote CreateVote(Vote vote)
        {
            return _voteRepository.Create(vote);
        }

        public Vote GetVote(int recipeId, int userId)
        {
            return _voteRepository.Get(recipeId, userId);
        }

        public List<Vote> GetAllVotes()
        {
            return _voteRepository.GetAll();
        }

        public bool DeleteVote(int recipeId, int userId)
        {
            return _voteRepository.Delete(recipeId, userId);
        }
    }
}
