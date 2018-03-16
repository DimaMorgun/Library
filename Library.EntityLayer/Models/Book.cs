using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.EntityLayer.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Name { get; set; }
        public int YearOfPublishing { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public Book()
        {
            Authors = new List<Author>();
        }
    }
}
