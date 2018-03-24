﻿using Library.DataAccessLayer.Repositories;
using Library.EntityLayer.Models;

using System;

namespace Library.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> Books { get; }
        IGenericRepository<Author> Authors { get; }
        BookAuthorRepository BookAuthors { get; }
        IGenericRepository<Magazine> Magazines { get; }
        IGenericRepository<Brochure> Brochures { get; }
        IGenericRepository<PublicationHouse> PublicationHousees { get; }
    }
}
