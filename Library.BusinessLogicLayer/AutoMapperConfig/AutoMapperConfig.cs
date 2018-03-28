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
                cfg.CreateMap<BookViewModel, Book>();
                cfg.CreateMap<BookAuthor, BookAuthorsRelationViewModel>();
                cfg.CreateMap<BookAuthor, BookViewModel>();
                cfg.CreateMap<Author, AuthorViewModel>();
                cfg.CreateMap<AuthorViewModel, Author>();
                cfg.CreateMap<BookPublicationHouse, BookPublicationHousesRelationViewModel>();
                cfg.CreateMap<PublicationHouse, PublicationHouseViewModel>();

                cfg.CreateMap<Magazine, MagazineViewModel>();
                cfg.CreateMap<MagazineViewModel, Magazine>();

                cfg.CreateMap<Brochure, BrochureViewModel>();
                cfg.CreateMap<BrochureViewModel, Brochure>();
            });
        }
    }
}
