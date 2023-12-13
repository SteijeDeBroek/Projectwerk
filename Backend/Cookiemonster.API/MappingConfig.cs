using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Infrastructure.EFRepository.Models;


namespace Cookiemonster.API
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            // GETMapping
            CreateMap<Category, CategoryDTOGet>();
            CreateMap<Image, ImageDTOGet>();
            CreateMap<Infrastructure.EFRepository.Models.Recipe, RecipeDTOGet>();
            CreateMap<Todo, TodoDTOGet>();
            CreateMap<User, UserDTOGet>();
            CreateMap<Vote, VoteDTOGet>();

            // POSTMapping
            CreateMap<CategoryDTOPost, Category>();
            CreateMap<ImageDTOPost, Image>();
            CreateMap<DTOPosts.RecipeDTOPost, Infrastructure.EFRepository.Models.Recipe>();
            CreateMap<TodoDTOPost, Todo>();
            CreateMap<UserDTOPost, User>();
            CreateMap<VoteDTOPost, Vote>();
        }

    }
}
