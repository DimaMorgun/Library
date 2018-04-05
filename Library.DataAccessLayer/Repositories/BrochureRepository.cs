using Library.EntityLayer.Models;

namespace Library.DataAccessLayer.Repositories
{
    public class BrochureRepository : GenericRepository<Brochure>
    {
        private string _connection;

        public BrochureRepository(string connection) : base(connection)
        {
            _connection = connection;
        }
    }
}
