using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Identity;
using Library.DataAccessLayer.Interfaces;
using Library.DataAccessLayer.Repositories;
using Library.EntityLayer.Identity;
using Library.EntityLayer.Models;

using Microsoft.AspNet.Identity.EntityFramework;

using System;
using System.Threading.Tasks;

namespace Library.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryDataAccessContext _context;
        private string _connection;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IClientManager _clientManager;

        private IGenericRepository<Book> _bookRepository;
        private IGenericRepository<Author> _authorRepository;
        private BookAuthorRepository _bookAuthorRepository;
        private IGenericRepository<PublicationHouse> _publicationHouseRepository;
        private BookPublicationHouseRepository _bookPublicationHouseRepository;
        private IGenericRepository<Magazine> _magazineRepository;
        private IGenericRepository<Brochure> _brochureRepository;

        public UnitOfWork(string connection)
        {
            _connection = connection;
            _context = new LibraryDataAccessContext(_connection);

            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            _clientManager = new ClientManager(_context);
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public IClientManager ClientManager
        {
            get { return _clientManager; }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IGenericRepository<Book> Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new GenericRepository<Book>(_connection);
                return _bookRepository;
            }
        }
        public IGenericRepository<Author> Authors
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new GenericRepository<Author>(_connection);
                return _authorRepository;
            }
        }
        public BookAuthorRepository BookAuthors
        {
            get
            {
                if (_bookAuthorRepository == null)
                    _bookAuthorRepository = new BookAuthorRepository(_connection);
                return _bookAuthorRepository;
            }
        }
        public IGenericRepository<PublicationHouse> PublicationHouses
        {
            get
            {
                if (_publicationHouseRepository == null)
                    _publicationHouseRepository = new GenericRepository<PublicationHouse>(_connection);
                return _publicationHouseRepository;
            }
        }
        public BookPublicationHouseRepository BookPublicationHouses
        {
            get
            {
                if (_bookPublicationHouseRepository == null)
                    _bookPublicationHouseRepository = new BookPublicationHouseRepository(_connection);
                return _bookPublicationHouseRepository;
            }
        }
        public IGenericRepository<Magazine> Magazines
        {
            get
            {
                if (_magazineRepository == null)
                    _magazineRepository = new GenericRepository<Magazine>(_connection);
                return _magazineRepository;
            }
        }
        public IGenericRepository<Brochure> Brochures
        {
            get
            {
                if (_brochureRepository == null)
                    _brochureRepository = new GenericRepository<Brochure>(_connection);
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
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _clientManager.Dispose();
                    //TODO: might be a bug
                    _context.Dispose();
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
