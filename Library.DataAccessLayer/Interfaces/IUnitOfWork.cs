using Library.EntityLayer.Models;

using System;

namespace Library.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Author> Authors { get; }
        IRepository<Magazine> Magazines { get; }
        void Save();
    }
}
