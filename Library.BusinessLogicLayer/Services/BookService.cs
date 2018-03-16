using Library.BusinessLogicLayer.Interfaces;
using Library.DataAccessLayer.UnitOfWork;
using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;

using AutoMapper;

using System.Collections.Generic;
using System.Linq;


namespace Library.BusinessLogicLayer.Services
{
    public class BookService : IService<BookViewModel>
    {
        private UnitOfWork _unitOfWork;

        public BookService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Create(BookViewModel book, int[] selectedItems)
        {
            Book bookModel = Mapper.Map<BookViewModel, Book>(book);

            if (selectedItems != null)
            {
                foreach (var author in _unitOfWork.Authors.GetList().Where(x => selectedItems.Contains(x.AuthorId)))
                {
                    bookModel.Authors.Add(author);
                }
            }

            _unitOfWork.Books.Create(bookModel);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Books.Delete(id);
            _unitOfWork.Save();
        }

        public BookViewModel GetByid(int id)
        {
            Book bookModel = _unitOfWork.Books.GetByid(id);

            BookViewModel bookViewModel = Mapper.Map<Book, BookViewModel>(bookModel);

            return bookViewModel;
        }

        public List<BookViewModel> GetList()
        {
            List<Book> allBooks = _unitOfWork.Books.GetList();

            List<BookViewModel> allBooksView = Mapper.Map<List<Book>, List<BookViewModel>>(allBooks);

            return allBooksView;
        }

        public BookAuthorsViewModel GetBookAuthors()
        {
            List<Author> allAuthors = _unitOfWork.Authors.GetList();

            List<AuthorViewModel> authorViewModel = Mapper.Map<List<Author>, List<AuthorViewModel>>(allAuthors);

            var bookAuthorsViewModel = new BookAuthorsViewModel
            {
                Authors = authorViewModel
            };

            return bookAuthorsViewModel;
        }

        public BookAuthorsViewModel GetBookAuthors(int id)
        {
            Book bookModel = _unitOfWork.Books.GetByid(id);
            List<Author> allAuthors = _unitOfWork.Authors.GetList();
            
            List<AuthorViewModel> authorViewModel = Mapper.Map<List<Author>, List<AuthorViewModel>>(allAuthors);
            BookViewModel bookViewModel = Mapper.Map<Book, BookViewModel>(bookModel);

            var bookAuthorsViewModel = new BookAuthorsViewModel
            {
                Book = bookViewModel,
                Authors = authorViewModel
            };

            return bookAuthorsViewModel;
        }

        public void Update(BookViewModel book, int[] selectedItems)
        {
            Book bookModel = _unitOfWork.Books.GetByid(book.BookId);
            bookModel.Name = book.Name;
            bookModel.YearOfPublishing = book.YearOfPublishing;

            bookModel.Authors.Clear();
            if (selectedItems != null)
            {
                foreach (var author in _unitOfWork.Authors.GetList().Where(x => selectedItems.Contains(x.AuthorId)))
                {
                    bookModel.Authors.Add(author);
                }
            }
            _unitOfWork.Books.Update(bookModel);
            _unitOfWork.Save();
        }
    }
}
