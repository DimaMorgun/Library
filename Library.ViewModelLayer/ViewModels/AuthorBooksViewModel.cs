using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class AuthorBooksViewModel
    {
        public AuthorViewModel Author { get; set; }
        public ICollection<BookAuthorsRelationViewModel> BookAuthors { get; set; }
        public ICollection<BookViewModel> Books { get; set; }

        public AuthorBooksViewModel()
        {
            BookAuthors = new List<BookAuthorsRelationViewModel>();
            Books = new List<BookViewModel>();
        }
    }
}
