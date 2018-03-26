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
    public class BookService
    {
        private UnitOfWork _unitOfWork;

        public BookService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Insert(BookViewModel book, int[] selectedItems)
        {
            Book bookModel = Mapper.Map<BookViewModel, Book>(book);
            var authorsModel = new List<BookAuthor>();

            var bookId = _unitOfWork.Books.Insert(bookModel);

            if (selectedItems != null)
            {
                foreach (var author in _unitOfWork.Authors.GetAll().Where(x => selectedItems.Contains(x.AuthorId)))
                {
                    //authorsModel.Add(new BookAuthor() { BookId = bookId, AuthorId = author.AuthorId });
                }
            }

            _unitOfWork.BookAuthors.Insert(authorsModel);
        }

        public void Delete(int id)
        {
            var bookModel = _unitOfWork.Books.Get(id);
            _unitOfWork.Books.Delete(bookModel);
        }

        public BookViewModel Get(int id)
        {
            List<BookAuthor> bookAuthors = _unitOfWork.BookAuthors.GetAllByBookId(id);

            var book = new Book() { BookId = bookAuthors.ElementAt(0).Book.BookId, Name = bookAuthors.ElementAt(0).Book.Name, YearOfPublishing = bookAuthors.ElementAt(0).Book.YearOfPublishing };
            var authors = new List<Author>();
            foreach (var bookAuthor in bookAuthors)
            {
                authors.Add(bookAuthor.Author);
            }
            var resultBook = Mapper.Map<Book, BookViewModel>(book);
            resultBook.Authors = Mapper.Map<List<Author>, List<AuthorViewModel>>(authors);

            return resultBook;
        }

        public List<BookViewModel> GetAll()
        {
            List<Book> allBooksModel = _unitOfWork.Books.GetAll();

            List<BookViewModel> allBooksViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);

            return allBooksViewModel;
        }

        public BookAuthorsViewModel GetBookAuthors()
        {
            List<Author> allAuthors = _unitOfWork.Authors.GetAll();

            List<AuthorViewModel> authorViewModel = Mapper.Map<List<Author>, List<AuthorViewModel>>(allAuthors);

            var bookAuthorsViewModel = new BookAuthorsViewModel
            {
                Authors = authorViewModel
            };

            return bookAuthorsViewModel;
        }

        public BookAuthorsViewModel GetBookAuthors(int id)
        {
            Book bookModel = _unitOfWork.Books.Get(id);
            List<Author> allAuthors = _unitOfWork.Authors.GetAll();
            List<BookAuthor> allBookAuthors = _unitOfWork.BookAuthors.GetAll();

            var bookViewModel = Mapper.Map<Book, BookViewModel>(bookModel);
            var authorViewModel = Mapper.Map<List<Author>, List<AuthorViewModel>>(allAuthors);
            var bookAuthorsTableViewModel = Mapper.Map<List<BookAuthor>, List<BookAuthorsTableViewModel>>(allBookAuthors);

            var bookAuthorsViewModel = new BookAuthorsViewModel
            {
                Book = bookViewModel,
                Authors = authorViewModel,
                //BookAuthors = bookAuthorsTableViewModel
            };

            return bookAuthorsViewModel;
        }

        //TODO: REWRITE
        public void Update(BookViewModel book, int[] selectedItems)
        {
            //Book bookModel = _unitOfWork.Books.Get(book.BookId);
            //bookModel.Name = book.Name;
            //bookModel.YearOfPublishing = book.YearOfPublishing;
            //_unitOfWork.Books.Update(bookModel);

            //var bookAuthor = new BookAuthor();
            //var newBookAuthor = new DataAccessLayer.Context.LibraryDataAccessContext().BookAuthors.Where(x => x.BookId == book.BookId).ToList();
            //foreach (var item in newBookAuthor)
            //{
            //    _unitOfWork.BookAuthors.Delete(item);
            //}
            //newBookAuthor.Clear();
            //if (selectedItems != null)
            //{
            //    foreach (var author in _unitOfWork.Authors.GetAll().Where(x => selectedItems.Contains(x.AuthorId)))
            //    {
            //        bookAuthor = new DataAccessLayer.Context.LibraryDataAccessContext().BookAuthors.FirstOrDefault(x => x.BookId == book.BookId && x.AuthorId == author.AuthorId);
            //        if (bookAuthor != null)
            //        {
            //            newBookAuthor.Add(bookAuthor);
            //        }
            //        else
            //        {
            //            newBookAuthor.Add(new BookAuthor() { BookId = book.BookId, AuthorId = author.AuthorId });
            //        }
            //    }
            //}
            //foreach (var item in newBookAuthor)
            //{
            //    _unitOfWork.BookAuthors.Insert(item);
            //}

        }

        public void SaveToJSON(int id)
        {
            Book bookModel = _unitOfWork.Books.Get(id);

            string json = JsonConvert.SerializeObject(
                bookModel,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var path = HttpContext.Current.Server.MapPath("~/App_Data/Book.json");

            System.IO.File.WriteAllText(path, json);
        }
    }
}
