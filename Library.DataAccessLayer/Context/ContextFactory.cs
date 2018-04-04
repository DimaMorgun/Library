using System.Data.Entity.Infrastructure;

namespace Library.DataAccessLayer.Context
{
    public class ContextFactory : IDbContextFactory<LibraryDataAccessContext>
    {
        public LibraryDataAccessContext Create()
        {
            return new LibraryDataAccessContext("LibraryDataAccessContext");
        }
    }
}
