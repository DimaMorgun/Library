using System.Configuration;
using System.Data.SqlClient;

namespace Library.DataAccessLayer.Connection
{
    public static class CurrentConnection
    {
        public static string ConnectionString { get; set; }

        static CurrentConnection()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["LibraryDataAccessContext"].ConnectionString;
        }
    }
}
