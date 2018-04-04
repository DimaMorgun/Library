using Library.DataAccessLayer.Context;
using Library.DataAccessLayer.Interfaces;
using Library.EntityLayer.Identity;

namespace Library.DataAccessLayer.Repositories
{
    public class ClientManager : IClientManager
    {
        private LibraryDataAccessContext _context { get; set; }

        public ClientManager(LibraryDataAccessContext context)
        {
            _context = context;
        }

        public void Create(ClientProfile item)
        {
            _context.ClientProfiles.Add(item);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
