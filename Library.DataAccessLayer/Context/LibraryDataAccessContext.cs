using Library.EntityLayer.Identity;
using Library.EntityLayer.Models;

using Microsoft.AspNet.Identity.EntityFramework;

using System.Data.Entity;

namespace Library.DataAccessLayer.Context
{
    public class LibraryDataAccessContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<PublicationHouse> PublicationHouses { get; set; }
        public DbSet<BookPublicationHouse> BookPublicationHouses { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public LibraryDataAccessContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}