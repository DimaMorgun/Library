using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Interfaces;
using Library.EntityLayer.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private LibraryDataAccessContext _context;

        public BookRepository(LibraryDataAccessContext context)
        {
            _context = context;
        }

        public List<Book> GetList()
        {
            return _context.Books.ToList();
        }

        public Book GetByid(int id)
        {
            return _context.Books.Find(id);
        }

        public void Create(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }
    }
}
