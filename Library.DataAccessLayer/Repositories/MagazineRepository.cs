using Library.EntityLayer.Models;

namespace Library.DataAccessLayer.Repositories
{
    public class MagazineRepository : GenericRepository<Magazine>
    {
        private string _connection;

        public MagazineRepository(string connection) : base(connection)
        {
            _connection = connection;
        }
    }
}
