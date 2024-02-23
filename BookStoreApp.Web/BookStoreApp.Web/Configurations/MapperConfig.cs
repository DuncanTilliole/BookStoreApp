using AutoMapper;

namespace BookStoreApp.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();
            CreateMap<BookUpdateDTO, BookReadOnlyDTO>()
                .ReverseMap()
                .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.Image));
        }
    }
}
