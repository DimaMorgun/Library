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
    public class AuthorService
    {
        private UnitOfWork _unitOfWork;

        public AuthorService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Insert(AuthorViewModel author, int[] selectedBooks)
        {
            var authorModel = Mapper.Map<AuthorViewModel, Author>(author);
            var bookAuthorsModel = new List<BookAuthor>();

            var authorId = _unitOfWork.Authors.Insert(authorModel);

            if (selectedBooks != null)
            {
                foreach (var bookId in selectedBooks)
                {
                    bookAuthorsModel.Add(new BookAuthor() { BookId = bookId, AuthorId = authorId });
                }
            }

            _unitOfWork.BookAuthors.Insert(bookAuthorsModel);
        }

        public AuthorViewModel Get(int id)
        {
            List<BookAuthor> bookAuthors = _unitOfWork.BookAuthors.GetAllById<Author>(id);

            var author = new Author() { AuthorId = bookAuthors.ElementAt(0).Author.AuthorId, Name = bookAuthors.ElementAt(0).Author.Name, Birthday = bookAuthors.ElementAt(0).Author.Birthday, Deathday = bookAuthors.ElementAt(0).Author.Deathday };
            var books = new List<Book>();
            foreach (var book in bookAuthors)
            {
                if (book.Book != null)
                {
                    books.Add(book.Book);
                }
            }
            var authorViewModel = Mapper.Map<Author, AuthorViewModel>(author);
            authorViewModel.Books = Mapper.Map<List<Book>, List<BookViewModel>>(books);

            return authorViewModel;
        }

        public List<AuthorViewModel> GetAll()
        {
            List<Author> allAuthorsModel = _unitOfWork.Authors.GetAll();

            var allAuthorsViewModel = Mapper.Map<List<Author>, List<AuthorViewModel>>(allAuthorsModel);

            return allAuthorsViewModel;
        }

        public AuthorBooksViewModel GetAuthorBooks()
        {
            List<Book> allBooks = _unitOfWork.Books.GetAll();

            var allBooksViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooks);

            var authorBooksViewModel = new AuthorBooksViewModel
            {
                Books = allBooksViewModel
            };

            return authorBooksViewModel;
        }

        public AuthorBooksViewModel GetAuthorBooks(int id)
        {
            Author authorModel = _unitOfWork.Authors.Get(id);
            List<BookAuthor> allBookAuthors = _unitOfWork.BookAuthors.GetAll();
            List<Book> allBooksModel = _unitOfWork.Books.GetAll();

            var bookAuthorsRelationViewModel = Mapper.Map<List<BookAuthor>, List<BookAuthorsRelationViewModel>>(allBookAuthors);
            var bookViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);
            var authorViewModel = Mapper.Map<Author, AuthorViewModel>(authorModel);

            var authorBooksViewModel = new AuthorBooksViewModel
            {
                Books = bookViewModel,
                Author = authorViewModel,
                BookAuthors = bookAuthorsRelationViewModel
            };

            return authorBooksViewModel;
        }

        public void Update(AuthorViewModel author, int[] selectedBooks)
        {
            Author authorModel = _unitOfWork.Authors.Get(author.AuthorId);
            authorModel.Name = author.Name;
            authorModel.Birthday = author.Birthday;
            authorModel.Deathday = author.Deathday;
            _unitOfWork.Authors.Update(authorModel);

            List<BookAuthor> oldBookAuthors = _unitOfWork.BookAuthors.GetAllById<Author>(author.AuthorId);
            var oldBookAuthorsWithRelation = oldBookAuthors.Where(x => x.BookId != 0).ToList();
            selectedBooks = selectedBooks == null ? new int[0] : selectedBooks;
            var BooksHas = oldBookAuthorsWithRelation.Where(x => selectedBooks.Contains(x.BookId)).ToList();
            var BooksNothas = oldBookAuthorsWithRelation.Where(x => !selectedBooks.Contains(x.BookId)).ToList();
            _unitOfWork.BookAuthors.Delete(BooksNothas);
            if (selectedBooks != null)
            {
                List<BookAuthor> currentBookAuthors = new List<BookAuthor>();

                foreach (var newBookId in selectedBooks)
                {
                    if (BooksHas.FirstOrDefault(x => x.BookId == newBookId) == null)
                    {
                        currentBookAuthors.Add(new BookAuthor() { BookId = newBookId, AuthorId = authorModel.AuthorId });
                    }
                }
                _unitOfWork.BookAuthors.Insert(currentBookAuthors);
            }
        }

        public void Delete(int id)
        {
            var authorModel = _unitOfWork.Authors.Get(id);
            _unitOfWork.Authors.Delete(authorModel);
        }

        public void SaveToJSON(int id)
        {
            Author authorModel = _unitOfWork.Authors.Get(id);

            string json = JsonConvert.SerializeObject(
                authorModel,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var path = HttpContext.Current.Server.MapPath("~/App_Data/Author.json");

            System.IO.File.WriteAllText(path, json);
        }
    }
}
