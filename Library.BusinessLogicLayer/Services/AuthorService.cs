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

        public void Create(AuthorViewModel author, int[] selectedItems)
        {
            Author authorModel = Mapper.Map<AuthorViewModel, Author>(author);

            if (selectedItems != null)
            {
                foreach (var book in _unitOfWork.Books.GetList().Where(x => selectedItems.Contains(x.BookId)))
                {
                    authorModel.Books.Add(book);
                }
            }

            _unitOfWork.Authors.Create(authorModel);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Authors.Delete(id);
            _unitOfWork.Save();
        }

        public AuthorViewModel GetByid(int id)
        {
            Author authorModel = _unitOfWork.Authors.GetByid(id);

            AuthorViewModel authorViewModel = Mapper.Map<Author, AuthorViewModel>(authorModel);

            return authorViewModel;
        }

        public List<AuthorViewModel> GetList()
        {
            List<Author> allAuthorsModel = _unitOfWork.Authors.GetList();

            List<AuthorViewModel> allAuthorsViewModel = Mapper.Map<List<Author>, List<AuthorViewModel>>(allAuthorsModel);

            return allAuthorsViewModel;
        }

        public AuthorBooksViewModel GetAuthorBooks()
        {
            List<Book> allBooksModel = _unitOfWork.Books.GetList();

            List<BookViewModel> allBooksViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);

            var authorBooksViewModel = new AuthorBooksViewModel
            {
                Books = allBooksViewModel
            };

            return authorBooksViewModel;
        }

        public AuthorBooksViewModel GetAuthorBooks(int id)
        {
            Author authorModel = _unitOfWork.Authors.GetByid(id);
            List<Book> allBooksModel = _unitOfWork.Books.GetList();

            List<BookViewModel> bookViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);
            AuthorViewModel authorViewModel = Mapper.Map<Author, AuthorViewModel>(authorModel);

            var authorBooksViewModel = new AuthorBooksViewModel
            {
                Books = bookViewModel,
                Author = authorViewModel
            };

            return authorBooksViewModel;
        }

        public void Update(AuthorViewModel author, int[] selectedItems)
        {
            Author authorModel = _unitOfWork.Authors.GetByid(author.AuthorId);
            authorModel.Name = author.Name;
            authorModel.Birthday = author.Birthday;
            authorModel.Deathday = author.Deathday;

            authorModel.Books.Clear();
            if (selectedItems != null)
            {
                foreach (var book in _unitOfWork.Books.GetList().Where(x => selectedItems.Contains(x.BookId)))
                {
                    authorModel.Books.Add(book);
                }
            }
            _unitOfWork.Authors.Update(authorModel);
            _unitOfWork.Save();
        }

        public void SaveToJSON(int id)
        {
            Author authorModel = _unitOfWork.Authors.GetByid(id);

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
