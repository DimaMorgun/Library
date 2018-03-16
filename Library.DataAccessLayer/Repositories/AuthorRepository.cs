using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Interfaces;
using Library.EntityLayer.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        private LibraryDataAccessContext _context;

        public AuthorRepository(LibraryDataAccessContext context)
        {
            _context = context;
        }

        public List<Author> GetList()
        {
            return _context.Authors.ToList();
        }

        public Author GetByid(int id)
        {
            return _context.Authors.Find(id);
        }

        public void Create(Author author)
        {
            _context.Authors.Add(author);
        }

        public void Update(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Author author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
        }
    }
}
