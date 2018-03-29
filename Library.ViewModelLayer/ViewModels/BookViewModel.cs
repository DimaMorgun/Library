using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int YearOfPublishing { get; set; }

        public virtual ICollection<AuthorViewModel> Authors { get; set; }
        public ICollection<int> SelectedAuthors { get; set; }
        public virtual ICollection<PublicationHouseViewModel> PublicationHouses { get; set; }
        public ICollection<int> SelectedPublicationHouses { get; set; }

        public BookViewModel()
        {
            Authors = new List<AuthorViewModel>();
            PublicationHouses = new List<PublicationHouseViewModel>();
            SelectedAuthors = new List<int>();
            SelectedPublicationHouses = new List<int>();
        }
    }
}
