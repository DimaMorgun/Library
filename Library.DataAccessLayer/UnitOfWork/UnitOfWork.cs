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
        private GenericRepository<Book> _bookRepository;
        private GenericRepository<Author> _authorRepository;
        private BookAuthorRepository _bookAuthorRepository;
        private GenericRepository<Magazine> _magazineRepository;
        private GenericRepository<Brochure> _brochureRepository;
        private GenericRepository<PublicationHouse> _publicationHouseRepository;

        public UnitOfWork()
        {
            _libraryContext = new LibraryDataAccessContext();
        }

        public IGenericRepository<Book> Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new GenericRepository<Book>();
                return _bookRepository;
            }
        }
        public IGenericRepository<Author> Authors
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new GenericRepository<Author>();
                return _authorRepository;
            }
        }
        public BookAuthorRepository BookAuthors
        {
            get
            {
                if (_bookAuthorRepository == null)
                    _bookAuthorRepository = new BookAuthorRepository();
                return _bookAuthorRepository;
            }
        }
        public IGenericRepository<Magazine> Magazines
        {
            get
            {
                if (_magazineRepository == null)
                    _magazineRepository = new GenericRepository<Magazine>();
                return _magazineRepository;
            }
        }
        public IGenericRepository<Brochure> Brochures
        {
            get
            {
                if (_brochureRepository == null)
                    _brochureRepository = new GenericRepository<Brochure>();
                return _brochureRepository;
            }
        }
        public IGenericRepository<PublicationHouse> PublicationHousees
        {
            get
            {
                if (_publicationHouseRepository == null)
                    _publicationHouseRepository = new GenericRepository<PublicationHouse>();
                return _publicationHouseRepository;
            }
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
