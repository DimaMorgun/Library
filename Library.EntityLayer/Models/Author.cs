using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.EntityLayer.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int? Birthday { get; set; }
        public int? Deathday { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
