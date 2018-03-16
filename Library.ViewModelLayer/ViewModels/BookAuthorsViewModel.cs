using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class BookAuthorsViewModel
    {
        public BookViewModel Book { get; set; }
        public ICollection<AuthorViewModel> Authors { get; set; }

        public BookAuthorsViewModel()
        {
            Authors = new List<AuthorViewModel>();
        }
    }
}
