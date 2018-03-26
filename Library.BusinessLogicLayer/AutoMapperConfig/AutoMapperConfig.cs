using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;
using AutoMapper;

namespace Library.BusinessLogicLayer.AutoMapperConfig
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookViewModel>();
                cfg.CreateMap<Author, AuthorViewModel>();
                cfg.CreateMap<BookViewModel, Book>();
                cfg.CreateMap<AuthorViewModel, Author>();
                cfg.CreateMap<Magazine, MagazineViewModel>();
                cfg.CreateMap<MagazineViewModel, Magazine>();
                cfg.CreateMap<Brochure, BrochureViewModel>();
                cfg.CreateMap<BrochureViewModel, Brochure>();
                cfg.CreateMap<BookAuthor, BookAuthorsTableViewModel>();
                cfg.CreateMap<BookAuthor, BookViewModel>();
            });
        }
    }
}
