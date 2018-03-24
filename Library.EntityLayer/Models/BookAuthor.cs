using Dapper.Contrib.Extensions;

namespace Library.EntityLayer.Models
{
    public class BookAuthor
    {
        [Key]
        public int BookAuthorId { get; set; }
        public int BookId { get; set; }
        protected virtual Book Book { get; set; }
        public int AuthorId { get; set; }
        protected virtual Author Author { get; set; }
    }
}
