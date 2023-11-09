using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Services
{
    public class VoteService : IDeletable
    {
        private readonly Repository<Vote> _voteRepository;

        public VoteService(Repository<Vote> voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public Vote CreateVote(Vote vote)
        {
            return _voteRepository.Create(vote);
        }

        public Vote GetVote(int recipeId, int userId)
        {
            return _voteRepository.Get(recipeId, userId); // Aangepast om twee argumenten te accepteren
        }

        public List<Vote> GetAllVotes()
        {
            return _voteRepository.GetAll();
        }

        public bool DeleteVote(int recipeId, int userId)
        {
            var entity = _voteRepository.Get(recipeId, userId);
            if (entity == null)
                return false;

            entity.isDeleted = true;
            return true;
        }
    }
}
