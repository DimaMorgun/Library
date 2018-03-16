using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int? Birthday { get; set; }
        public int? Deathday { get; set; }

        public virtual ICollection<BookViewModel> Books { get; set; }

        public AuthorViewModel()
        {
            Books = new List<BookViewModel>();
        }
    }
}
