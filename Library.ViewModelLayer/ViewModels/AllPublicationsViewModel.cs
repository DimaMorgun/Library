﻿using System.Collections.Generic;

namespace Library.ViewModelLayer.ViewModels
{
    public class AllPublicationsViewModel
    {
        public ICollection<BookViewModel> Books { get; set; }
        public ICollection<MagazineViewModel> Magazines { get; set; }

        public AllPublicationsViewModel()
        {
            Books = new List<BookViewModel>();
            Magazines = new List<MagazineViewModel>();
        }
    }
}
