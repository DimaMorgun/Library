using Library.EntityLayer.Models;

namespace Library.DataAccessLayer.Repositories
{
    public class PublicationHouseRepository : GenericRepository<PublicationHouse>
    {
        private string _connection;

        public PublicationHouseRepository(string connection) : base(connection)
        {
            _connection = connection;
        }
    }
}
