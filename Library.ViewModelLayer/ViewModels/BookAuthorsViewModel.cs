using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class BookAuthorsViewModel
    {
        public BookViewModel Book { get; set; }
        //public ICollection<BookAuthorsTableViewModel> BookAuthors { get; set; }
        public ICollection<AuthorViewModel> Authors { get; set; }

        public BookAuthorsViewModel()
        {
            //BookAuthors = new List<BookAuthorsTableViewModel>();
            Authors = new List<AuthorViewModel>();
        }
    }
}
