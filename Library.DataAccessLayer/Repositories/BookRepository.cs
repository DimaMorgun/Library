using Library.EntityLayer.Models;

namespace Library.DataAccessLayer.Repositories
{
    public class BookRepository : GenericRepository<Book>
    {
        private string _connection;

        public BookRepository(string connection) : base(connection)
        {
            _connection = connection;
        }
    }
}
