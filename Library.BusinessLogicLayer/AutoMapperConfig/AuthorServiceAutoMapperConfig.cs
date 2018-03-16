using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;

using AutoMapper;

using System.Collections.Generic;

namespace Library.BusinessLogicLayer.AutoMapperConfig
{
    public class AuthorServiceAutoMapperConfig
    {
        public static void Initialize()
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<Book, BookViewModel>();
            //    cfg.CreateMap<Author, AuthorViewModel>();
            //    cfg.CreateMap<List<Book>, List<BookViewModel>>();
            //    cfg.CreateMap<List<Author>, List<AuthorViewModel>>();
            //    cfg.CreateMap<BookViewModel, Book>();
            //});
        }
    }
}
