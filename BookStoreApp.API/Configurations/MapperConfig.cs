﻿using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTO.Auth;
using BookStoreApp.API.DTO.Author;
using BookStoreApp.API.DTO.Book;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() { 
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();

            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
