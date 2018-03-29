using Library.DataAccessLayer.UnitOfWork;
using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;

using AutoMapper;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BusinessLogicLayer.Services
{
    public class PublicationHouseService
    {
        private UnitOfWork _unitOfWork;

        public PublicationHouseService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Insert(PublicationHouseViewModel publicationHouse, int[] selectedBooks)
        {
            var publicationHouseModel = Mapper.Map<PublicationHouseViewModel, PublicationHouse>(publicationHouse);
            var bookPublicationHouseModel = new List<BookPublicationHouse>();

            var publicationHouseId = _unitOfWork.PublicationHouses.Insert(publicationHouseModel);

            if (selectedBooks != null)
            {
                foreach (var bookId in selectedBooks)
                {
                    bookPublicationHouseModel.Add(new BookPublicationHouse() { BookId = bookId, PublicationHouseId = publicationHouseId });
                }
            }

            _unitOfWork.BookPublicationHouses.Insert(bookPublicationHouseModel);
        }

        public PublicationHouseViewModel Get(int id)
        {
            List<BookPublicationHouse> bookPublicationHouse = _unitOfWork.BookPublicationHouses.GetAllByPublicationHouseId(id);

            var publicationHouse = new PublicationHouse() { PublicationHouseId = bookPublicationHouse.ElementAt(0).PublicationHouse.PublicationHouseId, Name = bookPublicationHouse.ElementAt(0).PublicationHouse.Name, Adress = bookPublicationHouse.ElementAt(0).PublicationHouse.Adress };
            var books = new List<Book>();
            foreach (var book in bookPublicationHouse)
            {
                if (book.Book != null)
                {
                    books.Add(book.Book);
                }
            }
            var publicationHouseViewModel = Mapper.Map<PublicationHouse, PublicationHouseViewModel>(publicationHouse);
            publicationHouseViewModel.Books = Mapper.Map<List<Book>, List<BookViewModel>>(books);

            return publicationHouseViewModel;
        }

        public List<PublicationHouseViewModel> GetAll()
        {
            List<PublicationHouse> allPublicationHousesModel = _unitOfWork.PublicationHouses.GetAll();

            var allPublicationHousesViewModel = Mapper.Map<List<PublicationHouse>, List<PublicationHouseViewModel>>(allPublicationHousesModel);

            return allPublicationHousesViewModel;
        }

        public PublicationHouseBooksViewModel GetPublicationHouseBooks()
        {
            List<Book> allBooks = _unitOfWork.Books.GetAll();

            var allBooksViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooks);

            var publicationHouseBooksViewModel = new PublicationHouseBooksViewModel
            {
                Books = allBooksViewModel
            };

            return publicationHouseBooksViewModel;
        }

        public PublicationHouseBooksViewModel GetPublicationHouseBooks(int id)
        {
            PublicationHouse publicationHouseModel = _unitOfWork.PublicationHouses.Get(id);
            List<BookPublicationHouse> allBookPublicationHouses = _unitOfWork.BookPublicationHouses.GetAll();
            List<Book> allBooksModel = _unitOfWork.Books.GetAll();

            var bookPublicationHouseRelationViewModel = Mapper.Map<List<BookPublicationHouse>, List<BookPublicationHousesRelationViewModel>>(allBookPublicationHouses);
            var bookViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);
            var publicationHouseViewModel = Mapper.Map<PublicationHouse, PublicationHouseViewModel>(publicationHouseModel);

            var publicationHouseBooksViewModel = new PublicationHouseBooksViewModel
            {
                Books = bookViewModel,
                PublicationHouse = publicationHouseViewModel,
                BookPublicationHouses = bookPublicationHouseRelationViewModel
            };

            return publicationHouseBooksViewModel;
        }

        public void Update(PublicationHouseViewModel publicationHouse, int[] selectedBooks)
        {
            PublicationHouse publicationHouseModel = _unitOfWork.PublicationHouses.Get(publicationHouse.PublicationHouseId);
            publicationHouseModel.Name = publicationHouse.Name;
            publicationHouseModel.Adress = publicationHouse.Adress;
            _unitOfWork.PublicationHouses.Update(publicationHouseModel);

            List<BookPublicationHouse> oldBookPublicationHouse = _unitOfWork.BookPublicationHouses.GetAllByPublicationHouseId(publicationHouse.PublicationHouseId);
            var oldBookpublicationHousesWithRelation = oldBookPublicationHouse.Where(x => x.BookId != 0).ToList();
            selectedBooks = selectedBooks ?? (new int[0]);
            var BooksHas = oldBookpublicationHousesWithRelation.Where(x => selectedBooks.Contains(x.BookId)).ToList();
            var BooksNothas = oldBookpublicationHousesWithRelation.Where(x => !selectedBooks.Contains(x.BookId)).ToList();
            _unitOfWork.BookPublicationHouses.Delete(BooksNothas);
            if (selectedBooks != null)
            {
                List<BookPublicationHouse> currentBookPublicationHouses = new List<BookPublicationHouse>();

                foreach (var newBookId in selectedBooks)
                {
                    if (BooksHas.FirstOrDefault(x => x.BookId == newBookId) == null)
                    {
                        currentBookPublicationHouses.Add(new BookPublicationHouse() { BookId = newBookId, PublicationHouseId = publicationHouseModel.PublicationHouseId });
                    }
                }
                _unitOfWork.BookPublicationHouses.Insert(currentBookPublicationHouses);
            }
        }

        public void Delete(int id)
        {
            var publicationHouseModel = _unitOfWork.PublicationHouses.Get(id);
            _unitOfWork.PublicationHouses.Delete(publicationHouseModel);
        }

        public void SaveToJSON(int id)
        {
            PublicationHouse publicationHouseModel = _unitOfWork.PublicationHouses.Get(id);

            string json = JsonConvert.SerializeObject(
                publicationHouseModel,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var path = HttpContext.Current.Server.MapPath("~/App_Data/PublicationHouse.json");

            System.IO.File.WriteAllText(path, json);
        }
    }
}
