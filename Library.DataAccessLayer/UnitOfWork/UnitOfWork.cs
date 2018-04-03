using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Repositories;
using Library.EntityLayer.Models;

using System;

namespace Library.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private LibraryDataAccessContext _libraryContext;
        private GenericRepository<Book> _bookRepository;
        private GenericRepository<Author> _authorRepository;
        private BookAuthorRepository _bookAuthorRepository;
        private GenericRepository<PublicationHouse> _publicationHouseRepository;
        private BookPublicationHouseRepository _bookPublicationHouseRepository;
        private GenericRepository<Magazine> _magazineRepository;
        private GenericRepository<Brochure> _brochureRepository;

        public UnitOfWork()
        {
            _libraryContext = new LibraryDataAccessContext();
        }

        public GenericRepository<Book> Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new GenericRepository<Book>();
                return _bookRepository;
            }
        }
        public GenericRepository<Author> Authors
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
        public GenericRepository<PublicationHouse> PublicationHouses
        {
            get
            {
                if (_publicationHouseRepository == null)
                    _publicationHouseRepository = new GenericRepository<PublicationHouse>();
                return _publicationHouseRepository;
            }
        }
        public BookPublicationHouseRepository BookPublicationHouses
        {
            get
            {
                if (_bookPublicationHouseRepository == null)
                    _bookPublicationHouseRepository = new BookPublicationHouseRepository();
                return _bookPublicationHouseRepository;
            }
        }
        public GenericRepository<Magazine> Magazines
        {
            get
            {
                if (_magazineRepository == null)
                    _magazineRepository = new GenericRepository<Magazine>();
                return _magazineRepository;
            }
        }
        public GenericRepository<Brochure> Brochures
        {
            get
            {
                if (_brochureRepository == null)
                    _brochureRepository = new GenericRepository<Brochure>();
                return _brochureRepository;
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
