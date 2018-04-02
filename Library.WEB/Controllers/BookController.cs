using Library.BusinessLogicLayer.Services;
using Library.ViewModelLayer.ViewModels;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

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
        public JsonResult GetAll()
        {
            List<BookViewModel> allBooks = _bookService.GetAll();

            return Json(allBooks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateView()
        {
            BookAuthorsPublicationHousesViewModel bookAuthors = _bookService.GetBookAuthorsPublicationHouses();

            return View(bookAuthors);
        }

        [HttpPost]
        public ActionResult Create(BookViewModel book)
        {
            _bookService.Insert(book);
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
        public ActionResult Update(BookViewModel book)
        {
            _bookService.Update(book);

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