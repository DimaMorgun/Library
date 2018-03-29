using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class PublicationHouseViewModel
    {
        public int PublicationHouseId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public virtual ICollection<BookViewModel> Books { get; set; }
        public ICollection<int> SelectedBooks { get; set; }

        public PublicationHouseViewModel()
        {
            Books = new List<BookViewModel>();
            SelectedBooks = new List<int>();
        }
    }
}
