using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class PublicationHouseBooksViewModel
    {
        public PublicationHouseViewModel PublicationHouse { get; set; }
        public ICollection<BookPublicationHousesRelationViewModel> BookPublicationHouses { get; set; }
        public ICollection<BookViewModel> Books { get; set; }

        public PublicationHouseBooksViewModel()
        {
            BookPublicationHouses = new List<BookPublicationHousesRelationViewModel>();
            Books = new List<BookViewModel>();
        }
    }
}
