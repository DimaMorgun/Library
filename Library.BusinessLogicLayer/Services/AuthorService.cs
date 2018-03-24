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

        public void Insert(AuthorViewModel author, int[] selectedItems)
        {
            Author authorModel = Mapper.Map<AuthorViewModel, Author>(author);

            if (selectedItems != null)
            {
                foreach (var book in _unitOfWork.Books.GetAll().Where(x => selectedItems.Contains(x.BookId)))
                {
                    //authorModel.Books.Add(book);
                }
            }

            _unitOfWork.Authors.Insert(authorModel);
        }

        public void Delete(int id)
        {
            var authorModel = _unitOfWork.Authors.Get(id);
            _unitOfWork.Authors.Delete(authorModel);
        }

        public AuthorViewModel Get(int id)
        {
            Author authorModel = _unitOfWork.Authors.Get(id);

            AuthorViewModel authorViewModel = Mapper.Map<Author, AuthorViewModel>(authorModel);

            return authorViewModel;
        }

        public List<AuthorViewModel> GetAll()
        {
            List<Author> allAuthorsModel = _unitOfWork.Authors.GetAll();

            List<AuthorViewModel> allAuthorsViewModel = Mapper.Map<List<Author>, List<AuthorViewModel>>(allAuthorsModel);

            return allAuthorsViewModel;
        }

        public AuthorBooksViewModel GetAuthorBooks()
        {
            List<Book> allBooksModel = _unitOfWork.Books.GetAll();

            List<BookViewModel> allBooksViewModel = Mapper.Map<List<Book>, List<BookViewModel>>(allBooksModel);

            var authorBooksViewModel = new AuthorBooksViewModel
            {
                Books = allBooksViewModel
            };

            return authorBooksViewModel;
        }

        public AuthorBooksViewModel GetAuthorBooks(int id)
        {
            Author authorModel = _unitOfWork.Authors.Get(id);
            List<Book> allBooksModel = _unitOfWork.Books.GetAll();

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
            Author authorModel = _unitOfWork.Authors.Get(author.AuthorId);
            authorModel.Name = author.Name;
            authorModel.Birthday = author.Birthday;
            authorModel.Deathday = author.Deathday;

            //authorModel.Books.Clear();
            if (selectedItems != null)
            {
                foreach (var book in _unitOfWork.Books.GetAll().Where(x => selectedItems.Contains(x.BookId)))
                {
                    //authorModel.Books.Add(book);
                }
            }
            _unitOfWork.Authors.Update(authorModel);
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
