using Library.EntityLayer.Models;
using System.Data.Entity;

namespace Library.DataAccessLayer.Context
{
    public class LibraryDataAccessContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Magazine> Magazines { get; set; }

        public LibraryDataAccessContext() : base("name=LibraryDataAccessContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().HasMany(c => c.Books)
            .WithMany(s => s.Authors)
            .Map(t => t.MapLeftKey("AuthorId")
            .MapRightKey("BookId")
            .ToTable("BookAuthor"));
        }
        
    }
}