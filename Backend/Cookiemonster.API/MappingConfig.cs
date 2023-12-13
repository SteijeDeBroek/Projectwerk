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
            CreateMap<Recipe, RecipeDTOGet>();
            CreateMap<Todo, TodoDTO>().ReverseMap();
            CreateMap<User, UserDTOGet>();
            CreateMap<Vote, VoteDTO>().ReverseMap();

            // POSTMapping
            CreateMap<CategoryDTOPost, Category>();
            CreateMap<ImageDTOPost, Image>();
            CreateMap<RecipeDTOPost, Recipe>();
            CreateMap<UserDTOPost, User>();
        }

    }
}
