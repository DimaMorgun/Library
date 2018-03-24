using Dapper.Contrib.Extensions;

namespace Library.EntityLayer.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Name { get; set; }
        public int YearOfPublishing { get; set; }
    }
}
