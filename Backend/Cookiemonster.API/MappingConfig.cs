using AutoMapper;
using Cookiemonster.API.DTOs;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.API
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
            CreateMap<Todo, TodoDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Vote, VoteDTO>().ReverseMap();
        }

    }
}
