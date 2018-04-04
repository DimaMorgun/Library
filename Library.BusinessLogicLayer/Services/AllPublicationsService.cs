using Library.DataAccessLayer.Connection;
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
            _unitOfWork = new UnitOfWork(CurrentConnection.ConnectionString);
        }

        public AllPublicationsViewModel GetAll()
        {
            List<Book> allBooksModel = _unitOfWork.Books.GetAll();
            List<Magazine> allMagazinesModel = _unitOfWork.Magazines.GetAll();
            List<Brochure> allBrochuresModel = _unitOfWork.Brochures.GetAll();

            var allPublicationsViewModel = new AllPublicationsViewModel();
            allPublicationsViewModel.Books = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);
            allPublicationsViewModel.Magazines = Mapper.Map<List<Magazine>, List<MagazineViewModel>>(allMagazinesModel);
            allPublicationsViewModel.Brochures = Mapper.Map<List<Brochure>, List<BrochureViewModel>>(allBrochuresModel);

            return allPublicationsViewModel;
        }
    }
}
