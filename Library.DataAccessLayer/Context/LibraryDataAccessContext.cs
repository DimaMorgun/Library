using Library.EntityLayer.Models;
using System.Data.Entity;

namespace Library.DataAccessLayer.Context
{
    public class LibraryDataAccessContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<PublicationHouse> PublicationHouses { get; set; }

        public LibraryDataAccessContext() : base("name=LibraryDataAccessContext")
        {

        }
    }
}