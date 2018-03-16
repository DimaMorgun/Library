using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int YearOfPublishing { get; set; }

        public virtual ICollection<AuthorViewModel> Authors { get; set; }

        public BookViewModel()
        {
            Authors = new List<AuthorViewModel>();
        }
    }
}
