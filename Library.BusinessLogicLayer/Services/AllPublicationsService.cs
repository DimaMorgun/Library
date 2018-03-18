using Library.DataAccessLayer.UnitOfWork;
using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;

using AutoMapper;

using System.Collections.Generic;

namespace Library.BusinessLogicLayer.Services
{
    public class AllPublicationsService
    {
        private UnitOfWork _unitOfWork;

        public AllPublicationsService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AllPublicationsViewModel GetList()
        {
            List<Book> allBooksModel = _unitOfWork.Books.GetList();
            List<Magazine> allMagazinesModel = _unitOfWork.Magazines.GetList();

            var allPublicationsViewModel = new AllPublicationsViewModel();
            allPublicationsViewModel.Books = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);
            allPublicationsViewModel.Magazines = Mapper.Map<List<Magazine>, List<MagazineViewModel>>(allMagazinesModel);

            return allPublicationsViewModel;
        }
    }
}
