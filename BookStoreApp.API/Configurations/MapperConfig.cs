using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTO.Author;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() { 
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
        }
    }
}
