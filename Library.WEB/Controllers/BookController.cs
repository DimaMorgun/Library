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
            List<BookViewModel> allBooks = _bookService.GetAll();

            return View(allBooks);
        }

        [HttpGet]
        public ActionResult CreateView()
        {
            BookAuthorsPublicationHousesViewModel bookAuthors = _bookService.GetBookAuthorsPublicationHouses();

            return View(bookAuthors);
        }

        [HttpPost]
        public ActionResult Create(BookViewModel book, int[] selectedAuthors, int[] selectedPublicationHouses)
        {
            _bookService.Insert(book, selectedAuthors, selectedPublicationHouses);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetByIdView(int id)
        {
            BookViewModel book = _bookService.Get(id);

            return View(book);
        }

        [HttpGet]
        public ActionResult UpdateView(int id)
        {
            BookAuthorsPublicationHousesViewModel bookAuthors = _bookService.GetBookAuthorsPublicationHouses(id);

            return View(bookAuthors);
        }

        [HttpPost]
        public ActionResult Update(BookViewModel book, int[] selectedAuthors, int[] selectedPublicationHouses)
        {
            _bookService.Update(book, selectedAuthors, selectedPublicationHouses);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteView(int id)
        {
            BookViewModel book = _bookService.Get(id);

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