using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Interfaces;
using Library.EntityLayer.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Library.DataAccessLayer.Repositories
{
    public class MagazineRepository : IRepository<Magazine>
    {
        private LibraryDataAccessContext _context;

        public MagazineRepository(LibraryDataAccessContext context)
        {
            _context = context;
        }

        public List<Magazine> GetList()
        {
            return _context.Magazines.ToList();
        }

        public Magazine GetByid(int id)
        {
            return _context.Magazines.Find(id);
        }

        public void Create(Magazine magazine)
        {
            _context.Magazines.Add(magazine);
        }

        public void Update(Magazine magazine)
        {
            _context.Entry(magazine).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Magazine magazine = _context.Magazines.Find(id);
            if (magazine != null)
            {
                _context.Magazines.Remove(magazine);
            }
        }
    }
}
