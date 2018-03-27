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
            var bookAuthorsModel = new List<BookAuthor>();

            var bookId = _unitOfWork.Books.Insert(bookModel);

            if (selectedItems != null)
            {
                List<Author> selectedAuthors = _unitOfWork.Authors.GetAll().Where(x => selectedItems.Contains(x.AuthorId)).ToList();
                foreach (var author in selectedAuthors)
                {
                    bookAuthorsModel.Add(new BookAuthor() { BookId = bookId, AuthorId = author.AuthorId });
                }
            }

            foreach (var item in bookAuthorsModel)
            {
                _unitOfWork.BookAuthors.Insert(item);
            }
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
            List<Author> authors = new List<Author>();
            foreach (var item in bookAuthors)
            {
                if (item.Author != null)
                {
                    authors.Add(item.Author);
                }
            }

            BookViewModel resultBook = Mapper.Map<Book, BookViewModel>(book);
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
                BookAuthors = bookAuthorsTableViewModel
            };

            return bookAuthorsViewModel;
        }

        public void Update(BookViewModel book, int[] selectedItems)
        {
            Book bookModel = _unitOfWork.Books.Get(book.BookId);
            bookModel.Name = book.Name;
            bookModel.YearOfPublishing = book.YearOfPublishing;
            _unitOfWork.Books.Update(bookModel);

            List<BookAuthor> oldBookAuthors = _unitOfWork.BookAuthors.GetAllByBookId(book.BookId);
            var oldBookAuthorsWithRelation = oldBookAuthors.Where(x => x.AuthorId != 0).ToList();
            var has = oldBookAuthorsWithRelation.Where(x => selectedItems.Contains(x.AuthorId)).ToList();
            var nothas = oldBookAuthorsWithRelation.Where(x => !selectedItems.Contains(x.AuthorId)).ToList();

            _unitOfWork.BookAuthors.Delete(nothas);

            if (selectedItems != null)
            {
                List<Author> authors = _unitOfWork.Authors.GetAll().Where(x => selectedItems.Contains(x.AuthorId)).ToList();
                List<BookAuthor> currentBookAuthors = new List<BookAuthor>();

                foreach (var newAuthor in authors)
                {
                    if (has.FirstOrDefault(x => x.AuthorId == newAuthor.AuthorId) == null)
                    {
                        currentBookAuthors.Add(new BookAuthor() { Book = bookModel, BookId = bookModel.BookId, Author = newAuthor, AuthorId = newAuthor.AuthorId });
                    }
                }

                _unitOfWork.BookAuthors.Insert(currentBookAuthors);
            }
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
