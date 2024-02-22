using AutoMapper;

namespace BookStoreApp.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<Author, AuthorUpdateDTO>().ReverseMap();
        }
    }
}
