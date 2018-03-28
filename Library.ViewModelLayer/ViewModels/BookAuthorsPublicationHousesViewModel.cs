using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class BookAuthorsPublicationHousesViewModel
    {
        public BookViewModel Book { get; set; }
        public ICollection<BookAuthorsRelationViewModel> BookAuthors { get; set; }
        public ICollection<AuthorViewModel> Authors { get; set; }
        public ICollection<BookPublicationHousesRelationViewModel> BookPublicationHouses { get; set; }
        public ICollection<PublicationHouseViewModel> PublicationHouses { get; set; }

        public BookAuthorsPublicationHousesViewModel()
        {
            BookAuthors = new List<BookAuthorsRelationViewModel>();
            Authors = new List<AuthorViewModel>();
            BookPublicationHouses = new List<BookPublicationHousesRelationViewModel>();
            PublicationHouses = new List<PublicationHouseViewModel>();
        }
    }
}
