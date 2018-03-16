using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;
using System.Linq;
using AutoMapper;

namespace Library.BusinessLogicLayer.AutoMapperConfig
{
    public class BookServiceAutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BookViewModel, Book>();
                cfg.CreateMap<Author, AuthorViewModel>();
                cfg.CreateMap<Book, BookViewModel>();
            });
        }
    }
}
