using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<BookViewModel> allBooks = _bookService.GetList();

            return View(allBooks);
        }

        [HttpGet]
        public ActionResult CreateView()
        {
            BookAuthorsViewModel bookAuthors = _bookService.GetBookAuthors();

            return View(bookAuthors);
        }

        [HttpPost]
        public ActionResult Create(BookViewModel book, int[] selectedItems)
        {
            _bookService.Create(book, selectedItems);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetByIdView(int id)
        {
            BookViewModel book = _bookService.GetByid(id);

            return View(book);
        }

        [HttpGet]
        public ActionResult UpdateView(int id)
        {
            BookAuthorsViewModel bookAuthors = _bookService.GetBookAuthors(id);

            return View(bookAuthors);
        }

        [HttpPost]
        public ActionResult Update(BookViewModel book, int[] selectedItems)
        {
            _bookService.Update(book, selectedItems);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteView(int id)
        {
            BookViewModel book = _bookService.GetByid(id);

            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _bookService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SaveToJSON(int id)
        {
            _bookService.SaveToJSON(id);

            return RedirectToAction("Index");
        }
    }
}