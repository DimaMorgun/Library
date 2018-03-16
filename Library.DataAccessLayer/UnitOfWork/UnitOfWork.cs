using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Interfaces;
using Library.DataAccessLayer.Repositories;
using Library.EntityLayer.Models;

using System;

namespace Library.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryDataAccessContext _libraryContext;
        private BookRepository _bookRepository;
        private AuthorRepository _authorRepository;
        private MagazineRepository _magazineRepository;

        public UnitOfWork()
        {
            _libraryContext = new LibraryDataAccessContext();
        }

        public IRepository<Book> Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_libraryContext);
                return _bookRepository;
            }
        }
        public IRepository<Author> Authors
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(_libraryContext);
                return _authorRepository;
            }
        }
        public IRepository<Magazine> Magazines
        {
            get
            {
                if (_magazineRepository == null)
                    _magazineRepository = new MagazineRepository(_libraryContext);
                return _magazineRepository;
            }
        }

        public void Save()
        {
            _libraryContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _libraryContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
